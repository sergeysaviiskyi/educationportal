namespace EducationPortal.Infrastructure.BackgroundJobs
{
    public class DatabaseCleaningBackgroundJob : IJob
    {
        private readonly ILogger<DatabaseCleaningBackgroundJob> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public DatabaseCleaningBackgroundJob(ILogger<DatabaseCleaningBackgroundJob> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _unitOfWork.VerificationCode.RemoveNotValid();
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Database cleaning iteration completed at: {Now}", DateTime.Now);
        }
    }
}