using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class StudentEntities
    {
        public StudentEntities(string id = "", string surnameIm = "", string nameIm = "", string lastNameIm = "", string surnameDa = "",
            string nameDa = "", string lastNameDa = "", string snils = "", DateTime? dateBorn = null, string placeBorn = "", string codeCountry = "",
            string viewDocument = "", string seriesPastDocumentEducation = "", string numberPastDocumentEducation = "",
            string surnamePastDocument = "", string regNumberIssueDocument = "", string serialNumberIssueDocument = "",
            string group = "", string gender = "", string startEducation = "", string levelEducation = "",
            string sourceFunding = "", DateTime? dateContractCo = null, string nameOrganisationEmployer = "", string ogrnEmployer = "", string kppEmployer = "",
            string subjectCo = "", string serialAndNumberIssueApplication = "", string yearEnd = "", bool isDublicate = false,
            string moreInfo = "", string note = "", string contractCo = "", string yearEndSchool = "",
            string countryPastDocument = "", bool isContractCo = false, bool isCheckGreat = false)
        {
            Id = id;
            SurnameIm = surnameIm;
            NameIm = nameIm;
            LastNameIm = lastNameIm;
            SurnameDa = surnameDa;
            NameDa = nameDa;
            LastNameDa = lastNameDa;
            Snils = snils;
            DateBorn = dateBorn;
            PlaceBorn = placeBorn;
            CodeCountry = codeCountry;
            ViewDocument = viewDocument;
            SeriesPastDocumentEducation = seriesPastDocumentEducation;
            NumberPastDocumentEducation = numberPastDocumentEducation;
            SurnamePastDocument = surnamePastDocument;
            RegNumberIssueDocument = regNumberIssueDocument;
            SerialNumberIssueDocument = serialNumberIssueDocument;
            Group = group;
            Gender = gender;
            YearStart = startEducation;
            LevelEducation = levelEducation;
            SourceFunding = sourceFunding;
            DateContractCo = dateContractCo;
            NameOrganisationEmployer = nameOrganisationEmployer;
            OgrnEmployer = ogrnEmployer;
            KppEmployer = kppEmployer;
            SubjectCo = subjectCo;
            SerialNumberIssueApplication = serialAndNumberIssueApplication;
            YearEnd = yearEnd;
            IsDublicate = isDublicate;
            MoreInfo = moreInfo;
            Note = note;
            ContractCo = contractCo;
            YearEndSchool = yearEndSchool;
            CountryPastDocument = countryPastDocument;
            IsContractCo = isContractCo;
            IsCheckGreat = isCheckGreat;
        }

        public string Id { get; set; }
        public string SurnameIm { get; set; }
        public string NameIm { get;set; }
        public string LastNameIm { get; set; }
        public string SurnameDa { get; set; }
        public string NameDa { get; set; }
        public string LastNameDa { get; set; }
        public string Snils { get; set; }
        public DateTime? DateBorn { get; set; }
        public string PlaceBorn { get; set; }
        public string CodeCountry { get; set; }
        public string ViewDocument { get; set; }
        public string SeriesPastDocumentEducation { get; set; }
        public string NumberPastDocumentEducation { get; set; } 
        public string SurnamePastDocument { get; set; }
        public string RegNumberIssueDocument { get; set; }
        public string SerialNumberIssueDocument { get; set; }
        public string SerialNumberIssueApplication { get; set; }
        public string Group { get; set; }
        public string Gender { get; set; }
        public string YearStart { get; set; }
        public string YearEnd { get; set; }
        public string LevelEducation { get; set; }
        public string SourceFunding { get; set; }
        public DateTime? DateContractCo { get; set; }
        public string NameOrganisationEmployer { get; set; }
        public string OgrnEmployer { get; set; }
        public string KppEmployer { get; set; }
        public string SubjectCo { get; set; }
        public bool IsDublicate { get; set; }
        public string MoreInfo { get; set; }
        public string Note { get; set; }
        public string ContractCo { get; set; }
        public string YearEndSchool { get; set; }
        public string CountryPastDocument { get; set; }
        public bool IsContractCo { get; set; }
        public bool IsCheckGreat { get; set; }
        //////////////////////////////////////
        //////////////////////////////////////

       
       
        public string GroupStr
        {
            get {
                if ((from q in App.groups where q.Id == Group select q.Name).ToList().Any())
                    return (from q in App.groups where q.Id == Group select q.Name).ToList().Single();
                else
                    return "";
            }
            set { }
        }
       
        public string CodeCountryStr
        {
            get
            {
                if ((from q in App.codeCountrys where q.Id == CodeCountry select q.Code).ToList().Any())
                    return (from q in App.codeCountrys where q.Id == CodeCountry select q.Code).ToList().Single();
                else
                    return "";
            }
            set { }

        }
        public string SpecialisationLong
        {
            get {
                if (Id != "")
                    return (from q in App.groups where q.Id == Group select q.SpecialisationStrLong).ToList().Single();
                else
                    return "";
            }
            set { }
        }
        public string SpecialisationShort
        {
            get
            {
                if (Id != "")
                    return (from q in App.groups where q.Id == Group select q.SpecialisationStrShort).ToList().Single();
                else
                    return "";
            }
            set { }
        }
        public string DateIssue
        {
            get
            {
                if (Id != "")
                    return (from q in App.groups where q.Id == Group select Convert.ToDateTime(q.DateIssueDocument).ToString("dd.MM.yyyy")).ToList().Single();
                else
                    return "";
            }
            set { }
        }
        public string DateDropStudent
        {
            get
            {
                if (Id != "")
                    return (from q in App.groups where q.Id == Group select Convert.ToDateTime(q.OrderDropStudentDate).ToString("dd.MM.yyyy")).ToList().Single();
                else
                    return "";
            }
            set { }
        }

    }
}