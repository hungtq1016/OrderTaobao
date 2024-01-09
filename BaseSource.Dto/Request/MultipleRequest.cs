
namespace BaseSource.Dto.Request
{
    public class MultipleRequest
    {
        public List<string> Ids { get; set; } = new List<string>();

        public bool Enable { get; set; }
    }
}
