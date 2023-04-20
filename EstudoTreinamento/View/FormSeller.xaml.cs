using System.Windows;

namespace EstudoTreinamento.View
{
    /// <summary>
    /// Lógica interna para FormSeller.xaml
    /// </summary>
    public partial class FormSeller : Window
    {
        public FormSeller()
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
