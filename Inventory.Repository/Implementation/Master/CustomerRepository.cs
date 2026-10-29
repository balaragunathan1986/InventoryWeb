using Inventory.Domain.DTO;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Master;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repository.Implementation.Master;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDBContext _dbContext;

    public CustomerRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<CustomerDTO>> GetCustomers()
    {
        return _dbContext.Customer
            .Where(customer => !customer.IsDeleted)
            .ToListAsync();
    }

    public async Task<CustomerDTO> GetCustomer(Guid customerId)
    {
        return await _dbContext.Customer
            .FirstOrDefaultAsync(customer => customer.Customer_Id == customerId && !customer.IsDeleted)
            ?? new CustomerDTO();
    }

    public async Task<CustomerDTO> Save(CustomerDTO customer)
    {
        if (customer.Customer_Id == Guid.Empty)
        {
            customer.Customer_Id = Guid.NewGuid();
        }

        await _dbContext.Customer.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
        return customer;
    }

    public async Task<CustomerDTO> Update(CustomerDTO customer)
    {
        var existingCustomer = await _dbContext.Customer.FirstOrDefaultAsync(item =>
            item.Customer_Id == customer.Customer_Id &&
            item.Id == customer.Id &&
            !item.IsDeleted);

        if (existingCustomer is null)
        {
            return new CustomerDTO();
        }

        existingCustomer.Customer_Name = customer.Customer_Name;
        existingCustomer.Address1 = customer.Address1;
        existingCustomer.Address2 = customer.Address2;
        existingCustomer.City = customer.City;
        existingCustomer.Customer_State = customer.Customer_State;
        existingCustomer.Country = customer.Country;
        existingCustomer.Phone_Number = customer.Phone_Number;
        existingCustomer.Email = customer.Email;
        existingCustomer.Gst_Number = customer.Gst_Number;
        existingCustomer.IsActive = customer.IsActive;
        existingCustomer.UpdatedBy = customer.UpdatedBy;
        existingCustomer.UpdatedAt = customer.UpdatedAt;

        await _dbContext.SaveChangesAsync();
        return existingCustomer;
    }

    public async Task<bool> Delete(Guid customerId)
    {
        var customer = await _dbContext.Customer.FirstOrDefaultAsync(item =>
            item.Customer_Id == customerId && !item.IsDeleted);

        if (customer is null)
        {
            return false;
        }

        customer.IsDeleted = true;
        customer.IsActive = false;
        customer.DeletedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
