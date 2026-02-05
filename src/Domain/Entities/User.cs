using System.ComponentModel.DataAnnotations;

namespace PeerlessInterview.src.Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
}