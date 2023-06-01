using DimplomStandart.Classes;
using DimplomStandart.Entities;
using DimplomStandart.Windows.DataReference.DisciplineWindows;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
            SpecialisationEntities specialisation = dgSpecialisation.SelectedItem as SpecialisationEntities;

            string idDiscipline = (from q in App.disciplines where q.IdSpecialisation==specialisation.Id select q.Id).ToList().Single();
            string idGroup = (from q in App.groups where q.Specialisation==specialisation.Id select q.Id).ToList().Single();
            string idStudent = (from q in App.students where q.Group==idGroup select q.Id).ToList().Single();

            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM public.specialisation_discipline WHERE id_discipline = @Id", App.Connection());
            command.Parameters.AddWithValue("@Id", idDiscipline);
            command.ExecuteNonQuery();

            command = new NpgsqlCommand("DELETE FROM public.discipline WHERE id = @Id", App.Connection());
            command.Parameters.AddWithValue("@Id", idDiscipline);
            command.ExecuteNonQuery();

            command = new NpgsqlCommand("DELETE FROM public.student WHERE id = @Id", App.Connection());
            command.Parameters.AddWithValue("@Id", idStudent);
            command.ExecuteNonQuery();


            command = new NpgsqlCommand("DELETE FROM public.group WHERE id = @Id",App.Connection());
            command.Parameters.AddWithValue("@Id", idGroup);
            command.ExecuteNonQuery();

            command = new NpgsqlCommand($"DELETE FROM public.specialisation WHERE id = @Id", App.Connection());
            command.Parameters.AddWithValue("@Id", specialisation.Id);
            command.ExecuteNonQuery();

            App.Refresh();

            dgSpecialisation.ItemsSource = null;
            dgSpecialisation.ItemsSource = App.specialisations;
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
