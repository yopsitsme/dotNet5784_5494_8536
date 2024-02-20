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

namespace PL.UserEngineer
{
    /// <summary>
    /// Interaction logic for userEngineerWindow.xaml
    /// </summary>
    public partial class userEngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public userEngineerWindow(int id)
        {
           BO.Engineer e= s_bl.Engineer.Read(id);
            TaskTitle = (e.Task?.Id).ToString() ?? "0";
            TaskDescription = e.Task?.Alias ?? "no task";
            InitializeComponent();

        }

        private void UpdateTaskDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewEngineerTasks_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
