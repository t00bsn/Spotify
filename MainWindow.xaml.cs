using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Spotify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Bei Programmstart wird der Bildschirm auf FullScreen gezogen
            Application.Current.MainWindow.WindowState = WindowState.Maximized;

            //Bei Programmstart steht oben links der Name des Users! FUNKTÌONIERT NOCH NICHT MUSS NOCH GEMERGED WERDEN
            //Lbl_WillkommenUser.Content = "Willkommen" + TB_BenutzerName.Text;

            //Labels von den Künstlern ausblenden
            Lbl_reezy.Visibility = Visibility.Hidden;
            Lbl_TravisScott.Visibility = Visibility.Hidden;
        }

        private void El_reezy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Lbl_TravisScott.Visibility = Visibility.Hidden;
            Lbl_reezy.Visibility = Visibility.Visible;

        }

        private void El_Profilbild_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Lbl_reezy.Visibility = Visibility.Hidden;
            Lbl_TravisScott.Visibility = Visibility.Hidden;
        }

        private void El_Travis_Scott_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Lbl_TravisScott.Visibility = Visibility.Visible;
            Lbl_reezy.Visibility = Visibility.Hidden;
        }

        
    }
}
