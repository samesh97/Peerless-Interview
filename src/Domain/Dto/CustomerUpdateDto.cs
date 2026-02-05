
namespace PeerlessInterview.src.Domain.Dto;

public class CustomerUpdateDto
{
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public int? Status { get; set; }
}