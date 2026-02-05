using System.ComponentModel.DataAnnotations;
using MediatR;
using PeerlessInterview.src.Repository.Customer;

namespace PeerlessInterview.src.Query.Customer.Filter;

public class CustomerFilterQueryHandler : IRequestHandler<CustomerFilterQuery, List<PeerlessInterview.src.Domain.Entities.Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerFilterQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<List<Domain.Entities.Customer>> Handle(CustomerFilterQuery request, CancellationToken cancellationToken)
    {
        var dto = request._dto;
        if(dto == null)
        {
            throw new Api.Exception.ValidationException("Filteration criteria object cannot be null.");
        }

        if(!dto.hasAnyFilter())
        {
            throw new Api.Exception.ValidationException("At least one filteration criteria must be provided.");
        }

        var result = _customerRepository.GetCustomersByFilter(dto);
        return Task.FromResult(result);
    }
}