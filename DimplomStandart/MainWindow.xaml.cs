using DimplomStandart.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DimplomStandart.Windows.DataReference.DisciplineWindows;
using DimplomStandart.Entities;
using DimplomStandart.Windows.DataReference.SpecialisationWindows;
using DimplomStandart.Windows.DataReference.SecretaryWindows;
using System.Text.RegularExpressions;
using DimplomStandart.Windows.DataReference.GroupWindows;
using DimplomStandart.Windows.DataReference.OrganisationWindows;
using DimplomStandart.Windows.DataReference.StudentWindows;
using DimplomStandart.Windows.DataReference.CodeCountryWindows;
using DimplomStandart.Windows.DataReference.ItogWindows;
using DimplomStandart.Windows.Report;
using DimplomStandart.Windows.Report.FRDOWindows;

namespace DimplomStandart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataTableCreator table = new DataTableCreator();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            Height = MaxHeight;
            Width = MaxWidth;

           
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            switch (item.Header)
            {
                case "Дисциплины":
                    Discipline discipline = new Discipline();
                    discipline.Owner = this;
                    discipline.Show();
                    break;
                case "Специальности/профессии":
                    Specialisation specialisation = new Specialisation();
                    specialisation.Owner = this; 
                    specialisation.Show();
                    break;
                case "Секретари":
                    Secretary secretary = new Secretary();
                    secretary.Owner = this;
                    secretary.Show();
                    break;
                case "Учебные группы":
                    ThisGroup group = new ThisGroup();
                    group.Owner = this;
                    group.Show();
                    break;
                case "Организация":
                    Organisation organisation = new Organisation();
                    organisation.Owner = this;
                    organisation.Show();
                    break;
                case "Студенты":
                    Student student = new Student();
                    student.Owner = this;
                    student.Show();
                    break;
                case "Коды стран":
                    CodeCountry codeCountry = new CodeCountry();
                    codeCountry.Owner = this;
                    codeCountry.Show();
                    break;
                case "Итоговые оценки документов":
                    ItogScore score = new ItogScore();
                    score.Owner = this;
                    score.Show();
                    break;
                case "ФРДО":
                    FRDO frdo = new FRDO();
                    frdo.Owner = this;
                    frdo.ShowDialog();
                    break;




            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //DataTableCreator dataTableCreator = new DataTableCreator();
            //dgDiscipline.ItemsSource = dataTableCreator.GiveMeDataTable("select name, type from public.discipline").DefaultView;

        }
    }
}
