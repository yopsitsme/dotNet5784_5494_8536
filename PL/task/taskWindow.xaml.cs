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
   
    public  int idState { get; set; } =0;
    public bool AddDependencyState { get; set; } = createMileseton.iscreated;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get(); // Business Logic API
    public ObservableCollection<BO.TaskInList> ?taskList = null; // List of engineers

    public ObservableCollection<BO.TaskInList>? Dependencies { get; set; } = null;// List of engineers


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
            AddDependencyState = true;
        }
        else
        {

            ContentTask = s_bl.Task.Read(Id);
            idState = Id;
            Dependencies= new ObservableCollection<BO.TaskInList>(Tools.depndentTesks(Id));
        }

    }
    private void Task_dubbleClick(object sender, MouseButtonEventArgs e)
    {
        BO.TaskInList? TaskInList = (sender as ListView)?.SelectedItem as BO.TaskInList;
        if (TaskInList != null)
        {
            taskWindow ew = new taskWindow(Dependencies, TaskInList.Id);
            ew.ShowDialog();
        }
    }

    private void click_addDependency(object sender, RoutedEventArgs e)
    {
        BO.TaskInList? TaskInList = sender as BO.TaskInList;
        new AddDependencyWindow(TaskInList.Id).ShowDialog();
        

    }

    private void save_click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (idState == 0)
            {
                s_bl.Task.Create(ContentTask);
                BO.TaskInList taskToAdd =s_bl.TaskInList.read(ContentTask.Id);
                taskList.Add(taskToAdd);
                MessageBox.Show("task created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                s_bl.Task.Update(ContentTask);
                BO.TaskInList TasktoUpdate = taskList.First(t => t.Id == idState);
                taskList.Remove(TasktoUpdate);
                BO.TaskInList taskToAdd = s_bl.TaskInList.read(ContentTask.Id);
                taskList.Add(taskToAdd);
                MessageBox.Show("task updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
                this.Close();
        }
        catch (BlNullPropertyException ex)
        {
            MessageBox.Show($"You must fill in id , Alias", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (InvalidInputException ex)
        {
            MessageBox.Show($"Invalid input: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BlAlreadyExistsException ex)
        {
            MessageBox.Show($"An task with such ID already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
