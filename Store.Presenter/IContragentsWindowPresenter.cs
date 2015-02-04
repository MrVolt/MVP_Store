using System;

namespace Store.Presenter
{
    public interface IContragentsWindowPresenter:IPresenter
    {
        void _contragentsWindowView_PrintContragentClicked(object sender, EventArgs e);

        void _contragentsWindowView_SaveContragentClicked(object sender, EventArgs e);

        void _contragentsWindowView_CopyContragentClicked(object sender, EventArgs e);

        void _contragentsWindowView_EditContragentClicked(object sender, EventArgs e);

        void _contragentsWindowView_ContragentsTableMouseClicked(object sender, EventArgs e);

        void _contragentsWindowView_WindowLoaded(object sender, EventArgs e);
    }
}
