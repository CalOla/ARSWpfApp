using System.Windows;
using System.Windows.Controls;
using ARSWpfApp.ServiceReference1;

namespace ARSWpfApp
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        #region Private Members
        private string firstName;
        private string lastName;
        private string username;
        private string password;
        private string dateOfBirth;
        private string administrator;
        private string gender;
        private int isAdmin;

        private ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        #endregion
        public SignUp()
        {
            InitializeComponent();
        }

        private void HandleGenderCheck(object sender, RoutedEventArgs e)
        {
            toggleGender.Content = "Female";
        }
        private void HandleGenderUnchecked(object sender, RoutedEventArgs e)
        {
            toggleGender.Content = "Male";
        }

        private void HandleAdminCheck(object sender, RoutedEventArgs e)
        {
            toggleAdmin.Content = "No";
        }

        private void HandleAdminUnchecked(object sender, RoutedEventArgs e)
        {
            toggleAdmin.Content = "Yes";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            firstName = txtFirstname.Text;
            lastName = txtLastname.Text;
            username = txtUsername.Text;


            InsertUser user = new InsertUser();
            user.FirstName = txtFirstname.Text;
            user.LastName = txtLastname.Text;
            user.UserPassword = txtPassword.Password;
            user.Email = txtEmail.Text;
            user.Gender = toggleGender.Content.ToString();
            user.UserName = txtUsername.Text;

            if (toggleAdmin.Content.ToString() == "Yes")
            {
                isAdmin = 1;
            }
            else
            {
                isAdmin = 0;
            }

            user.IsAdmin = isAdmin;

            string response = client.AddUser(user);
            client.Close();
            dummyLabel.Content = response;
            if (response == "Successfully Inserted")
            {
                this.NavigationService.Navigate(new SignIn());
            }

        }
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SignIn());
        }
    }
}
