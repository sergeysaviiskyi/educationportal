using EducationPortal.Application.Common.Validation;

namespace EducationPortal.PresentationWebAPI.Controllers
{
    public class MaterialsController : BaseApiController
    {
        private IMaterialService _materialService { get; set; }
        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetAll()
        {
            var result = await _materialService.GetAllAsync();
            return result switch
            {
                SuccessResult<IEnumerable<Material>> successResult => Ok(successResult.Data),
                ErrorResult<IEnumerable<Material>> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetById(int id)
        {
            var result = await _materialService.GetAsync(id);
            return result switch
            {
                SuccessResult<Material> successResult => Ok(successResult.Data),
                ErrorResult<Material> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPost("article")]
        public async Task<ActionResult<Article>> CreateArticle(CreateArticleRequest request)
        {
            var result = await _materialService.CreateArticleAsync(request.Name);
            return result switch
            {
                SuccessResult<Article> successResult => Ok(successResult.Data),
                ErrorResult<Article> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPost("ebook")]
        public async Task<ActionResult<EBook>> CreateEbook(CreateEbookRequest request)
        {
            var result = await _materialService.CreateEBookAsync(request.Name, request.Author);
            return result switch
            {
                SuccessResult<EBook> successResult => Ok(successResult.Data),
                ErrorResult<EBook> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPost("video")]
        public async Task<ActionResult<Video>> CreateVideo(CreateVideoRequest request)
        {
            var result = await _materialService.CreateVideoAsync(request.Name, request.Length);
            return result switch
            {
                SuccessResult<Video> successResult => Ok(successResult.Data),
                ErrorResult<Video> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpDelete]
        public async Task<ActionResult> Remove(int id)
        {
            var result = await _materialService.RemoveAsync(id);
            return result switch
            {
                SuccessResult successResult => Ok($"The material with id {id} successfully removed."),
                ErrorResult errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }
    }
}