namespace WebServiceConsumer.Services
{
    public interface ICreateOrderService
    {
        void CreateOrder(int fromId, int toId, string content);
    }
}
