using System.ComponentModel.DataAnnotations;
using MediatR;
using PeerlessInterview.src.Repository.Customer;
using PeerlessInterview.src.Util;

namespace PeerlessInterview.src.Command.Customer.Create;

public class CustomerCreateCommandHanlder : IRequestHandler<CustomerCreateCommand, PeerlessInterview.src.Domain.Entities.Customer>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCreateCommandHanlder(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<PeerlessInterview.src.Domain.Entities.Customer> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {

        var dto = request._dto;

        if(dto == null)
        {
            throw new ValidationException("Payload cannot be null.");
        }

        if(string.IsNullOrWhiteSpace(dto.CustCode))
        {
            throw new ValidationException("Customer code is required.");
        }

        if(string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ValidationException("Customer name is required.");
        }

        if(dto.Status != null)
        {
            var status = CommonUtil.GetStatusInstance(dto.Status.GetValueOrDefault());
            if(status == null)
            {
                throw new ValidationException("Invalid status value.");
            }
        }

        var savedCustomer = _customerRepository.CreateCustomer(dto);
        return Task.FromResult(savedCustomer);
    }
}