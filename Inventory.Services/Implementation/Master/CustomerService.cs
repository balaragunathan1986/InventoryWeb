using Inventory.Domain.DTO;
using Inventory.Repository.Interfaces.Master;
using Inventory.Services.Interfaces.Master;

namespace Inventory.Services.Implementation.Master;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<List<CustomerDTO>> GetCustomers() => _customerRepository.GetCustomers();

    public Task<CustomerDTO> GetCustomer(Guid customerId) => _customerRepository.GetCustomer(customerId);

    public Task<CustomerDTO> Save(CustomerDTO customer) => _customerRepository.Save(customer);

    public Task<CustomerDTO> Update(CustomerDTO customer) => _customerRepository.Update(customer);

    public Task<bool> Delete(Guid customerId) => _customerRepository.Delete(customerId);
}
