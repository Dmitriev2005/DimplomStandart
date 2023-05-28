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
using DimplomStandart.Entities;
using DimplomStandart.Windows.DataReference.DisciplineWindows;
using Npgsql;

namespace DimplomStandart.Windows.DataReference.SecretaryWindows
{
    /// <summary>
    /// Логика взаимодействия для Secretary.xaml
    /// </summary>
    public partial class Secretary : Window
    {
        public Secretary()
        {
            InitializeComponent();

            dgSecretary.ItemsSource = null;
            dgSecretary.ItemsSource = App.secretaries;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";
            dgSecretary.ItemsSource = null;
            dgSecretary.ItemsSource = App.secretaries;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<SecretaryEntities> buffer = new List<SecretaryEntities>();
            buffer = App.secretaries.Where(p => p.Name.Contains(tbSearch.Text)).ToList();

            dgSecretary.ItemsSource = null;
            dgSecretary.ItemsSource = buffer;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEntities secretary = dgSecretary.SelectedItem as SecretaryEntities;
            NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM public.secretary where id={secretary.Id}", App.Connection());
            command.ExecuteNonQuery();

            App.secretaries.Remove(secretary);

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgSecretary.ItemsSource = null;
            dgSecretary.ItemsSource = App.secretaries;

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddSecretary addSecretary = new AddSecretary(dgSecretary.SelectedItem as SecretaryEntities);
            addSecretary.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgSecretary.ItemsSource = null;
            dgSecretary.ItemsSource = App.secretaries;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddSecretary addSecretary = new AddSecretary(new SecretaryEntities());
            addSecretary.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgSecretary.ItemsSource = null;
            dgSecretary.ItemsSource = App.secretaries;
        }
    }
}
