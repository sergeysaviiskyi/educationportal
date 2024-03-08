namespace EducationPortal.Application.Interfaces.Services
{
    public interface IMaterialService
    {
        public Task<Result<Material>> GetAsync(int id);
        public Task<Result<IEnumerable<Material>>> GetAllAsync();
        public Task<Result<Video>> CreateVideoAsync(string name, int length);
        public Task<Result<EBook>> CreateEBookAsync(string name, string author);
        public Task<Result<Article>> CreateArticleAsync(string name);
        public Task<Result> RemoveAsync(int id);
    }
}
