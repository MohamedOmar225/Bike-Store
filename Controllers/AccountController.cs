using bike_store_2.Conforming_Services.Email_Service;
using bike_store_2.Conforming_Services.Phone_Service;
using bike_store_2.Data;
using bike_store_2.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]              
    //[Authorize(Roles ="Admin")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly IPhoneService _phoneService;

        public AccountController(UserManager<ApplicationUser> userManager
            , IConfiguration configuration, RoleManager<IdentityRole> roleManager
            , IEmailService emailService, IPhoneService phoneService)
        {
            _usermanger = userManager;
            _config = configuration;
            _roleManager = roleManager;
            _emailService = emailService;
            _phoneService = phoneService;
        }

        //Register
        [HttpPost("register")]
        public async Task<IActionResult> Registeration([FromForm]RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                // create new user  
                var applicationUser = new ApplicationUser
                {
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    PhoneNumber = userDTO.PhoneNumber
                };

                // add user to database
                IdentityResult result = await _usermanger.CreateAsync(applicationUser, userDTO.Password);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                // Ensure roles exist
                if (!await _roleManager.RoleExistsAsync("Admin")) 
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                if (!await _roleManager.RoleExistsAsync("User"))
                    await _roleManager.CreateAsync(new IdentityRole("User"));

                // Assign role to user
                var first = _usermanger.Users.Count() == 1;
                var role = first ? "Admin" : "User";
                await _usermanger.AddToRoleAsync(applicationUser, role);

                // Generate token for email confirmation 
                var emailtoken = await _usermanger.GenerateEmailConfirmationTokenAsync(applicationUser);
                                     // create url link   
                var emailConfirmationLink = Url.Action(
                    nameof(ConfirmEmail), // action name
                    "Account", // this controller name contain (ConfiromEmail)
                    new { userId = applicationUser.Id, token = emailtoken }, // route values
                    Request.Scheme); // protocol (http or https)

                // send email
                await _emailService.SendEmailAsync(
                    userDTO.Email, // recipient email
                    "Confirm your email", // subject
                    $"<a href='{emailConfirmationLink}'>Click here to confirm your email</a>"); // html body


                //// send sms
                //var Phonetoken = await _usermanger.GenerateChangePhoneNumberTokenAsync(applicationUser, userDTO.PhoneNumber);
                //await _phoneService.SendsmsAsync(userDTO.PhoneNumber, $"Your phone number confirmation code is:{Phonetoken}");

                return Ok(new
                {
                    message = $"User created successfully with role {role} , Please confirm your email and phone",
                    //emailConfirmationLink = emailConfirmationLink,
                    //phoneConfirmationCode = Phonetoken
                });

            }
            return BadRequest(ModelState);
        }




        [Authorize(Roles = "Admin")]
        [HttpPost("Change Roles")]
        public async Task<IActionResult> ChangeRoles([FromForm] ChangeRolesDto rolesDto)
        {
            var user = await _usermanger.FindByIdAsync(rolesDto.UserId);
            if (user == null)
                return NotFound("User not found");

            // Check if roles exist
            foreach (var role in rolesDto.NewRoles)
            {
                if (!await _roleManager.RoleExistsAsync(role)) // (Admin - User) 
                    return BadRequest($"Role {role} does not exist"); 
            }

            if(rolesDto.ReplaceOldRoles) // if true remove old roles
            {
                var oldRoles = await _usermanger.GetRolesAsync(user); // Get all old roles
                var result = await _usermanger.RemoveFromRolesAsync(user, oldRoles); // remove old roles

                if (!result.Succeeded)
                    return BadRequest("Failed to remove old roles");
            }

            // Add new roles 
            foreach (var role in rolesDto.NewRoles)
            {
                if(!await _usermanger.IsInRoleAsync(user, role)) // check if user not in this role 
                {
                    var addresult = await _usermanger.AddToRoleAsync(user, role); // add new role
                    if (!addresult.Succeeded)
                        return BadRequest($"Failed to add role {role}");
                }                                    
            }

            var userRoles = await _usermanger.GetRolesAsync(user); // Get all new user roles
            return Ok(new
            {
                message = "Roles updated successfully",                
                newRoles = userRoles
            });
        }









        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            
            var user = await _usermanger.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            var result = await _usermanger.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return BadRequest("Invalid confirmation link");

            return Ok("Email confirmed successfully");
        }





        [HttpPost("confirm-phone")]
        public async Task<IActionResult> Confirmphone([FromForm]string UserId, [FromForm]string PhoneNumber, [FromForm]string code)
        {            

            var user = await _usermanger.FindByIdAsync(UserId);
            if (user == null)
                return NotFound("User not found");

            var result = await _usermanger.ChangePhoneNumberAsync(user, PhoneNumber, code);
            if (!result.Succeeded)
                return BadRequest("Invalid phone confirmation code");

            return Ok("Phone number confirmed successfully");
        }







        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO dto)
        {
            var user = await _usermanger.FindByNameAsync(dto.UserName);
            if (user == null || !await _usermanger.CheckPasswordAsync(user, dto.Password))
                return Unauthorized();

            var roles = await _usermanger.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);

            return Ok(new
            {
                token = token,
                expiration = DateTime.UtcNow.AddHours(1)
            });
        }






        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role , "Admin")
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecrityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
