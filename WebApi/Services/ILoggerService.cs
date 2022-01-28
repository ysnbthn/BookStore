namespace WebApi.Services
{
    public interface ILoggerService
    {
        // servicede tek bir yerden yönetilebilirlik sağlıyor
        public void Write(string message);
    }
}