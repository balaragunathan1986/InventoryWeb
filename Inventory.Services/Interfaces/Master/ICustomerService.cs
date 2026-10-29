using Inventory.Domain.DTO;

namespace Inventory.Services.Interfaces.Master;

public interface ICustomerService
{
    Task<List<CustomerDTO>> GetCustomers();
    Task<CustomerDTO> GetCustomer(Guid customerId);
    Task<CustomerDTO> Save(CustomerDTO customer);
    Task<CustomerDTO> Update(CustomerDTO customer);
    Task<bool> Delete(Guid customerId);
}
