namespace EducationPortal.PresentationWebAPI.Controllers
{
    public class UsersController : BaseApiController
    {
        private IUserService _userService { get; set; }
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return result switch
            {
                SuccessResult<IEnumerable<User>> successResult => Ok(successResult.Data),
                ErrorResult<IEnumerable<User>> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var result = await _userService.GetWithFullInfoAsync(id);
            return result switch
            {
                SuccessResult<User> successResult => Ok(successResult.Data),
                ErrorResult<User> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<RegisterUserResponse>> Create(RegisterUserRequest request)
        {
            var result = await _userService.CreateAsync(request);
            return result switch
            {
                SuccessResult<RegisterUserResponse> successResult => Ok(successResult.Data),
                ErrorResult<RegisterUserResponse> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpDelete]
        public async Task<ActionResult> Remove(int id)
        {
            var result = await _userService.RemoveAsync(id);
            return result switch
            {
                SuccessResult successResult => Ok($"The user with id {id} successfully removed."),
                ErrorResult errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }
    }
}
