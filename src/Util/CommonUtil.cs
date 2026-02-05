using PeerlessInterview.src.Domain.Enums;

namespace PeerlessInterview.src.Util;

public class CommonUtil
{
    public static CustomerStatus? GetStatusInstance(int status)
    {
        if (Enum.IsDefined(typeof(CustomerStatus), status))
        {
            return (CustomerStatus)status;
        }
        return null;
    }
}