using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace bike_store_2.Conforming_Services.Phone_Service
{

    public interface IPhoneService
    {
        Task SendsmsAsync(string toPhoneNumber, string message);
    }



    public class PhoneService : IPhoneService
    {

        private readonly IConfiguration _configuration;
        public PhoneService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendsmsAsync(string toPhoneNumber, string message)
        {
            var accountSid = _configuration["Twilio:AccountSid"];
            var authToken = _configuration["Twilio:AuthToken"];
            var fromPhoneNumber = _configuration["Twilio:FromPhone"];

            // Create a Twilio client
            TwilioClient.Init(accountSid, authToken);

            await MessageResource.CreateAsync(
                to: new PhoneNumber(toPhoneNumber),
                from: new PhoneNumber(fromPhoneNumber),                
                body: message                
            );

        }
    }
}
