
namespace BaseSource.Dto
{
    public class UserDetailResponse : UserResponse
    {
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool Enable { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
