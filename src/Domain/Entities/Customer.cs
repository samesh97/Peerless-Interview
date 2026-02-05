using System.ComponentModel.DataAnnotations;
using PeerlessInterview.src.Domain.Enums;

namespace PeerlessInterview.src.Domain.Entities;
public class Customer
{
    [Required]
    [Key]
    public string CustCode { get; set; }
    [Required]
    public string Name { get; set; }
    public string? ShortName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public CustomerStatus? Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid? ModifiedBy { get; set; }
}