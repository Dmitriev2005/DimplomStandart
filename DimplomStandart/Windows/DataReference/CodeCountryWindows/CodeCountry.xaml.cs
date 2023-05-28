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

namespace DimplomStandart.Windows.DataReference.CodeCountryWindows
{
    /// <summary>
    /// Логика взаимодействия для CodeCountry.xaml
    /// </summary>
    public partial class CodeCountry : Window
    {
        public CodeCountry()
        {
            InitializeComponent();
            dgCodeCountry.ItemsSource = null;
            dgCodeCountry.ItemsSource = App.codeCountrys;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";

            dgCodeCountry.ItemsSource = null;
            dgCodeCountry.ItemsSource = App.codeCountrys;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<CodeCountryEntities> buffer = new List<CodeCountryEntities>();

            buffer = App.codeCountrys.Where(p => p.Name.Contains(tbSearch.Text)).ToList();

            dgCodeCountry.ItemsSource = null;
            dgCodeCountry.ItemsSource = buffer;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand("DELETE FROM public.code_country where id=@Id::bigint",App.Connection());
            npgsqlCommand.Parameters.AddWithValue("Id", (dgCodeCountry.SelectedItem as CodeCountryEntities).Id);
            npgsqlCommand.ExecuteNonQuery();
            App.codeCountrys.Remove(dgCodeCountry.SelectedItem as CodeCountryEntities);

            dgCodeCountry.ItemsSource = null;
            dgCodeCountry.ItemsSource = App.codeCountrys;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditCodeCountry addEditCodeCountry = new AddEditCodeCountry(dgCodeCountry.SelectedItem as CodeCountryEntities);
            addEditCodeCountry.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgCodeCountry.ItemsSource = null;
            dgCodeCountry.ItemsSource = App.codeCountrys;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditCodeCountry addEditCodeCountry = new AddEditCodeCountry(new CodeCountryEntities());
            addEditCodeCountry.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgCodeCountry.ItemsSource = null;
            dgCodeCountry.ItemsSource = App.codeCountrys;
        }
    }
}
