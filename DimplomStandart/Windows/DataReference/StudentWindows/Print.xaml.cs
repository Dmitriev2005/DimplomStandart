using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using DimplomStandart.Entities;
using DimplomStandart.Classes;

namespace DimplomStandart.Windows.DataReference.StudentWindows
{
    /// <summary>
    /// Interaction logic for Print.xaml
    /// </summary>
    public partial class Print : Window
    {
        public Print(StudentEntities studentEntities)
        {
            InitializeComponent();
            StudentEntities = studentEntities;
        }
        public StudentEntities StudentEntities { get; set; }
        string choose;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var check = (RadioButton)sender;
            switch (check.Content)
            {
                case "Титул диплома":
                    choose = check.Content.ToString();
                    break;
                case "Приложение к диплому":
                    choose = check.Content.ToString();
                    break;

                default:
                    break;
            }
        }

        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            
            var items = new Dictionary<string, string>
                {
                    {"<surname>",StudentEntities.SurnameIm },
                    {"<name>", StudentEntities.NameIm },
                    {"<lastname>",StudentEntities.LastNameIm },
                    {"<longspecialisation>",StudentEntities.SpecialisationLong },
                    { "<organisation>",App.organization.NameI},
                    {"<shortspecialisation>",StudentEntities.SpecialisationShort },
                    {"<regnumber>",StudentEntities.RegNumberIssueDocument },
                    {"<startdate>",StudentEntities.DateDropStudent },
                    {"<director>", App.organization.Director },
                    {"<enddate>",StudentEntities.DateIssue }
                };

            WordHelper wordHelper = new WordHelper();
            wordHelper.Word(CopyShablonAndReturnPuth(), items);
        }
        private string CopyShablonAndReturnPuth()
        {
            string directory = "L:\\C#\\DimplomStandart\\DimplomStandart\\Resourses\\";
            string pathShablon = "";

            if (choose == "Титул диплома")
                pathShablon = directory + "Shablon.docx";
            else if (choose == "Приложение к диплому")
                pathShablon = directory + "ShablonApplication.docx";
            

            FileInfo file = new FileInfo(pathShablon);
            
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "word (*.docx)|*.docx"
            };
            dialog.FileName = StudentEntities.SurnameIm + StudentEntities.GroupStr;


            dialog.ShowDialog();
            string newPath = System.IO.Path.GetFullPath(dialog.FileName);
            file.CopyTo(newPath, true);

            return newPath;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Документ не будет создан", "Закрыть окно?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();

        }
    }
}
