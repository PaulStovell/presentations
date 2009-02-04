using Demo1;

namespace Demo1
{
    public interface IContactSearchService
    {
        Contact[] FindByName(string firstName);
        Contact[] FindByEmailAddress(string emailAddress);
    }
}