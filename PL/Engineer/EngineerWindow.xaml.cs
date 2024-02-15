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
/// Interaction logic for EnginnerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static int idState = 0;

    public BO.Engineer ContentEngineer
    {
        get { return (BO.Engineer)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }

    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("ContentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

    public EngineerWindow(int Id=0)
    {
        InitializeComponent();
        if (Id == 0)
        {
            ContentEngineer = new BO.Engineer();
        }
        else { 
            ContentEngineer=s_bl.Engineer.Read(Id);
            idState = Id;
        }
    }

    private void save_click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (idState == 0)
            {
                s_bl.Engineer.Create(ContentEngineer);
                MessageBox.Show("Engineer created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                s_bl.Engineer.Update(ContentEngineer);
                MessageBox.Show("Engineer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
            this.Close();
    }

}
