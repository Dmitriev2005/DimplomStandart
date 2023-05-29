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

            tableType = dataTableCreator.GiveMeDataTable("select name_short from public.specialisation");
            for (int i = 0; i < tableType.Rows.Count; i++)
                cmbGroup.Items.Add(tableType.Rows[i][0]);

           // if (disciplineEntities.Id != "")
             // idStudentDiscipline = (from q in App.itogDisciplines where q.IdDiscipline== disciplineEntities.Id select q.Id).ToList().Single();

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
                    $"id_specialisation = @IdSpecialisation::bigint, count_hour = @CountHour::bigint where id = @Id::bigint", App.Connection());
                command.Parameters.AddWithValue("@Id", DisciplineEntities.Id);
                command.Parameters.AddWithValue("@Name", DisciplineEntities.Name);
                command.Parameters.AddWithValue("@Type", DisciplineEntities.Type);
                command.Parameters.AddWithValue("@IdSpecialisation", DisciplineEntities.IdSpecialisation);
                command.Parameters.AddWithValue("@CountHour", DisciplineEntities.CountHour);

                command.ExecuteNonQuery();

               
                List<String> bufferStudentsId = new List<String>();

                for (int i = 0; i < App.students.Count; i++)
                {
                    string idGroup = App.groups[i].Specialisation == DisciplineEntities.IdSpecialisation ? App.groups[i].Id : "";
                    if (idGroup != "")
                        foreach (var item in App.students)
                            if (!bufferStudentsId.Contains(item.Id) && item.Group == idGroup)
                                bufferStudentsId.Add(item.Id);
                }

                foreach (var idStudent in bufferStudentsId)
                {
                    command = new NpgsqlCommand("UPDATE public.student_discipline SET id_student = @IdStudent::bigint, id_discipline = @IdDiscipline::bigint", App.Connection());
                    command.Parameters.AddWithValue("@IdDiscipline", DisciplineEntities.Id);
                    command.Parameters.AddWithValue("@IdStudent", idStudent);

                    command.ExecuteNonQuery();

                }


                int index = App.disciplines.FindIndex(i => i.Id== DisciplineEntities.Id);
                App.disciplines[index] = DisciplineEntities;

                App.LoadItogDisciplines();
                Close(); 
            }
            else
            {
                //Если Id пуст, тогда вставляю запись
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO public.discipline(name,type,id_specialisation,count_hour) values(@Name,@Type::enum_type_discipline," +
                    $"@IdSpecialisation::bigint,@CountHour::bigint)",App.Connection());
                command.Parameters.AddWithValue("@Name", DisciplineEntities.Name);
                command.Parameters.AddWithValue("@Type", DisciplineEntities.Type);
                command.Parameters.AddWithValue("@IdGroup", DisciplineEntities.IdSpecialisation);
                command.Parameters.AddWithValue("@IdSpecialisation", DisciplineEntities.IdSpecialisation);
                command.Parameters.AddWithValue("@CountHour", DisciplineEntities.CountHour);

                command.ExecuteNonQuery();

                DataTableCreator dataTableCreator = new DataTableCreator();
                DisciplineEntities.Id = dataTableCreator.GiveMeDataTable("SELECT MAX(id) FROM public.discipline").Rows[0][0].ToString();
                App.disciplines.Add(DisciplineEntities);
                List<String> bufferStudentsId = new List<String>();

                for (int i = 0; i < App.students.Count; i++)
                {
                    string idGroup = App.groups[i].Specialisation == DisciplineEntities.IdSpecialisation ? App.groups[i].Id : "";
                    if (idGroup!="")
                        foreach (var item in App.students)
                            if(!bufferStudentsId.Contains(item.Id)&&item.Group==idGroup)
                                bufferStudentsId.Add(item.Id);
                }
                foreach (var idStudent in bufferStudentsId)
                {
                    command = new NpgsqlCommand("INSERT INTO public.student_discipline(id_student, id_discipline)values(" +
                        "@IdStudent::bigint,@IdDiscipline::bigint)", App.Connection());
                    command.Parameters.AddWithValue("@IdDiscipline", DisciplineEntities.Id);
                    command.Parameters.AddWithValue("@IdStudent", idStudent);

                    command.ExecuteNonQuery();

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
