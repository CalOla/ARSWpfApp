using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ARSWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MyWindow_Loaded;

        }
        #endregion

        private void MyWindow_Loaded(Object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new SignIn());
        }
    }
}
