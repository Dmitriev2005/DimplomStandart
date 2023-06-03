using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DimplomStandart.Classes;
using DimplomStandart.Entities;
using DimplomStandart.Windows.DataReference.DisciplineWindows;
using Npgsql;

namespace DimplomStandart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Refresh();
        }
        public static List<DisciplineEntities> disciplines = new List<DisciplineEntities>();
        public static List<SpecialisationEntities> specialisations = new List<SpecialisationEntities>();
        public static List<SecretaryEntities> secretaries = new List<SecretaryEntities>();
        public static List<GroupEntities> groups = new List<GroupEntities>();
        public static OrganisationEntities organization = new OrganisationEntities();
        public static List<StudentEntities> students = new List<StudentEntities>();
        public static List<CodeCountryEntities> codeCountrys = new List<CodeCountryEntities>();
        public static List<ItogDisciplineEntities> itogDisciplines = new List<ItogDisciplineEntities>();
        public static NpgsqlConnection Connection()
        {
            
            NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=rtme;Database=diplone;");
            try
            {
                connection.Open();
            }
            catch { }

            return connection;
           
        } 
        private static void LoadDisciplines()
        {
            disciplines.Clear();
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable table = dataTableCreator.GiveMeDataTable("select * from public.discipline");

           
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DisciplineEntities disciplineEntities = new DisciplineEntities(id:table.Rows[i]["id"].ToString(), 
                    name:table.Rows[i]["name"].ToString(), type:table.Rows[i]["type"].ToString(),
                    idSpecialisation: table.Rows[i]["id_specialisation"].ToString(), countHour: table.Rows[i]["count_hour"].ToString());
                App.disciplines.Add(disciplineEntities);
            }
        }
        private static void LoadSpecialisations()
        {
            App.specialisations.Clear();
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable table = dataTableCreator.GiveMeDataTable("select * from public.specialisation");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                SpecialisationEntities specialisationEntities = new SpecialisationEntities(table.Rows[i][4].ToString(),
                table.Rows[i][0].ToString(), table.Rows[i][1].ToString(), table.Rows[i][5].ToString(),
                    table.Rows[i][3].ToString(), Convert.ToBoolean(table.Rows[i][2]), table.Rows[i][6].ToString(),table.Rows[i][7].ToString());
                App.specialisations.Add(specialisationEntities);
            }
        }
        private static void LoadSecretaries() {
            App.secretaries.Clear();
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable table = dataTableCreator.GiveMeDataTable("select * from public.secretary");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                SecretaryEntities secretaryEntities = new SecretaryEntities(id:table.Rows[i]["id"].ToString(), name: table.Rows[i]["name"].ToString(), 
                    specialisation: table.Rows[i]["specialisation"].ToString());
                App.secretaries.Add(secretaryEntities);
            }
        }
        private static void LoadGroups()
        {
            App.groups.Clear();
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable table = dataTableCreator.GiveMeDataTable("select * from public.group");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DateTime dateIssueDocumentSQL, dateResultGekSQL, orderDropStudentDateSQL = new DateTime();

                dateResultGekSQL = DateTime.Parse(table.Rows[i]["date_result_gek"].ToString());
                dateIssueDocumentSQL = DateTime.Parse(table.Rows[i]["date_issue_document"].ToString());
                orderDropStudentDateSQL = DateTime.Parse(table.Rows[i]["order_drop_student_date"].ToString());

                GroupEntities groupEntities = new GroupEntities(id:table.Rows[i]["id"].ToString(), name:table.Rows[i]["name"].ToString(), specialisation:table.Rows[i]["specialisation"].ToString(), 
                    levelTraning:table.Rows[i]["level_training"].ToString(), nameFactory:table.Rows[i]["name_factory"].ToString(), dateResultGek:dateResultGekSQL, dateIssueDocument:dateIssueDocumentSQL,
                    presidentGek:table.Rows[i]["president_gek"].ToString(), secretary:table.Rows[i]["secretary"].ToString(), formStudy:table.Rows[i]["form_study"].ToString(), protocolGek:table.Rows[i]["protocol_gek"].ToString(), orderDropStudent:table.Rows[i]["order_drop_student"].ToString(),
                    orderDropStudentDate:orderDropStudentDateSQL, moreInfo:table.Rows[i]["more_info"].ToString(), note:table.Rows[i]["note"].ToString(), isLoadToFrdo:Convert.ToBoolean(table.Rows[i]["is_load_to_frdo"]));
                App.groups.Add(groupEntities);
            }
        }
        private static void LoadOrganisation()
        {
            App.organization = new OrganisationEntities();
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable table = dataTableCreator.GiveMeDataTable("select * from public.organisation");

            if(table.Rows.Count > 0)
                organization = new OrganisationEntities(id: table.Rows[0]["id"].ToString(),
                    nameI: table.Rows[0]["name_i"].ToString(), nameP: table.Rows[0]["name_p"].ToString(), nameLocality: table.Rows[0]["name_locality"].ToString(),
                    pastName: table.Rows[0]["past_name"].ToString(), region: table.Rows[0]["region"].ToString(), director: table.Rows[0]["director"].ToString(),
                    ogrn: table.Rows[0]["ogrn"].ToString(), kpp: table.Rows[0]["kpp"].ToString(), rightAct: table.Rows[0]["right_act"].ToString());
        }
        private static void LoadStudents()
        {
            students.Clear();
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable table = dataTableCreator.GiveMeDataTable("select * from public.student");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DateTime dateBornSQL = new DateTime();
                DateTime dateContractCoSQL = new DateTime();

                dateBornSQL = DateTime.Parse(table.Rows[i]["date_born"].ToString());

                if (table.Rows[i]["date_contract_co"].ToString() != "")
                    dateContractCoSQL = DateTime.Parse(table.Rows[i]["date_contract_co"].ToString());


                StudentEntities studentEntities = new StudentEntities(
                      id: table.Rows[i]["id"].ToString(), surnameIm: table.Rows[i]["surname_im"].ToString(),
                      nameIm: table.Rows[i]["name_im"].ToString(), lastNameIm: table.Rows[i]["lastname_im"].ToString(),
                      surnameDa: table.Rows[i]["surname_da"].ToString(), nameDa: table.Rows[i]["name_da"].ToString(),
                      lastNameDa: table.Rows[i]["lastname_da"].ToString(), snils: table.Rows[i]["snils"].ToString(),
                      dateBorn: dateBornSQL, placeBorn: table.Rows[i]["place_born"].ToString(),
                      codeCountry: table.Rows[i]["code_country"].ToString(), viewDocument: table.Rows[i]["view_document"].ToString(),
                      seriesPastDocumentEducation: table.Rows[i]["series_past_document"].ToString(), numberPastDocumentEducation: table.Rows[i]["number_past_document"].ToString(),
                      surnamePastDocument: table.Rows[i]["surname_in_document"].ToString(), regNumberIssueDocument: table.Rows[i]["registration_number"].ToString(),
                      serialNumberIssueDocument: table.Rows[i]["serial_and_number_issue_document"].ToString(), group: table.Rows[i]["group_s"].ToString(),
                      gender: table.Rows[i]["gender"].ToString(), startEducation: table.Rows[i]["year_start"].ToString(),
                      levelEducation: table.Rows[i]["level_education"].ToString(), sourceFunding: table.Rows[i]["source_funding"].ToString(), dateContractCo: dateContractCoSQL, nameOrganisationEmployer: table.Rows[i]["name_organisation_employer"].ToString(),
                      ogrnEmployer: table.Rows[i]["ogrn_employer"].ToString(), kppEmployer: table.Rows[i]["kpp_employer"].ToString(), subjectCo: table.Rows[i]["subject_co"].ToString(),
                      serialAndNumberIssueApplication: table.Rows[i]["serial_and_number_issue_application"].ToString(), yearEnd: table.Rows[i]["year_end"].ToString(),
                      isDublicate: Convert.ToBoolean(table.Rows[i]["is_dublicate"]), moreInfo: table.Rows[i]["more_info"].ToString(), note: table.Rows[i]["note"].ToString(),
                      contractCo: table.Rows[i]["contract_co"].ToString(), yearEndSchool: table.Rows[i]["year_end_shcool"].ToString(),
                      countryPastDocument: table.Rows[i]["country_past_document"].ToString(), isContractCo: Convert.ToBoolean(table.Rows[i]["is_contract_co"].ToString())

                   );

                students.Add(studentEntities);
            }

        }
        private static void LoadCodeCountrys()
        {
            codeCountrys.Clear();
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable table = dataTableCreator.GiveMeDataTable("select * from public.code_country");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                CodeCountryEntities codeCountryEntities = new CodeCountryEntities(
                table.Rows[i]["id"].ToString(), table.Rows[i]["name"].ToString(), table.Rows[i]["code"].ToString());
                App.codeCountrys.Add(codeCountryEntities);
            }
        }
        private static void LoadItogDisciplines()
        {
            DataTableCreator dataTableCreator = new DataTableCreator();
            DataTable table = dataTableCreator.GiveMeDataTable("select * from public.student_discipline");
            itogDisciplines.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                bool isCourseSQL = false;
                if (table.Rows[i]["is_course_work"].ToString() != "")
                    isCourseSQL = Convert.ToBoolean(table.Rows[i]["is_course_work"].ToString());

                ItogDisciplineEntities itogDisciplineEntities = new ItogDisciplineEntities(
                id: table.Rows[i]["id"].ToString(), idStudent: table.Rows[i]["id_student"].ToString(), idDiscipline: table.Rows[i]["id_discipline"].ToString(),
                 score: table.Rows[i]["score"].ToString(),
                moreInfo: table.Rows[i]["more_info"].ToString(), typeOfActivity: table.Rows[i]["type_of_activity"].ToString(), useMethodsEducation: table.Rows[i]["use_methods_education"].ToString(),
                placePassage: table.Rows[i]["place_passage"].ToString(), 
                isCourse: isCourseSQL
                );
                App.itogDisciplines.Add(itogDisciplineEntities);
            }

        }
        public static void Refresh()
        {
            LoadItogDisciplines();
            LoadCodeCountrys();
            LoadStudents();
            LoadOrganisation();
            LoadGroups();
            LoadSecretaries();
            LoadSpecialisations();
            LoadDisciplines();

        }


    }
}
