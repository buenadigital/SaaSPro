using System.Net;
using System.Net.Sockets;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Common
{
    public class IPAddressRange
    {
        private readonly AddressFamily addressFamily;
        private readonly byte[] lowerBytes;
        private readonly byte[] upperBytes;
       
        public IPAddressRange(IPAddress lower, IPAddress upper)
        {
            Ensure.Argument.NotNull(lower, "lower");
            Ensure.Argument.NotNull(upper, "upper");
            Ensure.Argument.Is(lower.AddressFamily == upper.AddressFamily);
            
            this.addressFamily = lower.AddressFamily;
            this.lowerBytes = lower.GetAddressBytes();
            this.upperBytes = upper.GetAddressBytes();
        }

        public bool Contains(IPAddress address)
        {
            Ensure.Argument.NotNull(address, "address");
            
            if (address.AddressFamily != addressFamily)
            {
                return false;
            }

            byte[] addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, 
                 upperBoundary = true;

            for (int i = 0; i < this.lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == upperBytes[i]);
            }

            return true;
        }
    }
}