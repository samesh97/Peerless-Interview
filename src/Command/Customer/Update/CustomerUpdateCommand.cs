using MediatR;
using PeerlessInterview.src.Domain.Dto;

namespace PeerlessInterview.src.Command.Customer.Update;

public class CustomerUpdateCommand : IRequest<PeerlessInterview.src.Domain.Entities.Customer>
{
    public string _custCode { get; set; } = string.Empty;
    public CustomerUpdateDto _dto { get; set; }

    public CustomerUpdateCommand(string custCode, CustomerUpdateDto dto)
    {
        _custCode = custCode;
        _dto = dto;
    }
}