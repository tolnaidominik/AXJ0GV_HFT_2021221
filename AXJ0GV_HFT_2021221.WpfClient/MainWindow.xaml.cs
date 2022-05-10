using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
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

namespace AXJ0GV_HFT_2021221.WpfClient
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

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {

            cb_box.ItemsSource = Enum.GetValues(typeof(Sex)).Cast<Sex>();
        }
        private void ComboBoxDoggoLoaded(object sender, RoutedEventArgs e)
        {

            cb_boxDoggo.ItemsSource = Enum.GetValues(typeof(Sex)).Cast<Sex>();
        }
        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            cb_boxInjectionName.ItemsSource = Enum.GetValues(typeof(InjectionName)).Cast<InjectionName>();
            cb_boxInjectionCommonness.ItemsSource = Enum.GetValues(typeof(Commonness)).Cast<Commonness>();
        }
    }
}
