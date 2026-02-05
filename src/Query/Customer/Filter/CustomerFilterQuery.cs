
using MediatR;
using PeerlessInterview.src.Domain.Dto;

namespace PeerlessInterview.src.Query.Customer.Filter;

public class CustomerFilterQuery : IRequest<List<PeerlessInterview.src.Domain.Entities.Customer>>
{
    public CustomerSearchDto _dto;

    public CustomerFilterQuery(CustomerSearchDto dto)
    {
        _dto = dto;
    }

}