namespace SaaSPro.Web
{
    public interface ICustomerStartupTask
    {
        void Execute(CustomerInstance customer);
    }
}