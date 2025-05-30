using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace bike_store_2.Repositories
{




    public interface IEmployeeRepository
    {
        // Define methods for employee-related operations
        Task<IEnumerable<EmployeeMakedOrdersDto>> GetAllActiveEmployeesAsync();
        Task<IEnumerable<EmployeeMakedOrdersDto>> GetAllDeletingEmployeesAsync();
        Task<EmployeeMakedOrdersDto?> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<EmployeeMakedOrdersDto?>> GetEmployeeByNameAsync(string employeeName);
        Task<Employee> CreateEmployeeAsync(CreateEmployeeDTO employeeDTO);
        Task<Employee> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDTO updateDTO);
        Task DeleteEmployeeAsync(int employeeId);
    }



    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }

        public async Task<Employee> CreateEmployeeAsync(CreateEmployeeDTO employeeDTO)
        {
            var store = await _appDbContext.Stores
                .FirstOrDefaultAsync(s => s.StoreId == employeeDTO.StoreId && s.IsExist);
            if (store == null)
                throw new KeyNotFoundException($"Store with ID {employeeDTO.StoreId} not found.");

            var employee = new Employee
            {
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeEmail = employeeDTO.EmployeeEmail,
                EmployeePhone = employeeDTO.EmployeePhone,
                EmployeeSalary = employeeDTO.EmployeeSalary,
                StoreId = employeeDTO.StoreId,
                IsActive = true
            };
            _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();
            return employee;
        }




        public async Task<Employee> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDTO updateDTO)
        {
            var store = await _appDbContext.Stores
               .FirstOrDefaultAsync(s => s.StoreId == updateDTO.StoreId && s.IsExist);
            if (store == null)
                throw new KeyNotFoundException($"Store with ID {updateDTO.StoreId} not found..");

            var employee = await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && e.IsActive);
            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");

            employee.EmployeeName = updateDTO.EmployeeName;
            employee.EmployeeEmail = updateDTO.EmployeeEmail;
            employee.EmployeePhone = updateDTO.EmployeePhone;
            employee.EmployeeSalary = updateDTO.EmployeeSalary;
            employee.StoreId = updateDTO.StoreId;
            employee.IsActive = updateDTO.IsActive;
            _appDbContext.Employees.Update(employee);
            await _appDbContext.SaveChangesAsync();
            return employee;
        }




        public async Task<IEnumerable<EmployeeMakedOrdersDto>> GetAllActiveEmployeesAsync()
        {
            var employees = await _appDbContext.Employees.Where(e => e.IsActive).Include(e => e.Store).ToListAsync();

            var result = new List<EmployeeMakedOrdersDto>();
            foreach (var employee in employees)
            {
                var orders = await _appDbContext.Orders
                    .Select(o => new OrderShortDto
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        ShippedDate = o.ShippedDate,
                        TotalAmount = o.TotalAmount
                    }).ToListAsync();
                var employeeDto = new EmployeeMakedOrdersDto
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeEmail = employee.EmployeeEmail,
                    EmployeePhone = employee.EmployeePhone,
                    EmployeeSalary = employee.EmployeeSalary,
                    StoreId = employee.StoreId,
                    IsActive = true,
                    Orders = orders
                };
                result.Add(employeeDto);
            }
            return result;
        }




        public async Task<IEnumerable<EmployeeMakedOrdersDto>> GetAllDeletingEmployeesAsync()
        {
            var employees = await _appDbContext.Employees.Where(e => !e.IsActive).Include(e => e.Store).ToListAsync();

            var result = new List<EmployeeMakedOrdersDto>();
            foreach (var employee in employees)
            {
                var orders = await _appDbContext.Orders
                    .Select(o => new OrderShortDto
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        ShippedDate = o.ShippedDate,
                        TotalAmount = o.TotalAmount
                    }).ToListAsync();
                var employeeDto = new EmployeeMakedOrdersDto
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeEmail = employee.EmployeeEmail,
                    EmployeePhone = employee.EmployeePhone,
                    EmployeeSalary = employee.EmployeeSalary,
                    StoreId = employee.StoreId,
                    IsActive = true,
                    Orders = orders
                };
                result.Add(employeeDto);
            }
            return result;
        }





        public async Task<EmployeeMakedOrdersDto?> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _appDbContext.Employees
                .Include(e => e.Store)
                .Include(e => e.Orders)                              
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && e.IsActive);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");

            var orders = await _appDbContext.Orders                
                .Select(o => new OrderShortDto
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    ShippedDate = o.ShippedDate,
                    TotalAmount = o.TotalAmount
                }).ToListAsync();

            var employeeDto = new EmployeeMakedOrdersDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                EmployeeEmail = employee.EmployeeEmail,
                EmployeePhone = employee.EmployeePhone,
                EmployeeSalary = employee.EmployeeSalary,
                StoreId = employee.StoreId,
                IsActive = true,
                Orders = orders
            };

            return employeeDto;
        }




        public async Task<IEnumerable<EmployeeMakedOrdersDto?>> GetEmployeeByNameAsync(string employeeName)
        {
            var employees = await _appDbContext.Employees
                .Include(e => e.Store)
                .Include(e => e.Orders)
                .Where(e => e.EmployeeName.ToLower() == employeeName.ToLower() && e.IsActive)
                .ToListAsync();

            if (employees == null || employees.Count == 0)
                throw new KeyNotFoundException($"Employee with name {employeeName} not found.");

            var orders = employees.SelectMany(e => e.Orders)
                .Select(o => new OrderShortDto
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    ShippedDate = o.ShippedDate,
                    TotalAmount = o.TotalAmount
                }).ToList();

            var result = new List<EmployeeMakedOrdersDto>();
            foreach (var employee in employees)
            {
                var employeeDto = new EmployeeMakedOrdersDto
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeEmail = employee.EmployeeEmail,
                    EmployeePhone = employee.EmployeePhone,
                    EmployeeSalary = employee.EmployeeSalary,
                    StoreId = employee.StoreId,
                    IsActive = true,
                    Orders = orders
                };
                result.Add(employeeDto);
            }

            return result;
        }




        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && e.IsActive);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");

            employee.IsActive = false;
            _appDbContext.Employees.Update(employee);
            await _appDbContext.SaveChangesAsync();

            var orders = await _appDbContext.Orders
                .Where(o => o.EmployeeId == employeeId && o.IsExist)
                .ToListAsync();
            foreach (var order in orders)
            {
                order.IsExist = false;
                _appDbContext.Orders.Update(order);
            }
            await _appDbContext.SaveChangesAsync();
        }
             
        
    }
}
