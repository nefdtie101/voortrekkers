namespace DataBaseModles;

public class RoleModel
{
    public string IdRole { get; set; }
    
    public string RoleName { get; set;}
    
    public List<string> AllowedUrls { get; set; }
}