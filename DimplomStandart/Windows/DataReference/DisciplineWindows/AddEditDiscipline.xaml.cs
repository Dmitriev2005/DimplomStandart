using DimplomStandart.Classes;
using DimplomStandart.Entities;
using DimplomStandart.Windows.DataReference.StudentWindows;
using Npgsql;
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

namespace DimplomStandart.Windows.DataReference.DisciplineWindows
{
    /// <summary>
    /// Логика взаимодействия для AddEditDiscipline.xaml
    /// </summary>
    public partial class AddEditDiscipline : Window
    {
        public AddEditDiscipline(DisciplineEntities disciplineEntities)
        {
            InitializeComponent();
            //Добавляю в ComboBox типы
            DataTableCreator dataTableCreator = new DataTableCreator();

            DataTable tableType = dataTableCreator.GiveMeDataTable("select unnest(enum_range(NULL::enum_type_discipline))");
            for (int i = 0; i < tableType.Rows.Count; i++)
                cmbType.Items.Add(tableType.Rows[i][0]);

            tableType = dataTableCreator.GiveMeDataTable("select name from public.group");
            for (int i = 0; i < tableType.Rows.Count; i++)
                cmbGroup.Items.Add(tableType.Rows[i][0]);

            if (disciplineEntities.Id != "")
                idStudentDiscipline = (from q in App.itogDisciplines where q.IdDiscipline==DisciplineEntities.Id select q.Id).ToList().Single();

            DisciplineEntities = disciplineEntities;
            DataContext = DisciplineEntities;
        }
        private string idStudentDiscipline;
        DisciplineEntities DisciplineEntities { get; set; }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            

            if (DisciplineEntities.Id!="")
            {
                //Если Id не пуст, тогда обновляю запись
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE public.discipline SET name = @Name, type = @Type::enum_type_discipline, " +
                    $"id_group = @IdGroup::bigint, count_hour = @CountHour::bigint where id = @Id::bigint", App.Connection());
                command.Parameters.AddWithValue("@Id", DisciplineEntities.Id);
                command.Parameters.AddWithValue("@Name", DisciplineEntities.Name);
                command.Parameters.AddWithValue("@Type", DisciplineEntities.Type);
                command.Parameters.AddWithValue("@IdGroup", DisciplineEntities.IdGroup);
                command.Parameters.AddWithValue("@CountHour", DisciplineEntities.CountHour);

                command.ExecuteNonQuery();

                for (int i = 0; i < App.students.Count; i++)
                {
                    string idStudent = App.students[i].Group == DisciplineEntities.IdGroup ? App.students[i].Id : "";

                    if (idStudent != "")
                    {
                        command = new NpgsqlCommand($"UPDATE public.student_discipline SET id_student = @IdStudent::bigserial, id_discipline = @IdDiscipline::bigserial, " +
                            $"id_group = @IdGroup::bigint where id = @Id::bigint", App.Connection());
                        command.Parameters.AddWithValue("@Id", idStudentDiscipline);
                        command.Parameters.AddWithValue("@IdDiscipline", DisciplineEntities.Id);
                        command.Parameters.AddWithValue("@IdStudent", idStudent);
                        command.Parameters.AddWithValue("@IdGroup", DisciplineEntities.IdGroup);

                        command.ExecuteNonQuery();
                    }
                }

                int index = App.disciplines.FindIndex(i => i.Id== DisciplineEntities.Id);
                App.disciplines[index] = DisciplineEntities;

                App.LoadItogDisciplines();
                Close(); 
            }
            else
            {
                //Если Id пуст, тогда вставляю запись
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO public.discipline(name,type,id_group,count_hour) values(@Name,@Type::enum_type_discipline," +
                    $"@IdGroup::bigint,@CountHour::bigint)",App.Connection());
                command.Parameters.AddWithValue("@Name", DisciplineEntities.Name);
                command.Parameters.AddWithValue("@Type", DisciplineEntities.Type);
                command.Parameters.AddWithValue("@IdGroup", DisciplineEntities.IdGroup);
                command.Parameters.AddWithValue("@CountHour", DisciplineEntities.CountHour);

                command.ExecuteNonQuery();

                DataTableCreator dataTableCreator = new DataTableCreator();
                var tabId = dataTableCreator.GiveMeDataTable("SELECT MAX(id) FROM public.discipline");
                DisciplineEntities.Id = (int.Parse(tabId.Rows[0][0].ToString()) + 1).ToString();

                for (int i = 0; i < App.students.Count; i++)
                {
                    string idStudent = App.students[i].Group == DisciplineEntities.IdGroup ? App.students[i].Id : "";
                    if (idStudent != "")
                    {
                        command = new NpgsqlCommand("INSERT INTO public.student_discipline(id_student, id_group, id_discipline)values(" +
                            "@IdStudent::bigint,@IdGroup::bigint,@IdDiscipline::bigint)", App.Connection());
                        command.Parameters.AddWithValue("@IdDiscipline", DisciplineEntities.Id);
                        command.Parameters.AddWithValue("@IdStudent", idStudent);
                        command.Parameters.AddWithValue("@IdGroup", DisciplineEntities.IdGroup);

                        command.ExecuteNonQuery();

                    }

                }


                App.LoadItogDisciplines();
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            if(MessageBox.Show("Данные будут потеряны", "Закрыть окно?", 
                MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                Close();
           
                
        }

        
    }
}
