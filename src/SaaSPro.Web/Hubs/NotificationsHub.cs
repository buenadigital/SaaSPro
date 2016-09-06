using SaaSPro.Domain;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using SaaSPro.Data;

namespace SaaSPro.Web.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly Func<CustomerInstance> _customerAccessor;
        private readonly Func<EFDbContext> _sessionFactory;
        
        public NotificationsHub(Func<CustomerInstance> customerAccessor, Func<EFDbContext> sessionFactory)
        {
            _customerAccessor = customerAccessor;
            _sessionFactory = sessionFactory;
        }

        public override Task OnConnected()
        {
            JoinGroups();
            return base.OnConnected();
        }

        private Task JoinGroups()
        {
            var customer = _customerAccessor();
            var currentUser = _sessionFactory().Set<User>().FirstOrDefault(u => u.Email == Context.User.Identity.Name && u.CustomerId == customer.CustomerId);

            if (currentUser != null)
            {
                // create a group for each user
                Groups.Add(Context.ConnectionId, currentUser.Id.ToString());
            }

            // add user to Customer specific group
            return Groups.Add(Context.ConnectionId, customer.CustomerId.ToString());
        }
    }
}