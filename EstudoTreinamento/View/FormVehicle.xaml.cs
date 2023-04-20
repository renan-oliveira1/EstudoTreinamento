using System.Windows;

namespace EstudoTreinamento
{
    /// <summary>
    /// Lógica interna para EditVehicle.xaml
    /// </summary>
    public partial class FormVehicle : Window
    {
        public FormVehicle()
        {
            InitializeComponent();
        }

        public void btnSave(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public void btnCancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
