using BaseSource.Model;

namespace BaseSource.Dto
{
    public class CategoryResponse:BaseEntity
    {
        public string Name { get; set; }

        public string Slug { get; set; }
        
    }
}
