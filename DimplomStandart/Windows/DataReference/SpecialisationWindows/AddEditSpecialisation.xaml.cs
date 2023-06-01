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
using System.Xml.Linq;
using DimplomStandart.Classes;
using DimplomStandart.Entities;
using Npgsql;

namespace DimplomStandart.Windows.DataReference.SpecialisationWindows
{
    /// <summary>
    /// Логика взаимодействия для AddEditSpecialisation.xaml
    /// </summary>
    public partial class AddEditSpecialisation : Window
    {
        public AddEditSpecialisation(SpecialisationEntities specialisation)
        {
            InitializeComponent();
            
            Specialisation = specialisation;
            DataContext = Specialisation;
        }
        public SpecialisationEntities Specialisation { get; set; }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Specialisation.Id != "")
            {
                //Если Id не пуст, тогда обновляю запись
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE public.specialisation SET name_short = \'{Specialisation.NameShort}\', name_long = \'{Specialisation.NameLong}\'," +
                    $"is_profession={Specialisation.IsProfession.ToString()}, normal_period_study=\'{Specialisation.NormalPeriodStudy}\', qualification=\'{Specialisation.Qualification}\', " +
                    $"direction=\'{Specialisation.Direction}\', year_specialisation=\'{Specialisation.YearSpecialisation}\' where id={Specialisation.Id}", App.Connection());
                
                command.ExecuteNonQuery();

                int index = App.specialisations.FindIndex(i=>i.Id== Specialisation.Id);
                App.specialisations[index] = Specialisation;

                Close();
            }
            else
            {
                //Если Id пуст, тогда вставляю запись
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO public.specialisation(name_short, name_long, year_specialisation, is_profession, normal_period_study, qualification,direction) values(\'{Specialisation.NameShort}\'," +
                    $"\'{Specialisation.NameLong}\',\'{Specialisation.YearSpecialisation}\',{Specialisation.IsProfession},\'{Specialisation.NormalPeriodStudy}\', \'{Specialisation.Qualification}\',\'{Specialisation.Direction}\')", App.Connection());
                command.ExecuteNonQuery();

                DataTableCreator dataTableCreator = new DataTableCreator();

                Specialisation.Id = dataTableCreator.GiveMeDataTable("SELECT MAX(id) FROM public.specialisation").Rows[0][0].ToString();
                App.specialisations.Add(Specialisation);

                Close();
            }
        }

        private void btnCencel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Данные будут потеряны", "Закрыть окно?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }
    }
}
