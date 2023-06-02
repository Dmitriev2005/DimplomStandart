using DimplomStandart.Classes;
using DimplomStandart.Entities;
using DimplomStandart.Windows.DataReference.DisciplineWindows;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DimplomStandart.Windows.DataReference.SpecialisationWindows
{
    /// <summary>
    /// Логика взаимодействия для Specialisation.xaml
    /// </summary>
    public partial class Specialisation : Window
    {
        public Specialisation()
        {
            InitializeComponent();

            //Вывожу List в DataGrid
            dgSpecialisation.ItemsSource = null;
            dgSpecialisation.ItemsSource = App.specialisations;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditSpecialisation specialisation = new AddEditSpecialisation(new SpecialisationEntities());
            specialisation.ShowDialog();

            //Делаю так, что бы, главное окно при закрытии, не уходило за другие
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgSpecialisation.ItemsSource = null;
            dgSpecialisation.ItemsSource = App.specialisations;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditSpecialisation specialisation = new AddEditSpecialisation(dgSpecialisation.SelectedItem as SpecialisationEntities);
            specialisation.ShowDialog();

            //Делаю так, что бы, главное окно при закрытии, не уходило за другие
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgSpecialisation.ItemsSource = null;
            dgSpecialisation.ItemsSource = App.specialisations;
        }
        //Событие удаления
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("При удалениие специальности, будут удаленные все связанные упоменания данной специальности! " +
                "Вы уверенны что хотите этого?","Требуется подтверждение",MessageBoxButton.YesNo);
            if(answer == MessageBoxResult.Yes)
            {
                SpecialisationEntities specialisation = dgSpecialisation.SelectedItem as SpecialisationEntities;

                NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM public.specialisation WHERE id = @Id::bigint", App.Connection());
                command.Parameters.AddWithValue("@Id", specialisation.Id);
                command.ExecuteNonQuery();

                App.Refresh();

                dgSpecialisation.ItemsSource = null;
                dgSpecialisation.ItemsSource = App.specialisations;

            }
        }
        //Событие поиска
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<SpecialisationEntities> buffer = new List<SpecialisationEntities>();

            buffer = App.specialisations.Where(p => p.NameShort.Contains(tbSearch.Text)||p.NameLong.Contains(tbSearch.Text)||p.Qualification.Contains(tbSearch.Text)).ToList();
            dgSpecialisation.ItemsSource = null;
            dgSpecialisation.ItemsSource = buffer;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";
            dgSpecialisation.ItemsSource = null;
            dgSpecialisation.ItemsSource = App.specialisations;
        }

       
    }
}
