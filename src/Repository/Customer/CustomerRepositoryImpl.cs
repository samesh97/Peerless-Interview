namespace PeerlessInterview.src.Repository.Customer;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PeerlessInterview.src.Api.Exception;
using PeerlessInterview.src.Data;
using PeerlessInterview.src.Domain.Dto;
using PeerlessInterview.src.Domain.Entities;
using PeerlessInterview.src.Domain.Enums;
using PeerlessInterview.src.Util;

public class ICustomerRepositoryImpl : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ICustomerRepositoryImpl(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Customer CreateCustomer(CustomerCreateDto dto)
    {
        var existingCustomer = GetCustomerByCode(dto.CustCode);
        if(existingCustomer != null)
        {
            throw new AlreadyExistsException("Customer with the same code already exists.");
        }
    
        var customer = new Customer()
        {
            CustCode = dto.CustCode,
            Name = dto.Name,
            ShortName = dto.ShortName,
            City = dto.City,
            State = dto.State,
            Country = dto.Country,
            Status = CommonUtil.GetStatusInstance(dto.Status.GetValueOrDefault()),
            CreatedDate = DateTime.UtcNow,
            CreatedBy = Guid.NewGuid()

        };

        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();
        return customer;
    }

    public Customer? GetCustomerByCode(string custCode)
    {
        return _dbContext.Customers.FirstOrDefault(obj => obj.CustCode == custCode);
    }

    public List<Customer> GetCustomersByFilter(CustomerSearchDto filter)
    {
        var query = CreateFilter(filter);
        return query.ToList();
    }

    public Customer UpdateCustomer(CustomerUpdateDto dto, string customerCode)
    {
        var existingCustomer = GetCustomerByCode(customerCode);
        
        if (existingCustomer == null)
        {
            throw new NotFoundException("Customer not found.");
        }

        if(!string.IsNullOrWhiteSpace(dto.Name))
        {
            existingCustomer.Name = dto.Name;
        }
        if(!string.IsNullOrWhiteSpace(dto.ShortName))
        {
            existingCustomer.ShortName = dto.ShortName;
        }
        if(!string.IsNullOrWhiteSpace(dto.City))
        {
            existingCustomer.City = dto.City;   
        }

        if(!string.IsNullOrWhiteSpace(dto.State))
        {
            existingCustomer.State = dto.State;   
        }

        if(!string.IsNullOrWhiteSpace(dto.Country))
        {
            existingCustomer.Country = dto.Country;   
        }

        if(dto.Status != null)
        {
            existingCustomer.Status = CommonUtil.GetStatusInstance(dto.Status.GetValueOrDefault());
        }

        existingCustomer.ModifiedBy = Guid.NewGuid();
        existingCustomer.ModifiedDate = DateTime.UtcNow;


        _dbContext.Customers.Update(existingCustomer);
        _dbContext.SaveChanges();
        return existingCustomer;
    }

    private IQueryable<Customer> CreateFilter(CustomerSearchDto filter)
    {

        IQueryable<Customer> query = _dbContext.Customers;

        if(!string.IsNullOrWhiteSpace(filter.CustCode))
        {
            query = query.Where(obj => obj.CustCode == filter.CustCode);
        }

        if(!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(obj => obj.Name != null && obj.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if(!string.IsNullOrWhiteSpace(filter.City))
        {
            query = query.Where(obj => obj.City == filter.City);
        }

        if(!string.IsNullOrWhiteSpace(filter.State))
        {
            query = query.Where(obj => obj.State == filter.State);
        }

        if(!string.IsNullOrWhiteSpace(filter.Country))
        {
            query = query.Where(obj => obj.Country == filter.Country);
        }

        if(filter.Status != null)
        {
            var status = CommonUtil.GetStatusInstance(filter.Status.GetValueOrDefault());
            if(status == null)
            {
                throw new ValidationException("Invalid status value.");
            }
            query = query.Where(obj => obj.Status == status);
        }

        if(filter.CreatedDateFrom != null)
        {
            query = query.Where(obj => obj.CreatedDate >= filter.CreatedDateFrom);
        }

        if(filter.CreatedDateTo != null)
        {
            query = query.Where(obj => obj.CreatedDate <= filter.CreatedDateTo);
        }

        return query;

    }
}