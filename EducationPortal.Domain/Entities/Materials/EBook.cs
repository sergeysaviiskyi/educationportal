namespace EducationPortal.Domain.Entities.Materials
{
    public class EBook : Material
    {
        //Clarification
        public string Author { get; set; }
        //public int PagesNumber { get; set; }
        public string Format { get; set; } = "fb2";
        //public int PublicationYear { get; set; } = DateTime.Now.Year;
        public EBook() { }
        public EBook(string name, string author) : base(name)
        {
            Author = author;
            Type = "EBook";
        }
    }
}
