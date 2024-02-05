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
using System.Media;
using System.Linq;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

namespace Spotify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = it-woche-2024";

        public MainWindow()
        {
           
        InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbl_Fehlermeldung.Visibility = Visibility.Hidden;
        }


        private void Btn_anmelden_Click(object sender, RoutedEventArgs e)
        {
            // ZUM VERSTÄNDIS: Wir erstellen erst eine Verbindung zum Server wenn wir auf den Button klicken, er checkt ob admin admin (Benutzer, Password) vorhanden ist und blendet das Overlay aus wenn Benutzer und Passwort stimmen. Wenn Benutzer und Passwort nicht stimmen, wird dir das angezeigt.

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            
            string BenutzerEingegeben = TB_benutzer.Text.ToString();
            string PasswortEingegeben = PB_passwort.Password.ToString();

            string query = "SELECT Benutzername, Passwort FROM login WHERE Benutzername = @Benutzername AND Passwort = @Passwort";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Benutzername", BenutzerEingegeben);
            cmd.Parameters.AddWithValue("@Passwort", PasswortEingegeben);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                LoginCanvas.Visibility = Visibility.Hidden;
            }
            else
            {
                Lbl_Fehlermeldung.Visibility = Visibility.Visible;

                TB_benutzer.Clear();
                TB_benutzer.BorderBrush = Brushes.Red;
                TB_benutzer.BorderThickness = new Thickness(3);

                PB_passwort.Clear();
                PB_passwort.BorderBrush = Brushes.Red;
                PB_passwort.BorderThickness = new Thickness(3);
            }

            reader.Close(); 
            conn.Close();
            // Nach den Befehl wird die Connection zur Login-Tabelle geschlossen, weil ja der Login erfolgreich war.

        }
    }
}
