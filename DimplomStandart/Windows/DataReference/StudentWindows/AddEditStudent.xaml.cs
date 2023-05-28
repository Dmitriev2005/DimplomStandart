using DimplomStandart.Entities;
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
using System.Data;
using Npgsql;
using DimplomStandart.Windows.DataReference.CodeCountryWindows;
using Microsoft.VisualBasic;
using System.Linq;

namespace DimplomStandart.Windows.DataReference.StudentWindows
{
    /// <summary>
    /// Логика взаимодействия для AddEditStudent.xaml
    /// </summary>
    public partial class AddEditStudent : Window
    {
        public AddEditStudent(StudentEntities studentEntities)
        {
            InitializeComponent();
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable tabBuf = dataTableCreator.GiveMeDataTable("select  unnest(enum_range(NULL::enum_gender))");
            for (int i = 0; i < tabBuf.Rows.Count; i++)
                cmbGender.Items.Add(tabBuf.Rows[i][0].ToString());
            
            cmbGroup.ItemsSource = (from q in App.groups select q.Name).ToList();
            cmbNationality.ItemsSource = (from q in App.codeCountrys select q.Code).ToList();
                

            Student = studentEntities;
            DataContext = Student;
        }
        StudentEntities Student{ get; set; }
        List<TextBox> textBoxes;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Substitution();

            
            if (Student.Id != "")
            {
                if (chbIsContractCo.IsChecked.Value)
                {
                    NpgsqlCommand command = new NpgsqlCommand("UPDATE public.student SET group_s = @Group::bigint, surname_im = @SurnameIm, name_im = @NameIm," +
                        "lastname_im = @LastNameIm,surname_da = @SurnameDa, name_da = @NameDa,lastname_da = @LastNameDa, " +
                        "snils = @Snils::bigint, date_born = @DateBorn::date, place_born = @PlaceBorn, code_country = @CodeCountry::bigint," +
                        "series_past_document = @SeriesPastDocumentEducation, surname_in_document = @SurnamePastDocument," +
                        "registration_number = @RegNumberIssueDocument::bigint,contract_co = @ContractCo::bigint, gender = @Gender::enum_gender, year_start = @YearStart," +
                        "level_education = @LevelEducation, source_funding = @SourceFunding, date_contract_co = @DateContractCo::date," +
                        "name_organisation_employer = @NameOrganisationEmployer, ogrn_employer = @OgrnEmployer::bigint, kpp_employer = @KppEmployer::bigint," +
                        "subject_co = @SubjectCo, view_document = @ViewDocument, number_past_document = @NumberPastDocumentEducation::bigint, serial_and_number_issue_application = @SerialNumberIssueApplication," +
                        "serial_and_number_issue_document = @SerialNumberIssueDocument, year_end = @YearEnd, is_dublicate = @IsDublicate::boolean," +
                        "more_info = @MoreInfo,note = @Note, country_past_document = @CountryPastDocument, year_end_shcool = @YearEndSchool,is_contract_co = @IsContractCo   WHERE id = @Id::bigint", App.Connection());

                    command.Parameters.AddWithValue("@Group", Student.Group);
                    command.Parameters.AddWithValue("@SurnameIm", Student.SurnameIm);
                    command.Parameters.AddWithValue("@NameIm", Student.NameIm);
                    command.Parameters.AddWithValue("@LastNameIm", Student.LastNameIm);
                    command.Parameters.AddWithValue("@SurnameDa", Student.SurnameDa);
                    command.Parameters.AddWithValue("@NameDa", Student.NameDa);
                    command.Parameters.AddWithValue("@LastNameDa", Student.LastNameDa);
                    command.Parameters.AddWithValue("@Snils", Student.Snils);
                    command.Parameters.AddWithValue("@DateBorn", Student.DateBorn);
                    command.Parameters.AddWithValue("@PlaceBorn", Student.PlaceBorn);
                    command.Parameters.AddWithValue("@CodeCountry", Student.CodeCountry);
                    command.Parameters.AddWithValue("@ViewDocument", Student.ViewDocument);
                    command.Parameters.AddWithValue("@SeriesPastDocumentEducation", Student.SeriesPastDocumentEducation);
                    command.Parameters.AddWithValue("@NumberPastDocumentEducation", Student.NumberPastDocumentEducation);
                    command.Parameters.AddWithValue("@SurnamePastDocument", Student.SurnamePastDocument);
                    command.Parameters.AddWithValue("@RegNumberIssueDocument", Student.RegNumberIssueDocument);
                    command.Parameters.AddWithValue("@SerialNumberIssueDocument", Student.SerialNumberIssueDocument);
                    command.Parameters.AddWithValue("@SerialNumberIssueApplication", Student.SerialNumberIssueApplication);
                    command.Parameters.AddWithValue("@Gender", Student.Gender);
                    command.Parameters.AddWithValue("@YearStart", Student.YearStart);
                    command.Parameters.AddWithValue("@YearEnd", Student.YearEnd);
                    command.Parameters.AddWithValue("@LevelEducation", Student.LevelEducation);
                    command.Parameters.AddWithValue("@SourceFunding", Student.SourceFunding);
                    command.Parameters.AddWithValue("@ContractCo", Student.ContractCo);
                    command.Parameters.AddWithValue("@DateContractCo", Student.DateContractCo);
                    command.Parameters.AddWithValue("@NameOrganisationEmployer", Student.NameOrganisationEmployer);
                    command.Parameters.AddWithValue("@OgrnEmployer", Student.OgrnEmployer);
                    command.Parameters.AddWithValue("@KppEmployer", Student.KppEmployer);
                    command.Parameters.AddWithValue("@SubjectCo", Student.SubjectCo);
                    command.Parameters.AddWithValue("@IsDublicate", Student.IsDublicate);
                    command.Parameters.AddWithValue("@MoreInfo", Student.MoreInfo);
                    command.Parameters.AddWithValue("@Note", Student.Note);
                    command.Parameters.AddWithValue("@Id", Student.Id);
                    command.Parameters.AddWithValue("@CountryPastDocument", Student.CountryPastDocument);
                    command.Parameters.AddWithValue("@YearEndSchool", Student.YearEndSchool);
                    command.Parameters.AddWithValue("@IsContractCo", Student.IsContractCo);


                    command.ExecuteNonQuery();

                }
                else
                {
                    NpgsqlCommand command = new NpgsqlCommand("UPDATE public.student SET group_s = @Group::bigint, surname_im = @SurnameIm, name_im = @NameIm," +
                        "lastname_im = @LastNameIm,surname_da = @SurnameDa, name_da = @NameDa,lastname_da = @LastNameDa, " +
                        "snils = @Snils::bigint, date_born = @DateBorn::date, place_born = @PlaceBorn, code_country = @CodeCountry::bigint," +
                        "series_past_document = @SeriesPastDocumentEducation, surname_in_document = @SurnamePastDocument," +
                        "registration_number = @RegNumberIssueDocument::bigint, gender = @Gender::enum_gender, year_start = @YearStart," +
                        "level_education = @LevelEducation, source_funding = @SourceFunding, view_document = @ViewDocument, number_past_document = @NumberPastDocumentEducation::bigint, serial_and_number_issue_application = @SerialNumberIssueApplication," +
                        "serial_and_number_issue_document = @SerialNumberIssueDocument, year_end = @YearEnd, is_dublicate = @IsDublicate::boolean," +
                        "more_info = @MoreInfo,note = @Note, country_past_document = @CountryPastDocument, year_end_shcool = @YearEndSchool, is_contract_co = @IsContractCo  WHERE id = @Id::bigint", App.Connection());

                    command.Parameters.AddWithValue("@Group", Student.Group);
                    command.Parameters.AddWithValue("@SurnameIm", Student.SurnameIm);
                    command.Parameters.AddWithValue("@NameIm", Student.NameIm);
                    command.Parameters.AddWithValue("@LastNameIm", Student.LastNameIm);
                    command.Parameters.AddWithValue("@SurnameDa", Student.SurnameDa);
                    command.Parameters.AddWithValue("@NameDa", Student.NameDa);
                    command.Parameters.AddWithValue("@LastNameDa", Student.LastNameDa);
                    command.Parameters.AddWithValue("@Snils", Student.Snils);
                    command.Parameters.AddWithValue("@DateBorn", Student.DateBorn);
                    command.Parameters.AddWithValue("@PlaceBorn", Student.PlaceBorn);
                    command.Parameters.AddWithValue("@CodeCountry", Student.CodeCountry);
                    command.Parameters.AddWithValue("@ViewDocument", Student.ViewDocument);
                    command.Parameters.AddWithValue("@SeriesPastDocumentEducation", Student.SeriesPastDocumentEducation);
                    command.Parameters.AddWithValue("@NumberPastDocumentEducation", Student.NumberPastDocumentEducation);
                    command.Parameters.AddWithValue("@SurnamePastDocument", Student.SurnamePastDocument);
                    command.Parameters.AddWithValue("@RegNumberIssueDocument", Student.RegNumberIssueDocument);
                    command.Parameters.AddWithValue("@SerialNumberIssueDocument", Student.SerialNumberIssueDocument);
                    command.Parameters.AddWithValue("@SerialNumberIssueApplication", Student.SerialNumberIssueApplication);
                    command.Parameters.AddWithValue("@Gender", Student.Gender);
                    command.Parameters.AddWithValue("@YearStart", Student.YearStart);
                    command.Parameters.AddWithValue("@YearEnd", Student.YearEnd);
                    command.Parameters.AddWithValue("@LevelEducation", Student.LevelEducation);
                    command.Parameters.AddWithValue("@SourceFunding", Student.SourceFunding);
                    command.Parameters.AddWithValue("@IsDublicate", Student.IsDublicate);
                    command.Parameters.AddWithValue("@MoreInfo", Student.MoreInfo);
                    command.Parameters.AddWithValue("@Note", Student.Note);
                    command.Parameters.AddWithValue("@Id", Student.Id);
                    command.Parameters.AddWithValue("@CountryPastDocument", Student.CountryPastDocument);
                    command.Parameters.AddWithValue("@YearEndSchool", Student.YearEndSchool);
                    command.Parameters.AddWithValue("@IsContractCo", Student.IsContractCo);

                    command.ExecuteNonQuery();

                }

                int index = App.students.FindIndex(q=>q.Id==Student.Id);
                App.students[index] = Student;
                Close();
            }
            else
            {
                if (chbIsContractCo.IsChecked.Value)
                {
                    NpgsqlCommand command = new NpgsqlCommand(
                        "INSERT INTO public.student(group_s, surname_im, name_im,lastname_im," +
                        "surname_da, name_da,lastname_da," +
                        "snils, date_born, place_born, code_country," +
                        "series_past_document, surname_in_document," +
                        "registration_number,contract_co, " +
                        "gender, year_start, level_education," +
                        "source_funding, date_contract_co, name_organisation_employer," +
                        "ogrn_employer, kpp_employer," +
                        "subject_co, view_document, number_past_document, " +
                        "serial_and_number_issue_application,serial_and_number_issue_document, " +
                        "year_end, is_dublicate, more_info, note," +
                        "country_past_document, year_end_shcool,is_contract_co) " +
                        "values(@Group::bigint,@SurnameIm,@NameIm,@LastNameIm,@SurnameDa,@NameDa,@LastNameDa," +
                        "@Snils::bigint,@DateBorn::date,@PlaceBorn,@CodeCountry::bigint,@SeriesPastDocumentEducation," +
                        "@SurnamePastDocument,@RegNumberIssueDocument::bigint,@ContractCo::bigint,@Gender::enum_gender," +
                        "@YearStart,@LevelEducation,@SourceFunding,@DateContractCo::date, @NameOrganisationEmployer," +
                        "@OgrnEmployer::bigint,@KppEmployer::bigint,@SubjectCo,@ViewDocument,@NumberPastDocumentEducation::bigint," +
                        "@SerialNumberIssueApplication,@SerialNumberIssueDocument,@YearEnd, @IsDublicate::boolean," +
                        "@MoreInfo,@Note,@CountryPastDocument,@YearEndSchool,@IsContractCo)", App.Connection());


                    command.Parameters.AddWithValue("@Group", Student.Group);
                    command.Parameters.AddWithValue("@SurnameIm", Student.SurnameIm);
                    command.Parameters.AddWithValue("@NameIm", Student.NameIm);
                    command.Parameters.AddWithValue("@LastNameIm", Student.LastNameIm);
                    command.Parameters.AddWithValue("@SurnameDa", Student.SurnameDa);
                    command.Parameters.AddWithValue("@NameDa", Student.NameDa);
                    command.Parameters.AddWithValue("@LastNameDa", Student.LastNameDa);
                    command.Parameters.AddWithValue("@Snils", Student.Snils);
                    command.Parameters.AddWithValue("@DateBorn", Student.DateBorn);
                    command.Parameters.AddWithValue("@PlaceBorn", Student.PlaceBorn);
                    command.Parameters.AddWithValue("@CodeCountry", Student.CodeCountry);
                    command.Parameters.AddWithValue("@ViewDocument", Student.ViewDocument);
                    command.Parameters.AddWithValue("@SeriesPastDocumentEducation", Student.SeriesPastDocumentEducation);
                    command.Parameters.AddWithValue("@NumberPastDocumentEducation", Student.NumberPastDocumentEducation);
                    command.Parameters.AddWithValue("@SurnamePastDocument", Student.SurnamePastDocument);
                    command.Parameters.AddWithValue("@RegNumberIssueDocument", Student.RegNumberIssueDocument);
                    command.Parameters.AddWithValue("@SerialNumberIssueDocument", Student.SerialNumberIssueDocument);
                    command.Parameters.AddWithValue("@SerialNumberIssueApplication", Student.SerialNumberIssueApplication);
                    command.Parameters.AddWithValue("@Gender", Student.Gender);
                    command.Parameters.AddWithValue("@YearStart", Student.YearStart);
                    command.Parameters.AddWithValue("@YearEnd", Student.YearEnd);
                    command.Parameters.AddWithValue("@LevelEducation", Student.LevelEducation);
                    command.Parameters.AddWithValue("@SourceFunding", Student.SourceFunding);
                    command.Parameters.AddWithValue("@ContractCo", Student.ContractCo);
                    command.Parameters.AddWithValue("@DateContractCo", Student.DateContractCo);
                    command.Parameters.AddWithValue("@NameOrganisationEmployer", Student.NameOrganisationEmployer);
                    command.Parameters.AddWithValue("@OgrnEmployer", Student.OgrnEmployer);
                    command.Parameters.AddWithValue("@KppEmployer", Student.KppEmployer);
                    command.Parameters.AddWithValue("@SubjectCo", Student.SubjectCo);
                    command.Parameters.AddWithValue("@IsDublicate", Student.IsDublicate);
                    command.Parameters.AddWithValue("@MoreInfo", Student.MoreInfo);
                    command.Parameters.AddWithValue("@Note", Student.Note);
                    command.Parameters.AddWithValue("@Id", Student.Id);
                    command.Parameters.AddWithValue("@CountryPastDocument", Student.CountryPastDocument);
                    command.Parameters.AddWithValue("@YearEndSchool", Student.YearEndSchool);
                    command.Parameters.AddWithValue("@IsContractCo", Student.IsContractCo);


                    command.ExecuteNonQuery();

                }
                else
                {
                    NpgsqlCommand command = new NpgsqlCommand(
                    "INSERT INTO public.student(group_s, surname_im, name_im,lastname_im," +
                    "surname_da, name_da,lastname_da," +
                    "snils, date_born, place_born, code_country," +
                    "series_past_document, surname_in_document," +
                    "registration_number," +
                    "gender, year_start, level_education," +
                    "source_funding,"+
                    "view_document, number_past_document, " +
                    "serial_and_number_issue_application,serial_and_number_issue_document, " +
                    "year_end, is_dublicate, more_info, note," +
                    "country_past_document, year_end_shcool,is_contract_co) " +
                    "values(@Group::bigint,@SurnameIm,@NameIm,@LastNameIm,@SurnameDa,@NameDa,@LastNameDa," +
                    "@Snils::bigint,@DateBorn::date,@PlaceBorn,@CodeCountry::bigint,@SeriesPastDocumentEducation," +
                    "@SurnamePastDocument,@RegNumberIssueDocument::bigint,@Gender::enum_gender," +
                    "@YearStart,@LevelEducation,@SourceFunding," +
                    "@ViewDocument,@NumberPastDocumentEducation::bigint," +
                    "@SerialNumberIssueApplication,@SerialNumberIssueDocument,@YearEnd, @IsDublicate::boolean," +
                    "@MoreInfo,@Note,@CountryPastDocument,@YearEndSchool,@IsContractCo)", App.Connection());


                    command.Parameters.AddWithValue("@Group", Student.Group);
                    command.Parameters.AddWithValue("@SurnameIm", Student.SurnameIm);
                    command.Parameters.AddWithValue("@NameIm", Student.NameIm);
                    command.Parameters.AddWithValue("@LastNameIm", Student.LastNameIm);
                    command.Parameters.AddWithValue("@SurnameDa", Student.SurnameDa);
                    command.Parameters.AddWithValue("@NameDa", Student.NameDa);
                    command.Parameters.AddWithValue("@LastNameDa", Student.LastNameDa);
                    command.Parameters.AddWithValue("@Snils", Student.Snils);
                    command.Parameters.AddWithValue("@DateBorn", Student.DateBorn);
                    command.Parameters.AddWithValue("@PlaceBorn", Student.PlaceBorn);
                    command.Parameters.AddWithValue("@CodeCountry", Student.CodeCountry);
                    command.Parameters.AddWithValue("@ViewDocument", Student.ViewDocument);
                    command.Parameters.AddWithValue("@SeriesPastDocumentEducation", Student.SeriesPastDocumentEducation);
                    command.Parameters.AddWithValue("@NumberPastDocumentEducation", Student.NumberPastDocumentEducation);
                    command.Parameters.AddWithValue("@SurnamePastDocument", Student.SurnamePastDocument);
                    command.Parameters.AddWithValue("@RegNumberIssueDocument", Student.RegNumberIssueDocument);
                    command.Parameters.AddWithValue("@SerialNumberIssueDocument", Student.SerialNumberIssueDocument);
                    command.Parameters.AddWithValue("@SerialNumberIssueApplication", Student.SerialNumberIssueApplication);
                    command.Parameters.AddWithValue("@Gender", Student.Gender);
                    command.Parameters.AddWithValue("@YearStart", Student.YearStart);
                    command.Parameters.AddWithValue("@YearEnd", Student.YearEnd);
                    command.Parameters.AddWithValue("@LevelEducation", Student.LevelEducation);
                    command.Parameters.AddWithValue("@SourceFunding", Student.SourceFunding);
                    command.Parameters.AddWithValue("@IsDublicate", Student.IsDublicate);
                    command.Parameters.AddWithValue("@MoreInfo", Student.MoreInfo);
                    command.Parameters.AddWithValue("@Note", Student.Note);
                    command.Parameters.AddWithValue("@Id", Student.Id);
                    command.Parameters.AddWithValue("@CountryPastDocument", Student.CountryPastDocument);
                    command.Parameters.AddWithValue("@YearEndSchool", Student.YearEndSchool);
                    command.Parameters.AddWithValue("@IsContractCo", Student.IsContractCo);




                    command.ExecuteNonQuery();

                }


                DataTableCreator dataTableCreator = new DataTableCreator();
                var tabId = dataTableCreator.GiveMeDataTable("SELECT MAX(id) FROM public.student");
                Student.Id = (int.Parse(tabId.Rows[0][0].ToString()) + 1).ToString();

                App.students.Add(Student);
                Close();
            }
        }
        private void Substitution()
        {
            Student.Group = (from q in App.groups where q.Name==cmbGroup.SelectedItem.ToString() select q.Id).ToList().Single();
            Student.CodeCountry = (from q in App.codeCountrys where q.Code == cmbNationality.SelectedItem.ToString() select q.Id).ToList().Single();

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Данные будут потеряны", "Закрыть окно?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();

        }

        private void chbIsContractCo_Checked(object sender, RoutedEventArgs e)
        {
            stpContractCo.IsEnabled = true;
        }

        private void chbIsContractCo_Unchecked(object sender, RoutedEventArgs e)
        {
            stpContractCo.IsEnabled = false;
        }
    }
}
