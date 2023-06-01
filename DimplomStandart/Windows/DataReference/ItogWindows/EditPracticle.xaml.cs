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
using DimplomStandart.Classes;
using Npgsql;

namespace DimplomStandart.Windows.DataReference.ItogWindows
{
    /// <summary>
    /// Логика взаимодействия для EditPracticle.xaml
    /// </summary>
    public partial class EditPracticle : Window
    {
        public EditPracticle(ItogDisciplineEntities itogDisciplineEntities)
        {
            InitializeComponent();

            ItogDisciplineEntities = itogDisciplineEntities;
            DataContext = ItogDisciplineEntities;
        }
        ItogDisciplineEntities ItogDisciplineEntities { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE public.student_discipline SET type_of_activity = @TypeOfActivity, use_methods_education = @UseMethodsEducation," +
                "place_passage = @PlacePassage WHERE id = @Id::bigint", App.Connection());

            command.Parameters.AddWithValue("@Id", ItogDisciplineEntities.Id);
            command.Parameters.AddWithValue("@TypeOfActivity", ItogDisciplineEntities.TypeOfActivity);
            command.Parameters.AddWithValue("@UseMethodsEducation", ItogDisciplineEntities.UseMethodsEducation);
            command.Parameters.AddWithValue("@PlacePassage", ItogDisciplineEntities.PlacePassage);

            command.ExecuteNonQuery();

            App.Refresh();
            Close();

        }
    }
}
