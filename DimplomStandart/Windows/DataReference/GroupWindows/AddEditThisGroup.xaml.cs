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
using DimplomStandart.Classes;
using DimplomStandart.Entities;
using DimplomStandart.Windows.DataReference.SecretaryWindows;
using Npgsql;
using NpgsqlTypes;

namespace DimplomStandart.Windows.DataReference.GroupWindows
{
    /// <summary>
    /// Логика взаимодействия для AddEditThisGroup.xaml
    /// </summary>
    public partial class AddEditThisGroup : Window
    {
        public AddEditThisGroup(GroupEntities groupEntities)
        {
            InitializeComponent();
            GroupEntities = groupEntities;

            foreach (var item in App.specialisations)
                cmbSpecialisation.Items.Add(item.SpecialisationYear);

             DataTableCreator dataTableCreator = new DataTableCreator();

            DataTable tableType = dataTableCreator.GiveMeDataTable("select unnest(enum_range(NULL::enum_level_training))");
            for (int i = 0; i < tableType.Rows.Count; i++)
                cmbLevelTraning.Items.Add(tableType.Rows[i][0]);


            tableType = dataTableCreator.GiveMeDataTable("select unnest(enum_range(NULL::enum_form_study))");
            for (int i = 0; i < tableType.Rows.Count; i++)
                cmbFormStudy.Items.Add(tableType.Rows[i][0]);

            DataContext = GroupEntities;

        }
        GroupEntities GroupEntities { get; set; }
       
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (GroupEntities.Id != "")
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE public.group SET name = @Name, specialisation = @Specialisation, " +
                    "level_training = @LevelTraining::enum_level_training, name_factory = @NameFactory, date_result_gek = @DateResultGek::date, " +
                    "date_issue_document = @DateIssueDocument::date, president_gek = @PresidentGek, secretary = @Secretary, " +
                    "form_study = @FormStudy::enum_form_study, protocol_gek = @ProtocolGek::bigint, order_drop_student = @OrderDropStudent, " +
                    "order_drop_student_date = @OrderDropStudentDate::date, more_info = @MoreInfo, note = @Note, " +
                    "is_load_to_frdo = @IsLoadToFrdo WHERE id = @Id;", App.Connection());

                command.Parameters.AddWithValue("@Name", GroupEntities.Name);
                command.Parameters.AddWithValue("@Specialisation", long.Parse(GroupEntities.Specialisation));
                command.Parameters.AddWithValue("@LevelTraining", GroupEntities.LevelTraning);
                command.Parameters.AddWithValue("@NameFactory", GroupEntities.NameFactory);
                command.Parameters.AddWithValue("@DateResultGek", GroupEntities.DateResultGek);
                command.Parameters.AddWithValue("@DateIssueDocument", GroupEntities.DateIssueDocument);
                command.Parameters.AddWithValue("@PresidentGek", GroupEntities.PresidentGek);
                command.Parameters.AddWithValue("@Secretary", int.Parse(GroupEntities.Secretary));
                command.Parameters.AddWithValue("@FormStudy", GroupEntities.FormStudy);
                command.Parameters.AddWithValue("@ProtocolGek", long.Parse(GroupEntities.ProtocolGek));
                command.Parameters.AddWithValue("@OrderDropStudent", GroupEntities.OrderDropStudent);
                command.Parameters.AddWithValue("@OrderDropStudentDate", GroupEntities.OrderDropStudentDate);
                command.Parameters.AddWithValue("@MoreInfo", GroupEntities.MoreInfo);
                command.Parameters.AddWithValue("@Note", GroupEntities.Note);
                command.Parameters.AddWithValue("@IsLoadToFrdo", GroupEntities.IsLoadToFrdo);
                command.Parameters.AddWithValue("@Id", int.Parse(GroupEntities.Id));
                command.ExecuteNonQuery();

                int index = App.groups.FindIndex(q => q.Id ==  GroupEntities.Id);
                App.groups[index] = GroupEntities;
                Close();
            }
            else
            {

                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.group(name, specialisation, level_training, name_factory, " +
                    "date_result_gek, date_issue_document, president_gek, secretary, form_study, protocol_gek, order_drop_student, " +
                    "order_drop_student_date, more_info, note, is_load_to_frdo) " +
                    "VALUES (@Name, @Specialisation, @LevelTraining::enum_level_training, @NameFactory, @DateResultGek::date, @DateIssueDocument::date, @PresidentGek, " +
                    "@Secretary, @FormStudy::enum_form_study, @ProtocolGek, @OrderDropStudent, @OrderDropStudentDate::date, @MoreInfo, @Note, @IsLoadToFrdo);",
                    App.Connection());

               

                command.Parameters.AddWithValue("@Name", GroupEntities.Name);
                command.Parameters.AddWithValue("@Specialisation", int.Parse(GroupEntities.Specialisation));
                command.Parameters.AddWithValue("@LevelTraining", NpgsqlDbType.Text, GroupEntities.LevelTraning.ToString());
                command.Parameters.AddWithValue("@NameFactory", GroupEntities.NameFactory);
                command.Parameters.AddWithValue("@DateResultGek", GroupEntities.DateResultGek);
                command.Parameters.AddWithValue("@DateIssueDocument", GroupEntities.DateIssueDocument);
                command.Parameters.AddWithValue("@PresidentGek", GroupEntities.PresidentGek);
                command.Parameters.AddWithValue("@Secretary", int.Parse(GroupEntities.Secretary));
                command.Parameters.AddWithValue("@FormStudy", NpgsqlDbType.Text, GroupEntities.FormStudy.ToString());
                command.Parameters.AddWithValue("@ProtocolGek", long.Parse(GroupEntities.ProtocolGek));
                command.Parameters.AddWithValue("@OrderDropStudent", GroupEntities.OrderDropStudent);
                command.Parameters.AddWithValue("@OrderDropStudentDate", GroupEntities.OrderDropStudentDate);
                command.Parameters.AddWithValue("@MoreInfo", GroupEntities.MoreInfo);
                command.Parameters.AddWithValue("@Note", GroupEntities.Note);
                command.Parameters.AddWithValue("@IsLoadToFrdo", GroupEntities.IsLoadToFrdo);

                command.ExecuteNonQuery();
                DataTableCreator dataTableCreator = new DataTableCreator();
                GroupEntities.Id = dataTableCreator.GiveMeDataTable("SELECT MAX(id) FROM public.group").Rows[0][0].ToString();
                App.groups.Add(GroupEntities);

                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Данные будут потеряны", "Закрыть окно?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }

        private void cmbSpecialisation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string selBuff = cmbSpecialisation.SelectedItem.ToString();
            var tempGroup = (from q in App.specialisations where q.SpecialisationYear == selBuff select q).ToList().Single();
            GroupEntities.Specialisation = tempGroup.Id;

            tbNormalSrok.Text = tempGroup.NormalPeriodStudy;
            tbDirection.Text = tempGroup.Direction;
            tbQualification.Text = tempGroup.Qualification;
            tbNameSpec.Text = tempGroup.NameLong;

            tbSecretary.Text = (from q in App.secretaries where tempGroup.Id == q.Specialisation select q.Name).ToList().Single();

            GroupEntities.Secretary = (from q in App.secretaries where tempGroup.Id == q.Specialisation select q.Id).ToList().Single();

        }

    }
}
