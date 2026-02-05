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
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("filter")]
    public IActionResult FindCustomers([FromBody] CustomerSearchDto dto)
    {
        _logger.LogInformation($"Request to find customers received with filter: {dto}");
        var result = _mediator.Send(new CustomerFilterQuery(dto));
        return Ok(CommonResponse.Success(result.Result));
    }

    [HttpPost]
    public IActionResult CreateCustomer(
        [FromBody] CustomerCreateDto dto
    )
    {
        _logger.LogInformation($"Request to create a customer received with payload: {dto}");

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
        _logger.LogInformation($"Request to update customer received with payload: {dto}, and customerCode: {customerCode}");

        if(string.IsNullOrEmpty(customerCode))
        {
            return BadRequest(CommonResponse.Error("Customer Code is required"));
        }

        if(dto == null)
        {
            return BadRequest(CommonResponse.Error("Request body is required"));
        }

        if(!dto.hasAnyUpdate())
        {
            return BadRequest(CommonResponse.Error("No fields to update."));
        }

        var rs = _mediator.Send(new Command.Customer.Update.CustomerUpdateCommand(customerCode, dto));
        return Ok(CommonResponse.Success(rs.Result));
    }
}