
using Moq;
using PeerlessInterview.src.Api.Exception;
using PeerlessInterview.src.Command.Customer.Create;
using PeerlessInterview.src.Domain.Dto;
using PeerlessInterview.src.Domain.Entities;
using PeerlessInterview.src.Domain.Enums;
using PeerlessInterview.src.Repository.Customer;
using Xunit;

namespace PeerlessInterview.test.Command.Create;

public class CustomerCreateCommandHandlerTest
{
    private readonly Mock<ICustomerRepository> _customerRepository;

    public CustomerCreateCommandHandlerTest()
    {
        _customerRepository = new Mock<ICustomerRepository>();
    }

    [Fact]
    public async Task Test_Handle_WhenDtoIsNull()
    {
        var cccHanlder = new CustomerCreateCommandHanlder(_customerRepository.Object);
        CustomerCreateCommand command = new CustomerCreateCommand(null);
        await Assert.ThrowsAsync<ValidationException>(() => cccHanlder.Handle(command, CancellationToken.None));
        
    }

    [Fact]
    public async Task Test_Handle_WhenCustomerNameAndCodeIsNull()
    {
        var cccHanlder = new CustomerCreateCommandHanlder(_customerRepository.Object);
        var customerCreateDto = new CustomerCreateDto();
        CustomerCreateCommand command = new CustomerCreateCommand(customerCreateDto);
        await Assert.ThrowsAsync<ValidationException>(() => cccHanlder.Handle(command, CancellationToken.None));
        
    }

    [Fact]
    public async Task Test_Handle_WhenCustomerCodeIsNull()
    {
        var cccHanlder = new CustomerCreateCommandHanlder(_customerRepository.Object);
        var customerCreateDto = new CustomerCreateDto()
        {
            Name = "Test Customer"
        };
        CustomerCreateCommand command = new CustomerCreateCommand(customerCreateDto);
        await Assert.ThrowsAsync<ValidationException>(() => cccHanlder.Handle(command, CancellationToken.None));
        
    }

    [Fact]
    public async Task Test_Handle_WhenCustomerNameIsNull()
    {
        var cccHanlder = new CustomerCreateCommandHanlder(_customerRepository.Object);
        var customerCreateDto = new CustomerCreateDto()
        {
            CustCode = "CUST001"
        };
        CustomerCreateCommand command = new CustomerCreateCommand(customerCreateDto);
        await Assert.ThrowsAsync<ValidationException>(() => cccHanlder.Handle(command, CancellationToken.None));
        
    }

    [Fact]
    public async Task Test_Handle_WhenCustomerCodeHasOnlyWhitespace()
    {
        var cccHanlder = new CustomerCreateCommandHanlder(_customerRepository.Object);
        var customerCreateDto = new CustomerCreateDto()
        {
            CustCode = "   "
        };
        CustomerCreateCommand command = new CustomerCreateCommand(customerCreateDto);
        await Assert.ThrowsAsync<ValidationException>(() => cccHanlder.Handle(command, CancellationToken.None));
        
    }

    [Fact]
    public async Task Test_Handle_WhenCustomerNameHasOnlyWhitespace()
    {
        var cccHanlder = new CustomerCreateCommandHanlder(_customerRepository.Object);
        var customerCreateDto = new CustomerCreateDto()
        {
            Name = "   "
        };
        CustomerCreateCommand command = new CustomerCreateCommand(customerCreateDto);
        await Assert.ThrowsAsync<ValidationException>(() => cccHanlder.Handle(command, CancellationToken.None));
        
    }

    [Fact]
    public async Task Test_Handle_WhenInValidStatusSupplied()
    {
        var cccHanlder = new CustomerCreateCommandHanlder(_customerRepository.Object);
        var customerCreateDto = new CustomerCreateDto()
        {
            Name = "Test Customer",
            CustCode = "CUST001",
            Status = -99
        };
        CustomerCreateCommand command = new CustomerCreateCommand(customerCreateDto);
        await Assert.ThrowsAsync<ValidationException>(() => cccHanlder.Handle(command, CancellationToken.None));
        
    }

    [Fact]
    public async Task Test_Handle_WhenAllMandatoryFieldsAreSupplied()
    {   

        string customerName = "Test Customer";
        string customerCode = "CUST001";

        var cccHanlder = new CustomerCreateCommandHanlder(_customerRepository.Object);

        var customer = new Customer()
        {
            Name = customerName,
            CustCode = customerCode,
            Status = CustomerStatus.ACTIVE
        };

        var customerCreateDto = new CustomerCreateDto()
        {
            Name = customerName,
            CustCode = customerCode,
            Status = 1
        };

        _customerRepository.Setup(mock => mock.CreateCustomer(customerCreateDto)).Returns(customer);        
            

        CustomerCreateCommand command = new CustomerCreateCommand(customerCreateDto);
        var result = await cccHanlder.Handle(command, CancellationToken.None);

        Assert.Equal(customerCode, result.CustCode);
        Assert.Equal(customerName, result.Name);
        Assert.Equal(CustomerStatus.ACTIVE, result.Status);
        
    }
}