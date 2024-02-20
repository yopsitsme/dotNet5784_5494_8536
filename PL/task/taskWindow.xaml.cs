using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.task;

/// <summary>
/// Interaction logic for taskWindow.xaml
/// </summary>
public partial class taskWindow : Window
{
    int idState = 0;
    
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get(); // Business Logic API
    public ObservableCollection<BO.TaskInList> taskList = null; // List of engineers
    public ObservableCollection<BO.TaskInList>? dependencies { get; set; } = null;// List of engineers


    public BO.Task ContentTask
    {
        get { return (BO.Task)GetValue(TaskProperty); }
        set { SetValue(TaskProperty, value); }
    }

    public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("ContentTask", typeof(BO.Task), typeof(taskWindow), new PropertyMetadata(null));
    public taskWindow(ObservableCollection<BO.TaskInList>? listTask,int Id=0)
    {
        InitializeComponent();
        taskList = listTask;
        if (Id == 0)
        {
            ContentTask = new BO.Task();
        }
        else
        {
            ContentTask = s_bl.Task.Read(Id);
            idState = Id;
            dependencies= new ObservableCollection<BO.TaskInList>(Tools.depndentTesks(Id));
        }

    }
    private void Task_dubbleClick(object sender, MouseButtonEventArgs e)
    {
        BO.TaskInList? TaskInList = (sender as ListView)?.SelectedItem as BO.TaskInList;
        if (TaskInList != null)
        {
            taskWindow ew = new taskWindow(dependencies, TaskInList.Id);
            ew.ShowDialog();
        }
    }
}
