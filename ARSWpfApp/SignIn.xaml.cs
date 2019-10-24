using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace ARSWpfApp
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        private ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        public SignIn()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SignUp());
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Service1Client credentials = new ServiceReference1.Service1Client();
            
            DataTable dataTable = client.GetSignInCredentials(txtUsername.Text, txtPassword.Password);

            string userPassword = "";
            string userName = "";
            string isAdmin = "";

            foreach (DataRow row in dataTable.Rows)
            {
                userPassword = row["UserPassword"].ToString();
                userName = row["UserName"].ToString();
                isAdmin = row["IsAdmin"].ToString();
            }

            if(userPassword == txtPassword.Password && userName == txtUsername.Text)
            {
                if(isAdmin == "True")
                {
                    this.NavigationService.Navigate(new AdminPage());
                }
                else
                {
                    CustomerPage customer = new CustomerPage();
                    customer.SetUpNavigationHandler(NavigationService);
                    this.NavigationService.Navigate(customer, userName);
                }
                return;
            }



            infoLabel.Content = "Invalid username or password";
        }
    }
}
