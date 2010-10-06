namespace Mike.MefAreas.Core.Services
{
    public interface INavigation
    {
        string Text { get; }
        string Action { get; }
        string Controller { get; }
    }
}