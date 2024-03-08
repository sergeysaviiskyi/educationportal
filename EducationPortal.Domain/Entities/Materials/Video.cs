namespace EducationPortal.Domain.Entities.Materials
{
    public class Video : Material
    {
        public int Length { get; set; }
        public string Quality { get; set; } = "1080p";
        public Video() { }
        public Video(string name) : base(name) { }
        public Video(string name, int length) : base(name)
        {
            Length = length;
            Type = "Video";
        }
    }
}
