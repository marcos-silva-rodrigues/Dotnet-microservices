namespace ItemService.EventProcessor
{
    public interface IEventProcessor
    {
        void ProcessMessage(string message);
    }
}
