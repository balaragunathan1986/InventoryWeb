using Inventory.Domain.DTO;

namespace Inventory.Repository.Interfaces.Master;

public interface ICustomerRepository
{
    Task<List<CustomerDTO>> GetCustomers();
    Task<CustomerDTO> GetCustomer(Guid customerId);
    Task<CustomerDTO> Save(CustomerDTO customer);
    Task<CustomerDTO> Update(CustomerDTO customer);
    Task<bool> Delete(Guid customerId);
}
