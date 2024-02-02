using Core;

namespace HistoryService.Models
{
    public class UserHistory : Entity
    {
        public string OldValue { get; set; } = string.Empty;

        public string NewValue { get; set; } = string.Empty;

        public string Field { get; set; } = string.Empty;
    }
}
