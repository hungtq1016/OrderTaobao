namespace ImageService.Data
{
    public class AudioContextFactory : AppDbContextFactory<AudioContext>
    {
        public AudioContextFactory() : base("audioDB")
        {
        }
    }
}
