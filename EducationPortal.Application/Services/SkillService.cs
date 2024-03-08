namespace EducationPortal.Application.Services
{
    public class SkillService : ISkillService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }
        public SkillService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<Skill>> GetAsync(int id)
        {
            if (await _unitOfWork.Skills.IsExisting(id))
            {
                var skill = await _unitOfWork.Skills.GetAsync(id);
                return new SuccessResult<Skill>(_mapper.Map<Skill>(skill));
            }
            var errors = new List<Error>
            {
                new Error("SkillId", "Skill with such id doesn't exist.")
            };
            return new ErrorResult<Skill>(errors);
        }
        public async Task<Result<IEnumerable<Skill>>> GetAllAsync()
        {
            var skills = await _unitOfWork.Skills.GetAllAsync();
            return new SuccessResult<IEnumerable<Skill>>(_mapper.Map<List<Skill>>(skills));
        }
        public async Task<Result<Skill>> CreateAsync(string name)
        {
            var createdSkill = new Skill(name);
            var skillModel = _mapper.Map<SkillModel>(createdSkill);
            await _unitOfWork.Skills.AddAsync(skillModel);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult<Skill>(createdSkill);
        }
        public async Task<Result> RemoveAsync(int id)
        {
            if (await _unitOfWork.Skills.IsExisting(id))
            {
                var skillToDelete = await _unitOfWork.Skills.GetAsync(id);
                _unitOfWork.Skills.Remove(skillToDelete);
                await _unitOfWork.CompleteAsync();
                return new SuccessResult();
            }
            else
            {
                var errors = new List<Error>
                {
                new Error("SkillId", "Skill with such id doesn't exist.")
                };
                return new ErrorResult(errors);
            }
        }
    }
}
