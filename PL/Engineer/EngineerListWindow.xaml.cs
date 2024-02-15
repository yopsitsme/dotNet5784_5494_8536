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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EnginennrListWindow.xaml
/// </summary>
public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperience experience { get; set; } = BO.EngineerExperience.All;

    public EngineerListWindow()
    {
        InitializeComponent();
        EngineerList = s_bl?.Engineer.ReadAll()!;

    }

    public IEnumerable<BO.Engineer> EngineerList
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

    private void sortByEngineerExperience(object sender, SelectionChangedEventArgs e)
    {
        EngineerList = (experience == BO.EngineerExperience.All) ?
s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == experience)!;

    }

    private void AddEngineer_click(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().ShowDialog();
    }

    private void Engineer_dubbleClick(object sender, MouseButtonEventArgs e)
    {
        BO.Engineer? EngineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
        if (EngineerInList != null)
        {
            EngineerWindow ew= new EngineerWindow(EngineerInList.Id);
            ew.ShowDialog();
        }

    }
}
