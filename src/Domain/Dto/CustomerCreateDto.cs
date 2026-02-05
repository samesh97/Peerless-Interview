using System.ComponentModel.DataAnnotations;

namespace PeerlessInterview.src.Domain.Dto;

public class CustomerCreateDto
{
    [Required(ErrorMessage = "Customer Code is required")]
    public string CustCode { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Customer Name is required")]
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public int? Status { get; set; }
}