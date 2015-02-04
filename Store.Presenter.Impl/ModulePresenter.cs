using Store.Presenter.FactoryOfPtresenter;
using Store.View;

namespace Store.Presenter.Impl
{
    public class ModulePresenter : IModulePresenter
    {
        private readonly IModuleView _moduleView;
        private readonly IPresenterFactory _factory;

        //Конструктор
        public ModulePresenter(IModuleView moduleView, IPresenterFactory factory)
        {
            _moduleView = moduleView;
            _factory = factory;
            _moduleView.StoreStart += delegate { Store(); };
            _moduleView.CalenderStart += delegate { Calender(); };
        }

        //Обработка события запуска Календаря
        public void Calender()
        {
            _moduleView.Hide();
            var calenderPresenter = _factory.CreateCalenderPresenter();
            calenderPresenter.Run();
        }

        //Обраотка события запуска Склада
        public void Store()
        {
            _moduleView.Hide();
            var storePresenter = _factory.CreateStorePresenter();
            storePresenter.Run();
        }

        //Запуск отображения
        public void Run()
        {
            _moduleView.Show();
        }
    }
}
