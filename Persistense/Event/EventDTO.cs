namespace Persistense.Event;

public class EventDTO
{
    public Guid Id { get; set; }
    public DateTime HappenDate { get; set; }
    public int AggregateType { get; set; }
    public string Data { get; set; }
    public Guid EntityId { get; set; }
}