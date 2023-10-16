
using BaseSource.Model;

namespace BaseSource.Dto
{
    public class UserResponse : Response
    {
        public UserResponse()
        {
            User = new HashSet<User>();
        }
        public virtual ICollection<User> User { get; set; }   

    }
}
