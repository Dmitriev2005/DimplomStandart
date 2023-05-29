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
using DimplomStandart.Classes;
using DimplomStandart.Windows;
using DimplomStandart.Entities;
using Npgsql;
using System.Buffers;
using DimplomStandart.Classes;

namespace DimplomStandart.Windows.DataReference.DisciplineWindows
{
    /// <summary>
    /// Логика взаимодействия для Discipline.xaml
    /// </summary>
    public partial class Discipline : Window
    {
        public Discipline()
        {
            InitializeComponent();

            //Вывожу List в DataGrid
            dgDiscipline.ItemsSource = null;
            dgDiscipline.ItemsSource = App.disciplines;

           
        }

        //Событие добавление новой записи
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            AddEditDiscipline discipline = new AddEditDiscipline(new DisciplineEntities());
            discipline.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgDiscipline.ItemsSource = null;
            dgDiscipline.ItemsSource = App.disciplines;

        }
        //Событие редактирования записи
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            AddEditDiscipline discipline = new AddEditDiscipline(dgDiscipline.SelectedItem as DisciplineEntities);

            discipline.ShowDialog();

            //Делаю так, что бы, главное окно при закрытии, не уходило за другие
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();


            dgDiscipline.ItemsSource = null;
            dgDiscipline.ItemsSource = App.disciplines;
        }
        //Событие удаления записи
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            DisciplineEntities discipline = dgDiscipline.SelectedItem as DisciplineEntities;
            NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM public.student_discipline where id_discipline={discipline.Id}",App.Connection());
            command.ExecuteNonQuery();
            command = new NpgsqlCommand($"DELETE FROM public.discipline where id={discipline.Id}",App.Connection());
            command.ExecuteNonQuery();

            App.disciplines.Remove(discipline);

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgDiscipline.ItemsSource = null;
            dgDiscipline.ItemsSource = App.disciplines;
            
        }       
        //Событие очищение поиска
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";
            dgDiscipline.ItemsSource = null;
            dgDiscipline.ItemsSource = App.disciplines;
        }
        //Событие поиска
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            // Создаю буффер для результатов поиска
            List<DisciplineEntities> buffer = new List<DisciplineEntities>();

            buffer = App.disciplines.Where(p => p.Name.Contains(tbSearch.Text)).ToList();

            dgDiscipline.ItemsSource = null;
            dgDiscipline.ItemsSource = buffer;

        }
    }
}
