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

namespace DimplomStandart.Windows.DataReference.SecretaryWindows
{
    /// <summary>
    /// Логика взаимодействия для AddSecretary.xaml
    /// </summary>
    public partial class AddSecretary : Window
    {
        public AddSecretary(SecretaryEntities secretaryEntities)
        {
            InitializeComponent();
            cmbSpecialisation.ItemsSource = (from q in App.specialisations select q.NameLong).ToList();
            this.secretaryEntities = secretaryEntities;
            if(secretaryEntities.Id != "")
            {
                tbName.Text = secretaryEntities.Name;
                cmbSpecialisation.SelectedItem = secretaryEntities.SpecialisationStr;
            }
        }
        SecretaryEntities secretaryEntities;
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            secretaryEntities.Name = tbName.Text;
            secretaryEntities.SpecialisationStr = cmbSpecialisation.SelectedItem.ToString();

            if(secretaryEntities.Id != "")
            {
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE public.secretary SET name = \'{secretaryEntities.Name}\', specialisation = \'{secretaryEntities.Specialisation}\' where id={secretaryEntities.Id}", App.Connection());
                command.ExecuteNonQuery();

                int index = App.secretaries.FindIndex(x => x.Id == secretaryEntities.Id);
                App.secretaries[index] = secretaryEntities;
                Close();
            }
            else
            {
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO public.secretary(name,specialisation) values(\'{secretaryEntities.Name}\',\'{secretaryEntities.Specialisation}\')", App.Connection()); 
                command.ExecuteNonQuery();
                secretaryEntities.Id = (int.Parse(App.secretaries.Max(p => p.Id)+1)).ToString();
                App.secretaries.Add(secretaryEntities);

                Close();


            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Данные будут потеряны", "Закрыть окно?",
               MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }
    }
}
