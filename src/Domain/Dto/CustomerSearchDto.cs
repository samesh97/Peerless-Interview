namespace PeerlessInterview.src.Domain.Dto;

public class CustomerSearchDto
{
    public string? CustCode { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public int? Status { get; set; }

    public DateTime? CreatedDateFrom { get; set; }
    public DateTime? CreatedDateTo { get; set; }

    public Boolean hasAnyFilter()
    {
        return !string.IsNullOrWhiteSpace(CustCode) 
            || !string.IsNullOrWhiteSpace(Name) 
            || !string.IsNullOrWhiteSpace(City) 
            || !string.IsNullOrWhiteSpace(State) 
            || !string.IsNullOrWhiteSpace(Country) 
            || Status != null
            || CreatedDateFrom != null
            || CreatedDateTo != null;
    }
}