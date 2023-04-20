using EstudoTreinamento.Class;
using EstudoTreinamento.Class.Db;
using EstudoTreinamento.Class.Entity;
using EstudoTreinamento.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace EstudoTreinamento
{
    public class MainWindowsVM : INotifyPropertyChanged
    {

        public ICommand AddVehicle { get; private set; }
        public ICommand DeleteVehicle { get; private set; }
        public ICommand UpdateVehicle { get; private set; }
        public ICommand AddSeller { get; private set; }
        public ICommand UpdateSeller { get; private set; }
        public ICommand DeleteSeller { get; private set; }
        public ICommand ReportsSeller { get; private set; }
        public ICommand SellVehicle { get; private set; }

        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<Seller> Sellers { get; set; }

        public Vehicle SelectedVehicle { get; set; }
        public Seller SelectedSeller { get; set; }

        private IDatabase postgreDb;

        public MainWindowsVM()
        {
            try
            {
                postgreDb = new PostgreDb();
                Sellers = new ObservableCollection<Seller>(postgreDb.GetSellers());
                Vehicles = new ObservableCollection<Vehicle>(postgreDb.GetVehicles());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }

            InitializeVehicleCommands();
            InitializeSellerCommands();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializeVehicleCommands()
        {
            AddVehicle = new RelayCommand((object _) =>
            {
                Vehicle newVehicle = new Vehicle();
                FormVehicle screen = new FormVehicle();
                screen.DataContext = newVehicle;
                bool? resultDialog = screen.ShowDialog();
                if (resultDialog.HasValue && resultDialog.Value == true)
                {
                    try
                    {
                        postgreDb.InsertVehicle(newVehicle);
                        Vehicles.Clear();
                        Vehicles = new ObservableCollection<Vehicle>(postgreDb.GetVehicles());
                        Notify(nameof(Vehicles));
                        MessageBox.Show("Veiculo inserido");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao inserir Veiculo \n"
                            + ex.Message);
                    }
                }
            });

            DeleteVehicle = new RelayCommand((object _) =>
            {
                if (SelectedVehicle != null)
                {
                    try
                    {
                        postgreDb.DeleteVehicle(SelectedVehicle);
                        Vehicles.Clear();
                        Vehicles = new ObservableCollection<Vehicle>(postgreDb.GetVehicles());
                        Notify(nameof(Vehicles));
                        MessageBox.Show("Veiculo excluido!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao deletar Veiculo \n"
                            + ex.Message);
                    }
                }
            });

            UpdateVehicle = new RelayCommand((object _) =>
            {

                FormVehicle screen = new FormVehicle();
                screen.DataContext = SelectedVehicle;
                bool? resultScreen = screen.ShowDialog();
                if (resultScreen.HasValue && resultScreen.Value == true)
                {
                    try
                    {
                        postgreDb.UpdateVehicle(SelectedVehicle);
                        Vehicles.Clear();
                        Vehicles = new ObservableCollection<Vehicle>(postgreDb.GetVehicles());
                        Notify(nameof(Vehicles));
                        MessageBox.Show("Veiculo atualizado!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar Veiculo \n"
                            + ex.Message);
                    }
                }
            });
        }

        public void InitializeSellerCommands()
        {
            AddSeller = new RelayCommand((object _) =>
            {
                Seller newSeller = new Seller();
                FormSeller formSeller = new FormSeller();
                formSeller.DataContext = newSeller;
                bool? resultDialog = formSeller.ShowDialog();
                if (resultDialog.HasValue && resultDialog.Value == true)
                {
                    try
                    {
                        postgreDb.InsertSeller(newSeller);
                        Sellers.Clear();
                        Sellers = new ObservableCollection<Seller>(postgreDb.GetSellers());
                        Notify(nameof(Sellers));
                        MessageBox.Show("Vendedor inserido!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao inserir vendedor \n"
                            + ex.Message);
                    }
                }
            });

            UpdateSeller = new RelayCommand((object _) =>
            {
                FormSeller formSeller = new FormSeller();
                formSeller.DataContext = SelectedSeller;
                bool? resultDialog = formSeller.ShowDialog();
                if (resultDialog.HasValue && resultDialog.Value)
                {
                    try
                    {
                        postgreDb.UpdateSeller(SelectedSeller);
                        Sellers.Clear();
                        Sellers = new ObservableCollection<Seller>(postgreDb.GetSellers());
                        Notify(nameof(Sellers));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar vendedor \n"
                            + ex.Message);
                    }
                }
            });

            DeleteSeller = new RelayCommand((object _) =>
            {
                try
                {
                    postgreDb.DeleteSeller(SelectedSeller);
                    Sellers.Clear();
                    Sellers = new ObservableCollection<Seller>(postgreDb.GetSellers());
                    Notify(nameof(Sellers));
                    MessageBox.Show("Vendedor excluido!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar vendedor \n"
                        + ex.Message);
                }
            });

            ReportsSeller = new RelayCommand((object _) =>
            {
                try
                {
                    Report report = postgreDb.GetSales(SelectedSeller);
                    SellerReport sellerReport = new SellerReport();
                    sellerReport.DataContext = report;
                    sellerReport.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar Veiculo \n"
                        + ex.Message);
                }
            });

            SellVehicle = new RelayCommand((object _) =>
            {
                try
                {
                    if (SelectedVehicle != null && SelectedSeller != null)
                    {
                        postgreDb.SaleVehicle(SelectedSeller, SelectedVehicle);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar Veiculo \n"
                        + ex.Message);
                }
            });


        }

    }


}
