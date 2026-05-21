using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.ViewModels
{
    public class MaterialViewModel : Helpers.ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ObservableCollection<Material> _materiale;
        private Material _selectedMaterial;

        public MaterialViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _materiale = new ObservableCollection<Material>();

            LoadMaterialeCommand = new Helpers.RelayCommand(async _ => await LoadMateriale());
            AddMaterialCommand = new Helpers.RelayCommand(async _ => await AddMaterial());
            EditMaterialCommand = new Helpers.RelayCommand(async _ => await EditMaterial(), _ => SelectedMaterial != null);
            DeleteMaterialCommand = new Helpers.RelayCommand(async _ => await DeleteMaterial(), _ => SelectedMaterial != null);
        }

        public ObservableCollection<Material> Materiale
        {
            get => _materiale;
            set => SetProperty(ref _materiale, value);
        }

        public Material SelectedMaterial
        {
            get => _selectedMaterial;
            set => SetProperty(ref _selectedMaterial, value);
        }

        public ICommand LoadMaterialeCommand { get; }
        public ICommand AddMaterialCommand { get; }
        public ICommand EditMaterialCommand { get; }
        public ICommand DeleteMaterialCommand { get; }

        private async Task LoadMateriale()
        {
            var materiale = await _unitOfWork.MaterialRepository.GetAllAsync();
            Materiale = new ObservableCollection<Material>(materiale.OrderBy(m => m.Tip).ThenBy(m => m.Denumire));
        }

        private async Task AddMaterial()
        {
            // TODO: Implement add material logic
            await Task.CompletedTask;
        }

        private async Task EditMaterial()
        {
            // TODO: Implement edit material logic
            await Task.CompletedTask;
        }

        private async Task DeleteMaterial()
        {
            if (SelectedMaterial != null)
            {
                _unitOfWork.MaterialRepository.Delete(SelectedMaterial);
                await _unitOfWork.SaveChangesAsync();
                await LoadMateriale();
            }
        }
    }
}
