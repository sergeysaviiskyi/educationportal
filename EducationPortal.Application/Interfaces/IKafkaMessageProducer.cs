namespace EducationPortal.Application.Interfaces
{
    public interface IKafkaMessageProducer
    {
        public Task PublishMassageAsync<T>(T message);
    }
}
