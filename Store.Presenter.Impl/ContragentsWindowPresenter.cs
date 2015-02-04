using System;
using Stimulsoft.Report;
using Store.DataAccess.Factory;
using Store.Model;
using Store.View;

namespace Store.Presenter.Impl
{
    public class ContragentsWindowPresenter : IContragentsWindowPresenter
    {
        private readonly IContragentsWindowView _contragentsWindowView;
        private readonly IDataAccessFactory _dataAccessFactory;

        //Конструктор
        public ContragentsWindowPresenter(IContragentsWindowView contragentsWindowView, IDataAccessFactory dataAccessFactory)
        {
            _contragentsWindowView = contragentsWindowView;
            _dataAccessFactory = dataAccessFactory;

            _contragentsWindowView.WindowLoaded += _contragentsWindowView_WindowLoaded;
            _contragentsWindowView.ContragentsTableMouseClicked += _contragentsWindowView_ContragentsTableMouseClicked;
            _contragentsWindowView.EditContragentClicked += _contragentsWindowView_EditContragentClicked;
            _contragentsWindowView.CopyContragentClicked += _contragentsWindowView_CopyContragentClicked;
            _contragentsWindowView.SaveContragentClicked += _contragentsWindowView_SaveContragentClicked;
            _contragentsWindowView.PrintContragentClicked += _contragentsWindowView_PrintContragentClicked;
        }

        //Печать
        public void _contragentsWindowView_PrintContragentClicked(object sender, EventArgs e)
        {
            //Получаем товар из накладной
            ContrAgents contragent = (ContrAgents) _contragentsWindowView.SelectedContragents;

            //Создаём объект rep и загружаем в него нашу модель
            StiReport rep = new StiReport();
            rep.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\ContragentPage.mrt");

            //Компилируем rep и вкладываем в него наши значения
            rep.Compile();
            rep["Id"] = contragent.Id.ToString();
            rep["shortName"] = contragent.shortName;
            rep["telephone"] = contragent.telephone;
            rep["address"] = contragent.address;
            rep["bin"] = contragent.bin;

            //Отображаем
            rep.ShowWithWpf();
        }

        //Сохранить
        public void _contragentsWindowView_SaveContragentClicked(object sender, EventArgs e)
        {
            try
            {
                //Если поля заполнены
                if (_contragentsWindowView.StringOfName != "" &&
                    _contragentsWindowView.StringOfTel != "" &&
                    _contragentsWindowView.StringOfAddress != "" &&
                    _contragentsWindowView.StringOfBin != "")
                {

                    //Если ID уже есть - пересохраняем редактируемого контрагента
                    if (_contragentsWindowView.StringOfId != "")
                    {
                        UpdateContragent();
                    }
                    //Иначе - создаём нового
                    else
                    {
                        CreateContragent();
                    }
                }
                //Если поля пустые - вывести предупреждение
                else
                {
                    _contragentsWindowView.ShowError("Заполните все доступные поля!");
                }
            }
            catch (Exception ex) { _contragentsWindowView.ShowError(ex.Message); }
        }

        private void CreateContragent()
        {
            var contrAgentsDb = _dataAccessFactory.CreateContragentsDbAccess();
            contrAgentsDb.AddNewContragent(_contragentsWindowView.StringOfName,
                _contragentsWindowView.StringOfAddress,
                _contragentsWindowView.StringOfTel,
                _contragentsWindowView.StringOfBin);

            _contragentsWindowView.ShowInformMessage("Контрагент успешно создан!");

            RefreshWindow();
        }

        private void UpdateContragent()
        {
            var contrAgentsDb = _dataAccessFactory.CreateContragentsDbAccess();
            contrAgentsDb.UpdateExistingContragent(_contragentsWindowView.StringOfId,
                _contragentsWindowView.StringOfName, _contragentsWindowView.StringOfAddress,
                _contragentsWindowView.StringOfTel, _contragentsWindowView.StringOfBin);

            _contragentsWindowView.ShowInformMessage("Контрагент успешно обновлён!");

            RefreshWindow();
        }

        //Метод обновления окна
        private void RefreshWindow()
        {
            var contrAgentsDb = _dataAccessFactory.CreateContragentsDbAccess();

            _contragentsWindowView.StringOfId = "";
            _contragentsWindowView.StringOfName = "";
            _contragentsWindowView.StringOfTel = "";
            _contragentsWindowView.StringOfAddress = "";
            _contragentsWindowView.StringOfBin = "";

            _contragentsWindowView.ListOfcontragents = null;
            _contragentsWindowView.ListOfcontragents = contrAgentsDb.GetAllContrAgents();

            _contragentsWindowView.IsEditEnabled = false;
            _contragentsWindowView.IsCopyEnabled = false;
            _contragentsWindowView.IsPrintEnabled = false;
        }

        //Копировать
        public void _contragentsWindowView_CopyContragentClicked(object sender, EventArgs e)
        {
            //Сбрасываем значения
            ResetValues();

            //Устанавливаем значения с копируемой накладной, кроме Id
            ContrAgents contragent = (ContrAgents)_contragentsWindowView.SelectedContragents;
            _contragentsWindowView.StringOfName = contragent.shortName;
            _contragentsWindowView.StringOfTel = contragent.telephone;
            _contragentsWindowView.StringOfAddress = contragent.address;
            _contragentsWindowView.StringOfBin = contragent.bin;
        }

        private void ResetValues()
        {
            _contragentsWindowView.StringOfId = "";
            _contragentsWindowView.StringOfName = "";
            _contragentsWindowView.StringOfTel = "";
            _contragentsWindowView.StringOfAddress = "";
            _contragentsWindowView.StringOfBin = "";
        }

        //Редактировать
        public void _contragentsWindowView_EditContragentClicked(object sender, EventArgs e)
        {
            //Сбрасываем значения
            ResetValues();

            //Устанавливаем значения с редактируемой накладной
            ContrAgents contragent = (ContrAgents)_contragentsWindowView.SelectedContragents;
            _contragentsWindowView.StringOfId = contragent.Id.ToString();
            _contragentsWindowView.StringOfName = contragent.shortName;
            _contragentsWindowView.StringOfTel = contragent.telephone;
            _contragentsWindowView.StringOfAddress = contragent.address;
            _contragentsWindowView.StringOfBin = contragent.bin;
        }

        //Щелчёк по таблице контрагентов
        public void _contragentsWindowView_ContragentsTableMouseClicked(object sender, EventArgs e)
        {
            if (_contragentsWindowView.SelectedContragents != null)
            {
                EnableComands();
            }
            else
            {
                DisableComands();
            }
        }

        private void DisableComands()
        {
            _contragentsWindowView.IsEditEnabled = false;
            _contragentsWindowView.IsCopyEnabled = false;
            _contragentsWindowView.IsPrintEnabled = false;
        }

        private void EnableComands()
        {
            _contragentsWindowView.IsEditEnabled = true;
            _contragentsWindowView.IsCopyEnabled = true;
            _contragentsWindowView.IsPrintEnabled = true;
        }

        //Окно загружается
        public void _contragentsWindowView_WindowLoaded(object sender, EventArgs e)
        {
            try
            {
                var contrAgentsDb = _dataAccessFactory.CreateContragentsDbAccess();
                _contragentsWindowView.ListOfcontragents = contrAgentsDb.GetAllContrAgents();
            }
            catch (Exception ex) { _contragentsWindowView.ShowError(ex.Message); }
        }

        //Запуск
        public void Run()
        {
            _contragentsWindowView.Show();
        }
    }
}
