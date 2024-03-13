namespace Core
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public abstract class Entity: IEntity
    {
        public Guid Id { get; set; } 

        public bool Enable { get; set; } 

        public DateTime CreatedAt { get; set; } 

        public DateTime UpdatedAt { get; set; } 
    }

    public abstract class EntityRequest
    {
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

    }

    public class AbstractFile : Entity
    {
        public string Title { get; set; }

        public string? Alt { get; set; }

        public long Size { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }
    }
}