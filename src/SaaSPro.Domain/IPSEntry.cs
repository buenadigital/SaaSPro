using System;
using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using System.Net;

namespace SaaSPro.Domain
{
    /// <summary>
    /// Represents an IPS (IP Address Security) entry.
    /// </summary>
    public class IPSEntry : AuditedEntity
    {
        public string Name { get; protected set; } 
        public byte[] StartBytes { get; protected set; }
        public byte[] EndBytes { get; protected set; }
        public Guid CustomerId { get; protected set; }
        
        public IPSEntry(Customer customer, string name, IPAddress start, IPAddress end)
        {
            Ensure.Argument.NotNull(customer, "Customer");
            Ensure.Argument.NotNullOrEmpty(name, "name");
            Ensure.Argument.NotNull(start, "start");
            Ensure.Argument.NotNull(end, "end");
            Ensure.Argument.Is(start.AddressFamily == end.AddressFamily);

            CustomerId = customer.Id;
            Name = name;
            StartBytes = start.GetAddressBytes();
            EndBytes = end.GetAddressBytes();
        }

        public IPAddress GetStartIPAddress()
        {
            return new IPAddress(StartBytes);
        }

        public IPAddress GetEndIPAddress() 
        {
            return new IPAddress(EndBytes);
        }

        public IPAddressRange GetIPAddressRange()
        {
            return new IPAddressRange(GetStartIPAddress(), GetEndIPAddress());
        }

        protected IPSEntry() { }
    }
}
