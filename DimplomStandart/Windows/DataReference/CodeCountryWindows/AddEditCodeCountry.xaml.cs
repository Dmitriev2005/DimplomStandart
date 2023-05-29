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

namespace DimplomStandart.Windows.DataReference.CodeCountryWindows
{
    /// <summary>
    /// Логика взаимодействия для AddEditCodeCountry.xaml
    /// </summary>
    public partial class AddEditCodeCountry : Window
    {
        public AddEditCodeCountry(CodeCountryEntities codeCountryEntities)
        {
            InitializeComponent();

            CodeCountry = codeCountryEntities;

            DataContext = CodeCountry;
        }
        CodeCountryEntities CodeCountry { get;set ; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CodeCountry.Id != "")
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                    "UPDATE public.code_country SET name=@Name, code=@Code WHERE id=@Id::bigint", App.Connection());

                npgsqlCommand.Parameters.AddWithValue("@Name", CodeCountry.Name);
                npgsqlCommand.Parameters.AddWithValue("@Code", CodeCountry.Code);
                npgsqlCommand.Parameters.AddWithValue("@Id", CodeCountry.Id);

                npgsqlCommand.ExecuteNonQuery();

                int index = App.codeCountrys.FindIndex(i => i.Id == CodeCountry.Id);
                App.codeCountrys[index] = CodeCountry;

                Close();
            }
            else
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                    "INSERT INTO public.code_country(name,code) values(@Name,@Code)", App.Connection());

                npgsqlCommand.Parameters.AddWithValue("@Name", CodeCountry.Name);
                npgsqlCommand.Parameters.AddWithValue("@Code", CodeCountry.Code);

                npgsqlCommand.ExecuteNonQuery();

                DataTableCreator dataTableCreator = new DataTableCreator();
                CodeCountry.Id = dataTableCreator.GiveMeDataTable("SELECT id from public.code_country MAX(id)").Rows[0][0].ToString();
                App.codeCountrys.Add(CodeCountry);
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
