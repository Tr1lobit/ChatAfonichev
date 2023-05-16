using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatAfonichev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient httpClient = new HttpClient();
        public MainWindow()
        {
            HttpClient httpClient = new HttpClient();
            InitializeComponent();
            if(!string.IsNullOrEmpty(Properties.Settings.Default.login) && !string.IsNullOrEmpty(Properties.Settings.Default.password))
            {
            LoginTb.Text = Properties.Settings.Default.login;
            PassTb.Text = Properties.Settings.Default.password;
            }

        }

        private async void EnterBtnClick(object sender, RoutedEventArgs e)
        {
            var content = new Data { login = LoginTb.Text, password = PassTb.Text };
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:50512/Api/login", httpContent);

            if (message.IsSuccessStatusCode)
            {
                MessageBox.Show("Вы успещно вошли!");
            }
            else
            {
                MessageBox.Show("ERROR!");
            }

            //if((bool)RememberCheck.IsChecked)
            //{
            //    Properties.Settings.Default.login = LoginTb.Text;
            //    Properties.Settings.Default.password = PassTb.Text;
            //    Properties.Settings.Default.Save();
            //}
            //else
            //{
            //    Properties.Settings.Default.login = String.Empty;
            //    Properties.Settings.Default.password = String.Empty;
            //    Properties.Settings.Default.Save();
            //}
        }

        private void LoginTbGotFocus(object sender, RoutedEventArgs e)
        {
            if(LoginTb.Text == "Введите логин")
            LoginTb.Text = string.Empty;
        }

        private void LoginTbLostFocus(object sender, RoutedEventArgs e)
        {
            if(LoginTb.Text == string.Empty)
            LoginTb.Text = "Введите логин";
        }

        private void PassTbGotFocus(object sender, RoutedEventArgs e)
        {
            if(PassTb.Text == "Введите пароль")
            PassTb.Text = string.Empty;
        }

        private void PassTbLostFocus(object sender, RoutedEventArgs e)
        {
            if(PassTb.Text == string.Empty)
            PassTb.Text = "Введите пароль";
        }

        public class Data
        {
            public string login { get; set; }
            public string password { get; set; }
        }
    }
}
