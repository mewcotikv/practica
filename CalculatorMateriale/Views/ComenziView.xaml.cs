using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorMateriale.Views
{
    public partial class ComenziView : UserControl
    {
        private IUnitOfWork _unitOfWork;
        private ObservableCollection<Comanda> comenziCollection;

        public ComenziView()
        {
            InitializeComponent();
            this.Loaded += ComenziView_Loaded;
        }

        private void ComenziView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get IUnitOfWork from dependency injection
                _unitOfWork = Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
                
                if (_unitOfWork == null)
                {
                    MessageBox.Show("Serviciile nu sunt inițializate", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la inițializare: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadOrders()
        {
            try
            {
                StatusBarText.Text = "Se încarcă comenzile...";
                var orders = await _unitOfWork.ComandaRepository.GetAllAsync();
                comenziCollection = new ObservableCollection<Comanda>(orders.OrderByDescending(c => c.DataComanda));
                ComenziGrid.ItemsSource = comenziCollection;
                RecordCountText.Text = $"Total: {comenziCollection.Count} comenzi";
                StatusBarText.Text = "Gata";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcare comenzi: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusBarText.Text = "Eroare la încărcare";
            }
        }

        private async void NewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = (await _unitOfWork.ClientRepository.GetAllAsync()).FirstOrDefault();
                if (client == null)
                {
                    client = new Client { Nume = "Client comanda", CUI = DateTime.Now.ToString("HHmmssff"), Localitate = "Chisinau" };
                    await _unitOfWork.ClientRepository.AddAsync(client);
                    await _unitOfWork.SaveChangesAsync();
                }

                var next = ((await _unitOfWork.ComandaRepository.GetAllAsync()).Count() + 1);
                var valoare = 2500m + (next * 475m);

                await _unitOfWork.ComandaRepository.AddAsync(new Comanda
                {
                    IdClient = client.IdClient,
                    DataComanda = DateTime.Now,
                    DataLivrare = DateTime.Now.AddDays(3),
                    Status = "Noua",
                    ValoareTotala = valoare,
                    TVA = decimal.Round(valoare * 0.20m, 2),
                    Observatii = $"Comanda rapida #{next}"
                });
                await _unitOfWork.SaveChangesAsync();
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la creare comanda: {ex.Message}", "Noua Comanda", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Comanda comanda)
            {
                MessageBox.Show($"Editare comandă {comanda.IdComanda} a clientului {comanda.IdClient}", "Editare", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Comanda comanda)
            {
                var result = MessageBox.Show($"Ești sigur că dorești să ștergi comanda {comanda.IdComanda}?",
                    "Confirmare ștergere", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _unitOfWork.ComandaRepository.Delete(comanda);
                        await _unitOfWork.SaveChangesAsync();
                        comenziCollection.Remove(comanda);
                        RecordCountText.Text = $"Total: {comenziCollection.Count} comenzi";
                        MessageBox.Show("Comandă ștearsă cu succes", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Eroare la ștergere: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private async void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Comanda comanda)
            {
                try
                {
                    // Status workflow: Noua -> Confirmata -> Finalizata -> Noua
                    string currentStatus = comanda.Status ?? "Noua";
                    string newStatus = currentStatus switch
                    {
                        "Noua" => "Confirmata",
                        "Confirmata" => "Finalizata",
                        "Finalizata" => "Noua",
                        _ => "Noua"
                    };

                    comanda.Status = newStatus;
                    ComenziGrid.Items.Refresh();
                    
                    // Save to database
                    _unitOfWork.ComandaRepository.Update(comanda);
                    await _unitOfWork.SaveChangesAsync();
                    
                    MessageBox.Show($"Status schimbat în: {newStatus}", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la schimbarea status: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            var total = comenziCollection?.Count ?? 0;
            var confirmate = comenziCollection?.Count(c => c.Status == "Confirmata") ?? 0;
            var finalizate = comenziCollection?.Count(c => c.Status == "Finalizata") ?? 0;
            var valoare = comenziCollection?.Sum(c => c.ValoareTotala) ?? 0;

            MessageBox.Show(
                $"Total comenzi: {total}\nConfirmate: {confirmate}\nFinalizate: {finalizate}\nValoare totala: {valoare:F2} MDL",
                "Raport Comenzi",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}


