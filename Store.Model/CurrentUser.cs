namespace Store.Model
{
    public class CurrentUser:ICurrentUser
    {
        private static CurrentUser _instance;

        private CurrentUser(AuthorizedUser user)
        {
            AuthorizedUser = user;
        }

        public static AuthorizedUser AuthorizedUser { get; set; }

        CurrentUser ICurrentUser.Instance
        {
            get { return Instance; }
        }

        AuthorizedUser ICurrentUser.AuthorizedUser
        {
            get { return AuthorizedUser; }
            set { AuthorizedUser = value; }
        }

        public static CurrentUser Instance
        {
            get { return _instance ?? (_instance = new CurrentUser(AuthorizedUser)); }
        }
    }
}