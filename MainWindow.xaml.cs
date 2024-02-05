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
using System;
using System.Media;
using System.IO;
using MySql.Data.MySqlClient;




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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer("TIGER_Reezy.wav");
            player.Play();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = it-woche-2024";

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * from songinformationen", conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Lb_Ausgabe.Items.Add(reader[1] + " " + reader[2]);
            }

            

            reader.Close();
            conn.Close();

        }
    }
}
