namespace EducationPortal.Application.Services
{
    public class CourseService : ICourseService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<Course>> GetAsync(int id)
        {
            if (await _unitOfWork.Courses.IsExisting(id))
            {
                var course = await _unitOfWork.Courses.GetAsync(id);
                return new SuccessResult<Course>(_mapper.Map<Course>(course));
            }
            var errors = new List<Error>
            {
                new Error("CourseId", "Course with such id doesn't exist.")
            };
            return new ErrorResult<Course>(errors);
        }
        public async Task<Result<IEnumerable<Course>>> GetAllAsync()
        {
            var courses = await _unitOfWork.Courses.GetAllAsync();
            return new SuccessResult<IEnumerable<Course>>(_mapper.Map<List<Course>>(courses));
        }
        public async Task<Result<Course>> GetWithFullInfoAsync(int id)
        {
            if (await _unitOfWork.Courses.IsExisting(id))
            {
                var course = await _unitOfWork.Courses.GetWithFullInfoAsync(id);
                return new SuccessResult<Course>(_mapper.Map<Course>(course));
            }
            var errors = new List<Error>
            {
                new Error("CourseId", "Course with such id doesn't exist.")
            };
            return new ErrorResult<Course>(errors);
        }
        public async Task<Result<IEnumerable<Course>>> GetAllWithFullInfoAsync()
        {
            var courses = await _unitOfWork.Courses.GetAllWithFullInfoAsync();
            return new SuccessResult<IEnumerable<Course>>(_mapper.Map<List<Course>>(courses));
        }
        public async Task<Result<Course>> CreateAsync(string name, string description)
        {
            var createdCourse = new Course(name, description);
            var courseModel = _mapper.Map<CourseModel>(createdCourse);
            await _unitOfWork.Courses.AddAsync(courseModel);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult<Course>(createdCourse);
        }
        public async Task<Result> StartNewCourseAsync(int userId, int courseId)
        {
            var user = await _unitOfWork.Users.GetWithFullInfoAsync(userId);
            if (!IsTakenByUser(user, courseId))
            {
                var course = await _unitOfWork.Courses.GetAsync(courseId);

                await _unitOfWork.UserCourses.AddAsync(new UserCourseModel { User = user, Course = course });
                await _unitOfWork.CompleteAsync();
                return new SuccessResult();
            }
            var errors = new List<Error>
            {
                new Error("CourseId", "This course is already taken by the user.")
            };
            return new ErrorResult(errors);
        }
        public async Task<Result> AddMaterialAsync(int courseId, int materialId)
        {
            var course = await _unitOfWork.Courses.GetWithFullInfoAsync(courseId);
            if (!ContainsMaterial(course, materialId))
            {
                var material = await _unitOfWork.Materials.GetAsync(materialId);
                await _unitOfWork.CourseMaterials.AddAsync(new CourseMaterialModel { Course = course, Material = material });
                course.Length += 1;
                await _unitOfWork.CompleteAsync();
                return new SuccessResult();
            }
            var errors = new List<Error>
            {
                new Error("MaterialId", "The course already contains such material.")
            };
            return new ErrorResult(errors);
        }
        public async Task<Result> AddSkillAsync(int courseId, int skillId)
        {
            var course = await _unitOfWork.Courses.GetWithFullInfoAsync(courseId);
            if (!ContainsSkill(course, skillId))
            {
                var skill = await _unitOfWork.Skills.GetAsync(skillId);
                await _unitOfWork.CourseSkills.AddAsync(new CourseSkillModel { Course = course, Skill = skill });
                await _unitOfWork.CompleteAsync();
                return new SuccessResult();
            }
            var errors = new List<Error>
            {
                new Error("SkillId", "The course already contains such skill.")
            };
            return new ErrorResult(errors);
        }
        public async Task<Result> LearnLessonAsync(int userId, int courseId)
        {
            var user = await _unitOfWork.Users.GetWithFullInfoAsync(userId);
            var errors = new List<Error>();
            if (IsTakenByUser(user, courseId))
            {
                var course = user.Courses.FirstOrDefault(c => c.CourseId == courseId);
                if (IsInProgress(user, course))
                {
                    course.LearningProgress += 1;
                    if (IsFinished(course))
                    {
                        course.Status = 1;
                        await AddLearnedSkillsAsync(user, course.Course);
                    }
                    await _unitOfWork.CompleteAsync();
                    return new SuccessResult();
                }
                errors.Add(new Error("This course is already finished."));
                return new ErrorResult(errors);
            }
            errors.Add(new Error("The user doesn't learn such course."));
            return new ErrorResult(errors);
        }
        public async Task<Result> RemoveAsync(int id)
        {
            if (await _unitOfWork.Courses.IsExisting(id))
            {
                var courseToDelete = await _unitOfWork.Courses.GetAsync(id);
                _unitOfWork.Courses.Remove(courseToDelete);
                await _unitOfWork.CompleteAsync();
                return new SuccessResult();
            }
            else
            {
                var errors = new List<Error>
                {
                new Error("CourseId", "Course with such id doesn't exist.")
                };
                return new ErrorResult(errors);
            }
        }
        private async Task AddLearnedSkillsAsync(UserModel user, CourseModel course)
        {
            var tasks = new List<Task>();
            foreach (var skill in course.CourseSkills)
            {
                if (user.LearnedSkills.Any(s => s.SkillId == skill.SkillId))
                {
                    user.LearnedSkills.Where(s => s.SkillId == skill.SkillId).FirstOrDefault().Level += 1;
                }
                else
                {
                    tasks.Add(_unitOfWork.UsersSkills.AddAsync(new UserSkillModel { User = user, Skill = skill.Skill }));
                }
            }
            await Task.WhenAll(tasks);
            await _unitOfWork.CompleteAsync();
        }
        private bool IsTakenByUser(UserModel user, int courseId)
        {
            return user.Courses.Any(c => c.CourseId == courseId) ? true : false;
        }
        private bool ContainsMaterial(CourseModel course, int materialId)
        {
            return course.CourseMaterials.Any(m => m.MaterialId == materialId) ? true : false;
        }
        private bool ContainsSkill(CourseModel course, int skillId)
        {
            return course.CourseSkills.Any(m => m.SkillId == skillId) ? true : false;
        }
        private bool IsInProgress(UserModel user, UserCourseModel course)
        {
            return course.Status == (int)CourseStates.InProgress;
        }
        private bool IsFinished(UserCourseModel course)
        {
            return course.LearningProgress == course.Course.Length;
        }
    }
}