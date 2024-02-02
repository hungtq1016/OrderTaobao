using Core;

namespace NotificationService.Models
{
    public class Notification : Entity
    {
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
    }
}
