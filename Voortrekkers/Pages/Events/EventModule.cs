using DataBaseModles;
public class EventModel
{
    
    
    public string IdEvent { get; set; }
    
    public EventType eventType { get; set; }
    
    public string EventName { get; set; }
    
    public string EventDiscription { get; set; }
    
    public DateTime EventDate { get; set; }
    
    public string idOrganization { get; set; }
    
    public string RedirectUri { get; set; }
}