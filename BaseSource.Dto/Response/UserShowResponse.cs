
using BaseSource.Model;

namespace BaseSource.Dto
{
    public class UserShowResponse
    {
        public UserDetailResponse User { get; set; }
        public IList<string> Roles { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public List<Order>? Orders { get; set; }

    }
}
