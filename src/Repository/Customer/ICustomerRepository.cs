namespace PeerlessInterview.src.Repository.Customer;

using PeerlessInterview.src.Domain.Dto;
using PeerlessInterview.src.Domain.Entities;

public interface ICustomerRepository
{
    Customer CreateCustomer(CustomerCreateDto dto);
    Customer UpdateCustomer(CustomerUpdateDto dto, string customerCode);

    Customer? GetCustomerByCode(string custCode);
    List<Customer> GetCustomersByFilter(CustomerSearchDto filter);
}