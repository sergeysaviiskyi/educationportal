using EducationPortal.Application.Common.Validation;

namespace EducationPortal.PresentationWebAPI.Controllers
{
    public class SkillsController : BaseApiController
    {
        private ISkillService _skillService { get; set; }
        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var result = await _skillService.GetAllAsync();
            return result switch
            {
                SuccessResult<IEnumerable<Skill>> successResult => Ok(successResult.Data),
                ErrorResult<IEnumerable<Skill>> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetById(int id)
        {
            var result = await _skillService.GetAsync(id);
            return result switch
            {
                SuccessResult<Skill> successResult => Ok(successResult.Data),
                ErrorResult<Skill> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPost]
        public async Task<ActionResult<Skill>> Create(CreateSkillRequest request)
        {
            var result = await _skillService.CreateAsync(request.Name);
            return result switch
            {
                SuccessResult<Skill> successResult => Ok(successResult.Data),
                ErrorResult<Skill> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpDelete]
        public async Task<ActionResult> Remove(int id)
        {
            var result = await _skillService.RemoveAsync(id);
            return result switch
            {
                SuccessResult successResult => Ok($"The skill with id {id} successfully removed."),
                ErrorResult errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }
    }
}
