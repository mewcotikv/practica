using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.ViewModels
{
    public class ComandaViewModel : Helpers.ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ObservableCollection<Comanda> _comenzi;
        private Comanda _selectedComanda;
        private decimal _valoareTotala;

        public ComandaViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _comenzi = new ObservableCollection<Comanda>();

            LoadComenziCommand = new Helpers.RelayCommand(async _ => await LoadComenzi());
            AddComandaCommand = new Helpers.RelayCommand(async _ => await AddComanda());
            EditComandaCommand = new Helpers.RelayCommand(async _ => await EditComanda(), _ => SelectedComanda != null);
            DeleteComandaCommand = new Helpers.RelayCommand(async _ => await DeleteComanda(), _ => SelectedComanda != null);
            PrintComandaCommand = new Helpers.RelayCommand(_ => PrintComanda(), _ => SelectedComanda != null);
        }

        public ObservableCollection<Comanda> Comenzi
        {
            get => _comenzi;
            set => SetProperty(ref _comenzi, value);
        }

        public Comanda SelectedComanda
        {
            get => _selectedComanda;
            set => SetProperty(ref _selectedComanda, value);
        }

        public decimal ValoareTotala
        {
            get => _valoareTotala;
            set => SetProperty(ref _valoareTotala, value);
        }

        public ICommand LoadComenziCommand { get; }
        public ICommand AddComandaCommand { get; }
        public ICommand EditComandaCommand { get; }
        public ICommand DeleteComandaCommand { get; }
        public ICommand PrintComandaCommand { get; }

        private async Task LoadComenzi()
        {
            var comenzi = await _unitOfWork.ComandaRepository.GetAllAsync();
            Comenzi = new ObservableCollection<Comanda>(comenzi.OrderByDescending(c => c.DataComanda));
            
            ValoareTotala = Comenzi.Sum(c => c.ValoareTotala);
        }

        private async Task AddComanda()
        {
            // TODO: Implement add order logic
            await Task.CompletedTask;
        }

        private async Task EditComanda()
        {
            // TODO: Implement edit order logic
            await Task.CompletedTask;
        }

        private async Task DeleteComanda()
        {
            if (SelectedComanda != null)
            {
                _unitOfWork.ComandaRepository.Delete(SelectedComanda);
                await _unitOfWork.SaveChangesAsync();
                await LoadComenzi();
            }
        }

        private void PrintComanda()
        {
            // TODO: Implement print order logic
        }
    }
}
