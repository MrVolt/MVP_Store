using System;

namespace Store.Presenter
{
    public interface ISearchWindowPresenter: IPresenter
    {
        void _searchWindowView_SearchClicked(object sender, EventArgs e);
    }
}
