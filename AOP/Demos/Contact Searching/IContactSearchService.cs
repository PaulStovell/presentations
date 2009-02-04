namespace Demo0
{
    public interface IContactSearchService
    {
        Contact[] FindByName(string firstName);
        Contact[] FindByEmailAddress(string emailAddress);
    }
}