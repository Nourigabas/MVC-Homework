namespace Homework_TV_MVC.Repositories
{
    public interface ISessionRepository
    {
        public void SetValue(string key, string value);
        public string GetValue(string key);
        public void Remove(string key);


    }
}
