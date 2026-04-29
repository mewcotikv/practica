using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using REDConstruct.Models;
using REDConstruct.Services;

namespace REDConstruct.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _execute(parameter);
    }

    public class RapoarteViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService;
        private DateTime _dataSfarsit;
        private DateTime _dataInceput;
        private string _obiectivSelectat;
        private ObservableCollection<RaportConsum> _raportData;
        private ObservableCollection<string> _obiective;

        public event PropertyChangedEventHandler PropertyChanged;

        public RapoarteViewModel()
        {
            _dataService = new DataService();

            // Initialize default dates (current month)
            _dataInceput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _dataSfarsit = DateTime.Now;

            _raportData = new ObservableCollection<RaportConsum>();
            _obiective = new ObservableCollection<string> { "Toate" };

            // Load objectives
            foreach (var obj in _dataService.GetObiective())
            {
                _obiective.Add(obj.Nume);
            }

            _obiectivSelectat = "Toate";

            // Commands
            FiltreazaCommand = new RelayCommand(_ => Filtreaza());
            ExporterExcelCommand = new RelayCommand(_ => ExportarExcel());

            // Load initial data
            Filtreaza();
        }

        public DateTime DataInceput
        {
            get => _dataInceput;
            set
            {
                if (_dataInceput != value)
                {
                    _dataInceput = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DataSfarsit
        {
            get => _dataSfarsit;
            set
            {
                if (_dataSfarsit != value)
                {
                    _dataSfarsit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ObiectivSelectat
        {
            get => _obiectivSelectat;
            set
            {
                if (_obiectivSelectat != value)
                {
                    _obiectivSelectat = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<RaportConsum> RaportData
        {
            get => _raportData;
            set
            {
                if (_raportData != value)
                {
                    _raportData = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Obiective
        {
            get => _obiective;
            set
            {
                if (_obiective != value)
                {
                    _obiective = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand FiltreazaCommand { get; }
        public ICommand ExporterExcelCommand { get; }

        private void Filtreaza()
        {
            var data = _dataService.GetRaportConsum(DataInceput, DataSfarsit, ObiectivSelectat);
            RaportData.Clear();
            foreach (var item in data)
            {
                RaportData.Add(item);
            }
        }

        private void ExportarExcel()
        {
            if (RaportData.Count == 0)
            {
                System.Windows.MessageBox.Show("Nu sunt date de exportat. Vă rugăm să filtrați datele mai întâi.", "Avertisment", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            ExcelExportService.ExportToExcel(RaportData.ToList(), DataInceput, DataSfarsit, ObiectivSelectat);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
