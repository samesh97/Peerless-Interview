using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeerlessInterview.src.Api.Response;
using PeerlessInterview.src.Domain.Dto;
using PeerlessInterview.src.Query.Customer.Filter;

namespace PeerlessInterview.src.Controller.Customer;

[ApiController]
[Route("api/v1/customers")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("filter")]
    public IActionResult FindCustomers([FromBody] CustomerSearchDto dto)
    {
        var result = _mediator.Send(new CustomerFilterQuery(dto));
        return Ok(CommonResponse.Success(result.Result));
    }

    [HttpPost]
    public IActionResult CreateCustomer(
        [FromBody] CustomerCreateDto dto
    )
    {

        if(!ModelState.IsValid)
        {
            var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(CommonResponse.Error(errors));
        }


        var rs = _mediator.Send(new Command.Customer.Create.CustomerCreateCommand(dto));
        
        return Ok(CommonResponse.Success(rs.Result));
    }

    [HttpPatch("{customerCode}")]
    public IActionResult UpdateCustomer(string customerCode, [FromBody] CustomerUpdateDto dto)
    {
        if(string.IsNullOrEmpty(customerCode))
        {
            return BadRequest(CommonResponse.Error("Customer Code is required"));
        }

        if(dto == null)
        {
            return BadRequest(CommonResponse.Error("Request body is required"));
        }

        if(dto.Name == null && dto.ShortName == null && dto.City == null && dto.State == null && dto.Country == null)
        {
            return BadRequest(CommonResponse.Error("No fields to update."));
        }

        var rs = _mediator.Send(new Command.Customer.Update.CustomerUpdateCommand(customerCode, dto));

        return Ok(CommonResponse.Success(rs.Result));
    }
}