
namespace RezhCity.WEB.Interfaces
{
    public interface ISearchable
    {
        string Keyword { get; set; }
        string Category { get; set; } //category for search (ex.: Auto, Appliances ...)
    }
}
