namespace EducationPortal.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }
        public MaterialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<Material>> GetAsync(int id)
        {
            if (await _unitOfWork.Materials.IsExisting(id))
            {
                var material = await _unitOfWork.Materials.GetAsync(id);
                return new SuccessResult<Material>(_mapper.Map<Material>(material));
            }
            var errors = new List<Error>
            {
                new Error("MaterialId", "Material with such id doesn't exist.")
            };
            return new ErrorResult<Material>(errors);
        }
        public async Task<Result<IEnumerable<Material>>> GetAllAsync()
        {
            var materials = await _unitOfWork.Materials.GetAllAsync();
            return new SuccessResult<IEnumerable<Material>>(_mapper.Map<List<Material>>(materials));
        }
        public async Task<Result<Video>> CreateVideoAsync(string name, int length)
        {
            var createdVideo = new Video(name, length);
            var videoModel = _mapper.Map<VideoModel>(createdVideo);
            await _unitOfWork.Materials.AddAsync(videoModel);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult<Video>(createdVideo);
        }
        public async Task<Result<EBook>> CreateEBookAsync(string name, string author)
        {
            var createdEBook = new EBook(name, author);
            var eBookModel = _mapper.Map<EBookModel>(createdEBook);
            await _unitOfWork.Materials.AddAsync(eBookModel);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult<EBook>(createdEBook);
        }
        public async Task<Result<Article>> CreateArticleAsync(string name)
        {
            var createdArticle = new Article(name);
            var articleModel = _mapper.Map<ArticleModel>(createdArticle);
            await _unitOfWork.Materials.AddAsync(articleModel);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult<Article>(createdArticle);
        }
        public async Task<Result> RemoveAsync(int id)
        {
            if (await _unitOfWork.Materials.IsExisting(id))
            {
                var materialToDelete = await _unitOfWork.Materials.GetAsync(id);
                _unitOfWork.Materials.Remove(materialToDelete);
                await _unitOfWork.CompleteAsync();
                return new SuccessResult();
            }
            else
            {
                var errors = new List<Error>
                {
                new Error("MaterialId", "Material with such id doesn't exist.")
                };
                return new ErrorResult(errors);
            }

        }
    }
}