using System.Net.NetworkInformation;

namespace Voortrekkers.Pages.Email;

public class EmailModel
{
    public string Subject { get; set; }
    public string Message { get; set; }
    public bool Staatmaker { get; set; }
}