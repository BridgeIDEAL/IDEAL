public class EventData
{
    public bool isOccurEvent = false;
    public bool isDoneEvent = false;
    public string eventName;

    public EventData(bool _done, string _name)
    {
        isDoneEvent = _done;
        eventName = _name;
    }

    public void ResetData() { isDoneEvent = false; }
}