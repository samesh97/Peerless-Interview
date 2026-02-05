using MediatR;
using PeerlessInterview.src.Domain.Dto;
using  PeerlessInterview.src.Domain.Entities;

namespace PeerlessInterview.src.Command.Customer.Create;

public class CustomerCreateCommand : IRequest<PeerlessInterview.src.Domain.Entities.Customer>
{
    public CustomerCreateDto _dto;
    public CustomerCreateCommand(CustomerCreateDto dto)
    {
        _dto = dto;
    }
}