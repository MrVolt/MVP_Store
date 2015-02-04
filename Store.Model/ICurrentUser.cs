namespace Store.Model
{
    public interface ICurrentUser
    {
        AuthorizedUser AuthorizedUser { get; set; }
        CurrentUser Instance { get; }
    }
}
