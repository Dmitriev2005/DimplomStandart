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
using DimplomStandart.Classes;

namespace DimplomStandart.Windows.DataReference.ItogWindows
{
    /// <summary>
    /// Логика взаимодействия для EditItogScore.xaml
    /// </summary>
    public partial class EditItogScore : Window
    {
        public EditItogScore(ItogDisciplineEntities itogDisciplineEntities)
        {
            InitializeComponent();

            DataTableCreator dataTableCreator = new DataTableCreator();

            var scors = dataTableCreator.GiveMeDataTable("SELECT unnest(enum_range(NULL::enum_score))");

            for (int i = 0; i < scors.Rows.Count; i++)
                cmbScore.Items.Add(scors.Rows[i][0]);
            
            ItogDisciplineEntities = itogDisciplineEntities;
            DataContext = ItogDisciplineEntities;
        }
        ItogDisciplineEntities ItogDisciplineEntities { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE public.student_discipline SET more_info = @MoreInfo, score = @Score::enum_score," +
                "is_course_work = @IsCourseWork WHERE id = @Id::bigint", App.Connection());

            command.Parameters.AddWithValue("@Id", ItogDisciplineEntities.Id);
            command.Parameters.AddWithValue("@MoreInfo",ItogDisciplineEntities.MoreInfo);
            command.Parameters.AddWithValue("@Score",ItogDisciplineEntities.Score);
            command.Parameters.AddWithValue("@IsCourseWork", ItogDisciplineEntities.IsCourseWork);

            command.ExecuteNonQuery();

            App.Refresh();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
