namespace DataBaseModles;

public enum EventType
{
    InterneAksie = 0,
    GebiedAksie = 1,
    SpesiealeKasie = 3,
    Kampe  = 4 ,
    Komitee = 5
}



public class EventDropdownType
{
    public string Name { get; set; }
    public int Value { get; set; }
} 