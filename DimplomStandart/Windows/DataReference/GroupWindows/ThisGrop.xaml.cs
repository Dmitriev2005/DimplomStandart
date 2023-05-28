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
using Npgsql;

namespace DimplomStandart.Windows.DataReference.GroupWindows
{
    /// <summary>
    /// Логика взаимодействия для Group.xaml
    /// </summary>
    public partial class ThisGroup : Window
    {
        public ThisGroup()
        {
            InitializeComponent();

            dgGroup.ItemsSource = App.groups;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditThisGroup addEditThisGroup = new AddEditThisGroup(new GroupEntities());
            addEditThisGroup.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgGroup.ItemsSource = null;
            dgGroup.ItemsSource = App.groups;

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditThisGroup addEditThisGroup = new AddEditThisGroup(dgGroup.SelectedItem as GroupEntities);
            addEditThisGroup.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgGroup.ItemsSource = null;
            dgGroup.ItemsSource = App.groups;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            GroupEntities groupEntities = dgGroup.SelectedItem as GroupEntities;
            NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM public.group where id =  {groupEntities.Id}", App.Connection());
            command.ExecuteNonQuery();

            App.groups.Remove(groupEntities);

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgGroup.ItemsSource = null;
            dgGroup.ItemsSource = App.groups;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<GroupEntities> buffer = new List<GroupEntities>();
            buffer = App.groups.Where(p => p.Name.Contains(tbSearch.Text)).ToList();

            dgGroup.ItemsSource = null;
            dgGroup.ItemsSource = buffer;

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";

            dgGroup.ItemsSource = null;
            dgGroup.ItemsSource = App.groups;
        }
    }
}
