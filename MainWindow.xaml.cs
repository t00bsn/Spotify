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
            //MainWindow wird bei Programmstart auf Bildschirm ausgerichtet!
            Application.Current.MainWindow.WindowState = WindowState.Maximized;


            // Fehlermeldung-Label sind beim Programmstart ausgeblendet!
            Lbl_Fehlermeldung.Visibility = Visibility.Hidden;

            Lbl_FehlermeldungRegisterErfolgreich.Visibility = Visibility.Hidden;
            Lbl_FehlermeldungRegister.Visibility = Visibility.Hidden;
            Lbl_FehlermeldungRegisterUserVerwendet.Visibility = Visibility.Hidden;
            Lbl_FehlermeldungRegister.Visibility = Visibility.Hidden;


            //Register-Tab ist ausgeblendet bei Programmstart
            RegisterCanvas.Visibility = Visibility.Hidden;

            //Designverbesserungen:

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
/*                TB_benutzer.BorderBrush = Brushes.Green;
                TB_benutzer.BorderThickness = new Thickness(3);

                PB_passwort.BorderBrush = Brushes.Green;
                PB_passwort.BorderThickness = new Thickness(3);
*/              
                //Eventuell eine Verzögerung einbauen und Code (siehe oben) einbauen.

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

                Lbl_Fehlermeldung.Visibility = Visibility.Visible;
                
            }

            reader.Close(); 
            conn.Close();
            // Nach dem ausgeführten Befehl wird die Connection zur Login-Tabelle geschlossen, weil ja der Login erfolgreich war.

        }

        private void Btn_Registrieren_Click(object sender, RoutedEventArgs e)
        {
            Lbl_FehlermeldungRegisterErfolgreich.Visibility = Visibility.Hidden;
            Lbl_FehlermeldungRegister.Visibility = Visibility.Hidden;
            Lbl_FehlermeldungRegisterUserVerwendet.Visibility = Visibility.Hidden;
            Lbl_FehlermeldungRegister.Visibility = Visibility.Hidden;


            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string BenutzerRegisterEingegeben = TB_BenutzerRegistrieren.Text.ToString();
            string PasswortRegisterEingeben   = PB_PasswortRegistrieren.Password.ToString();
            

            //Überprüft ob User schon bereits angelegt worden ist.
            string checkUserQuery = "SELECT COUNT(*) FROM login WHERE Benutzername = @Benutzername";
            MySqlCommand checkUserCmd = new MySqlCommand(checkUserQuery, conn);
            checkUserCmd.Parameters.AddWithValue("@Benutzername", BenutzerRegisterEingegeben);

            int userCount = Convert.ToInt32(checkUserCmd.ExecuteScalar());

            if(userCount > 0)
            {
                Lbl_FehlermeldungRegisterUserVerwendet.Visibility = Visibility.Visible;
            }
            else
            {
                string registerQuery = "INSERT INTO login (Benutzername, Passwort) VALUES (@Benutzername, @Passwort)";
                MySqlCommand cmd = new MySqlCommand(registerQuery, conn);

                cmd.Parameters.AddWithValue("@Benutzername", BenutzerRegisterEingegeben);
                cmd.Parameters.AddWithValue("@Passwort", PasswortRegisterEingeben);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Lbl_FehlermeldungRegisterErfolgreich.Visibility = Visibility.Visible;
                }
                else
                    Lbl_FehlermeldungRegister.Visibility = Visibility.Visible;
            }

            conn.Close();

        }

        private void Btn_RegisterSwitch_Click(object sender, RoutedEventArgs e)
        {
            LoginCanvas.Visibility = Visibility.Hidden;
            RegisterCanvas.Visibility = Visibility.Visible;
        }

        private void Btn_zurueckZurAnmeldung_Click(object sender, RoutedEventArgs e)
        {
            RegisterCanvas.Visibility = Visibility.Hidden;
            LoginCanvas.Visibility = Visibility.Visible;
        }
    }
}
