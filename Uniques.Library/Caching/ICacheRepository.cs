namespace Uniques.Library.Caching
{
    public interface ICacheRepository
    {
        object this[string name] { get; set; }

        void Delete(string name);
    }
}
