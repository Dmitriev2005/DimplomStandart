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
using DimplomStandart.Classes;
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
            cmbSpecialisation.ItemsSource = (from q in App.specialisations select q.SpecialisationYear).ToList();
            SecretaryEntities = secretaryEntities;

            DataContext = SecretaryEntities;
        }
        SecretaryEntities SecretaryEntities { get; set; }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           // SecretaryEntities.Specialisation = (from q in App.spe where q).ToList().Single();
            if(SecretaryEntities.Id != "")
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE public.secretary SET name = @Name, specialisation = @Specialisation::bigint WHERE id = @Id::bigint", App.Connection());
                command.Parameters.AddWithValue("@Name", SecretaryEntities.Name);
                command.Parameters.AddWithValue("@Specialisation", SecretaryEntities.Specialisation);
                command.Parameters.AddWithValue("@Id", SecretaryEntities.Id);
                command.ExecuteNonQuery();

                App.Refresh();
                Close();
            }
            else
            {
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO public.secretary(name,specialisation) values(@Name,@Specialisation::bigint)", App.Connection());
                command.Parameters.AddWithValue("@Name", SecretaryEntities.Name);
                command.Parameters.AddWithValue("@Specialisation", SecretaryEntities.Specialisation);

                command.ExecuteNonQuery();

                App.Refresh();
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
