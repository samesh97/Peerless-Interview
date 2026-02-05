using MediatR;
using PeerlessInterview.src.Api.Exception;
using PeerlessInterview.src.Repository.Customer;
using PeerlessInterview.src.Util;

namespace PeerlessInterview.src.Command.Customer.Update;

public class CustomerUpdateCommandHandler : IRequestHandler<CustomerUpdateCommand, PeerlessInterview.src.Domain.Entities.Customer>
{

    private readonly ICustomerRepository _customerRepository;

    public CustomerUpdateCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<Domain.Entities.Customer> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
    {
        var customerCode = request._custCode;
        var dto = request._dto;

        if(customerCode == null)
        {
            throw new ValidationException("Customer code is required.");
        }

        if(dto == null)
        {
            throw new ValidationException("Payload cannot be null.");
        }

        if(dto.Status != null)
        {
            var status = CommonUtil.GetStatusInstance(dto.Status.GetValueOrDefault());
            if(status == null)
            {
                throw new ValidationException("Invalid status value.");
            }
        }

        var rs = _customerRepository.UpdateCustomer(dto, customerCode);

        return Task.FromResult(rs);

    }
}