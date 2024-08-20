namespace Homework_TV_MVC.Repositories
{
    public class SessionRepository:ISessionRepository
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public SessionRepository(IHttpContextAccessor HttpContextAccessor)
        {
            this.HttpContextAccessor = HttpContextAccessor;
        }
        public void SetValue(string key,string value)
        {
            HttpContextAccessor?.HttpContext?.Session?.SetString(key, value);
        }
        public string GetValue(string key)
        {
            return HttpContextAccessor?.HttpContext?.Session?.GetString(key)??string.Empty;

        }
        public void Remove(string key)
        {
            HttpContextAccessor?.HttpContext?.Session?.Remove(key);
        }
    }
}
