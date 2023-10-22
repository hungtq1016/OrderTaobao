
using BaseSource.Model;

namespace BaseSource.Dto
{
    public class UserDetailResponse
    {
        public UserResponse User { get; set; }
        public IList<string> Roles { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
