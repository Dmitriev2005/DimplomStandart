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

namespace DimplomStandart.Windows.DataReference.OrganisationWindows
{
    /// <summary>
    /// Логика взаимодействия для Organisation.xaml
    /// </summary>
    public partial class Organisation : Window
    {
        public Organisation()
        {
            InitializeComponent();
            DataContext = App.organization;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(App.organization.Id != "") { 
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand("UPDATE public.organisation SET name_i=@NameI," +
                    "name_p=@NameP,name_locality=@NameLocality,past_name=@PastName,region=@Region,director=@Director," +
                    "ogrn=@Ogrn::bigint,kpp=@Kpp::bigint,right_act=@RightAct",App.Connection());

                npgsqlCommand.Parameters.AddWithValue("@NameI", App.organization.NameI);
                npgsqlCommand.Parameters.AddWithValue("@NameP", App.organization.NameP);
                npgsqlCommand.Parameters.AddWithValue("@NameLocality", App.organization.NameLocality);
                npgsqlCommand.Parameters.AddWithValue("@PastName", App.organization.PastName);
                npgsqlCommand.Parameters.AddWithValue("@Region", App.organization.Region);
                npgsqlCommand.Parameters.AddWithValue("@Director", App.organization.Director);
                npgsqlCommand.Parameters.AddWithValue("@Ogrn", App.organization.Ogrn);
                npgsqlCommand.Parameters.AddWithValue("@Kpp", App.organization.Kpp);
                npgsqlCommand.Parameters.AddWithValue("@RightAct", App.organization.RightAct);
                npgsqlCommand.ExecuteNonQuery();
            }
            else
            {
                

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand("INSERT INTO public.organisation (name_i, name_p, name_locality, past_name, region, director,  ogrn, kpp, right_act) " +
 "VALUES (@NameI, @NameP, @NameLocality, @PastName, @Region, @Director, @Ogrn::bigint, @Kpp::bigint, @RightAct);", App.Connection());

                npgsqlCommand.Parameters.AddWithValue("@NameI", App.organization.NameI);
                npgsqlCommand.Parameters.AddWithValue("@NameP", App.organization.NameP);
                npgsqlCommand.Parameters.AddWithValue("@NameLocality", App.organization.NameLocality);
                npgsqlCommand.Parameters.AddWithValue("@PastName", App.organization.PastName);
                npgsqlCommand.Parameters.AddWithValue("@Region", App.organization.Region);
                npgsqlCommand.Parameters.AddWithValue("@Director", App.organization.Director);
                npgsqlCommand.Parameters.AddWithValue("@Ogrn", App.organization.Ogrn);
                npgsqlCommand.Parameters.AddWithValue("@Kpp", App.organization.Kpp);
                npgsqlCommand.Parameters.AddWithValue("@RightAct", App.organization.RightAct);

                npgsqlCommand.ExecuteNonQuery();
            }
            MessageBox.Show("Изменения сохранены!");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Данные будут потеряны", "Закрыть окно?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }
    }
}
