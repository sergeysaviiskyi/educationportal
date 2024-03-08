namespace EducationPortal.Application.Interfaces
{
    public interface IMessageProducer
    {
        public void PublishMassage<T>(T message);
    }
}