using DimplomStandart.Entities;
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

namespace DimplomStandart.Windows.DataReference.ItogWindows
{
    /// <summary>
    /// Логика взаимодействия для ItogScore.xaml
    /// </summary>
    public partial class ItogScore : Window
    {
        public ItogScore()
        {
            InitializeComponent();
            foreach (var item in App.groups)
                cmbGroup.Items.Add(item.Name);

            dgStudent.ItemsSource = null;
            dgStudent.ItemsSource = (from q in App.students where q.GroupStr == cmbGroup.SelectedItem.ToString() select q).ToList();

        }
        StudentEntities StudentEntities { get; set; }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditItogScore editItogScore = new EditItogScore(dgDiscipline.SelectedItem as ItogDisciplineEntities);
            editItogScore.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgDiscipline.ItemsSource = null;
            dgDiscipline.ItemsSource = (from q in App.itogDisciplines
                                        where q.IdStudent == StudentEntities.Id
                                        && q.Type != "Учебная практика" && q.Type != "Производственная практика"
                                        select q).ToList();
        }


        private void dgStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (check)
            {
                StudentEntities = dgStudent.SelectedItem as StudentEntities;
                GroupEntities studentGroup = (from q in App.groups where StudentEntities.Group==q.Id select q).ToList().Single();

                //string idGroup = (from q in App.disciplines where q.Id == studentGroup.Specialisation select q.).ToList().Single();
               dgDiscipline.ItemsSource = null;
               dgDiscipline.ItemsSource = (from q in App.itogDisciplines
                                            where q.IdStudent == StudentEntities.Id
                                            && q.Type != "Учебная практика" && q.Type != "Производственная практика"
                                            select q).ToList();

                dgPracticle.ItemsSource = null;
                dgPracticle.ItemsSource = (from q in App.itogDisciplines
                                           where q.IdStudent == StudentEntities.Id
                                           && q.Type == "Учебная практика" || q.Type == "Производственная практика"
                                           select q).ToList();

            }
        }


        private void btnEditP_Click(object sender, RoutedEventArgs e)
        {
            EditPracticle editItogScore = new EditPracticle(dgPracticle.SelectedItem as ItogDisciplineEntities);
            editItogScore.ShowDialog();

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();

            dgPracticle.ItemsSource = null;
            dgPracticle.ItemsSource = (from q in App.itogDisciplines
                                       where q.IdStudent == StudentEntities.Id
                                       && q.Type == "Учебная практика" || q.Type == "Производственная практика"
                                       select q).ToList();

        }
        private void dgPracticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void dgDiscipline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        bool check = true;
        private void cmbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            check = false;

            dgDiscipline.ItemsSource = null;
            dgPracticle.ItemsSource = null;
            dgStudent.ItemsSource = null;

            dgStudent.ItemsSource = (from q in App.students where q.GroupStr == cmbGroup.SelectedValue.ToString() select q).ToList();

            check = true;
        }

    }
}
