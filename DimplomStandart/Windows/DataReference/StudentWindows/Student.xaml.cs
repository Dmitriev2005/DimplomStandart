using DimplomStandart.Entities;
using DimplomStandart.Windows.DataReference.DisciplineWindows;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace DimplomStandart.Windows.DataReference.StudentWindows
{
    /// <summary>
    /// Логика взаимодействия для Student.xaml
    /// </summary>
    public partial class Student : Window
    {
        public Student()
        {
            InitializeComponent();

            foreach (var item in App.students)
                cmbGroup.Items.Add(item.GroupStr);

            dgStudent.ItemsSource = null;

            dgStudent.ItemsSource = App.students;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

            dgStudent.ItemsSource = null;
            dgStudent.ItemsSource = App.students;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            StudentEntities selStudent = dgStudent.SelectedItem as StudentEntities;
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM public.student_discipline WHERE id_student = @Id::bigint",App.Connection());
            command.Parameters.AddWithValue("@Id", selStudent.Id);
            command.ExecuteNonQuery();

            command = new NpgsqlCommand("DELETE FROM public.student WHERE id=@Id::bigint", App.Connection());
            command.Parameters.AddWithValue("@Id", selStudent.Id);
            command.ExecuteNonQuery();

            App.students.Remove(selStudent);

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgStudent.ItemsSource = null;
            dgStudent.ItemsSource = App.students;

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditStudent addEditStudent = new AddEditStudent(dgStudent.SelectedItem as StudentEntities);
            addEditStudent.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();


            dgStudent.ItemsSource = null;
            dgStudent.ItemsSource = App.students;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditStudent addEditStudent = new AddEditStudent(new StudentEntities());
            addEditStudent.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();


            dgStudent.ItemsSource = null;
            dgStudent.ItemsSource = App.students;

        }
        private void cmbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                dgStudent.ItemsSource = null;
                dgStudent.ItemsSource = (from q in App.students where q.GroupStr == cmbGroup.SelectedItem.ToString() select q).ToList();


        }
    }
}
