namespace EducationPortal.PresentationWebAPI.Controllers
{
    public class CoursesController : BaseApiController
    {
        private ICourseService _courseService { get; set; }
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
        {
            var result = await _courseService.GetAllWithFullInfoAsync();
            return result switch
            {
                SuccessResult<IEnumerable<Course>> successResult => Ok(successResult.Data),
                ErrorResult<IEnumerable<Course>> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            var result = await _courseService.GetWithFullInfoAsync(id);
            return result switch
            {
                SuccessResult<Course> successResult => Ok(successResult.Data),
                ErrorResult<Course> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPost()]
        public async Task<ActionResult<Course>> Create(CreateCourseRequest request)
        {
            var result = await _courseService.CreateAsync(request.Name, request.Description);
            return result switch
            {
                SuccessResult<Course> successResult => Ok(successResult.Data),
                ErrorResult<Course> errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPatch("start-new-course")]
        public async Task<ActionResult> StartNewCourse(StartNewCourseRequest request)
        {
            var result = await _courseService.StartNewCourseAsync(request.UserId, request.CourseId);
            return result switch
            {
                SuccessResult successResult => Ok(),
                ErrorResult errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPatch("add-material")]
        public async Task<ActionResult> AddMaterial(AddMaterialRequest request)
        {
            var result = await _courseService.AddMaterialAsync(request.CourseId, request.MaterialId);
            return result switch
            {
                SuccessResult successResult => Ok(),
                ErrorResult errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPatch("add-skill")]
        public async Task<ActionResult> AddSkill(AddSkillRequest request)
        {
            var result = await _courseService.AddSkillAsync(request.CourseId, request.SkillId);
            return result switch
            {
                SuccessResult successResult => Ok(),
                ErrorResult errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpPatch("learn-lesson")]
        public async Task<ActionResult> LearnLesson(LearnLessonRequest request)
        {
            var result = await _courseService.LearnLessonAsync(request.UserId, request.CourseId);
            return result switch
            {
                SuccessResult successResult => Ok(),
                ErrorResult errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }


        [HttpDelete()]
        public async Task<ActionResult> Remove(int id)
        {
            var result = await _courseService.RemoveAsync(id);
            return result switch
            {
                SuccessResult successResult => Ok($"The course with id {id} successfully removed."),
                ErrorResult errorResult => BadRequest(errorResult.Errors.ErrorsToProblemDetails()),
                _ => new StatusCodeResult(500)
            };
        }
    }
}