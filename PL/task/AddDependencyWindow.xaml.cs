using BO;
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
using System.Windows.Shapes;

namespace PL.task
{
    /// <summary>
    /// Interaction logic for AddDependencyWindow.xaml
    /// </summary>
    public partial class AddDependencyWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get(); // Business Logic API

        int id = 0;
        public List<BO.TaskInList> allTask { get; set; }
        public AddDependencyWindow(int Id)
        {
            id= Id; 
            InitializeComponent();
            allTask = s_bl.TaskInList.ReadAll().ToList();
        }
        private void Chossen_dubbleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.TaskInList t = sender as BO.TaskInList;
                Tools.addDependency(id, t.Id);
                this.Close();
                MessageBox.Show("dependency add successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }


        }
    }
}
