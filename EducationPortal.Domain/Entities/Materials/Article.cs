namespace EducationPortal.Domain.Entities.Materials
{
    public class Article : Material
    {
        public string PublicationDate { get; set; } = DateTime.Now.ToString("dd - MM - yyyy");
        public string Resource { get; set; } = "metanit.com";
        public Article() { }
        public Article(string name) : base(name)
        {
            Type = "Article";
        }
    }
}
