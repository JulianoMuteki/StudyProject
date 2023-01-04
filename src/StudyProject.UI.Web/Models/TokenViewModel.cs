using System;

namespace CategoriasMvc.Models;

public class TokenViewModel
{
    public bool Authenticated { get; set; }
    public DateTime Expiration { get; set; }
    public string? Token { get; set; }
    public string? Message { get; set; }
}
