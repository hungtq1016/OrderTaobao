
namespace BaseSource.Model
{
    public  class Image : BaseEntity
    {
        public string Lable { get; set; }
        public string Url { get; set; }
        public string Field { get; set; }
        public string Size { get; set; }
        public List<ImageUser> Users { get; } = new();
    }
}
