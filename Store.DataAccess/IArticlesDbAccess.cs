using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Store.DataAccess
{
    public interface IArticlesDbAccess
    {
        List<string> GetListOfMakers();

        void AddArticle(string nameOfMaker, string nameOfArticle, int amountAll, int price, int amountBusy = 0, int amountFree = 0);

        void DeleteArticle(string nameOfMaker, string nameOfArticle);

        void ArticleInReserv(int articleId, int articleAmount);

        void ArrivalOfTheArticle(int articleId, int articleAmount);

        void ArticleInReservInvert(int articleId, int articleAmount);

        void ArrivalOfTheArticleInvert(int articleId, int articleAmount);

        ObservableCollection<Model.Articles> GetAllArticles();

        Model.Articles FindArticleById(int articleId);

        List<Model.Articles> GetArticlesByMaker(string maker);

        void GrantTheArrival(int id, int amount);

        void DebitItems(int id, int amount);

        List<Model.Articles> FindArticlesByRequestString(string searchText);
    }
}
