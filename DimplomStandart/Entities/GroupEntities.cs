using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class GroupEntities
    {
       
        public GroupEntities(string id = "", string name = "", string specialisation = "", string levelTraning = "", string normalSizeStudy = "", string nameFactory = "", 
            DateTime? dateResultGek = null, DateTime? dateIssueDocument = null, string presidentGek = "", string isVPresident = "", string secretary = "", string formStudy = "", string protocolGek = "", string orderDropStudent = "", 
            DateTime? orderDropStudentDate = null, string moreInfo = "", string note = "", bool isLoadToFrdo = false)
        {
            Id = id;
            Name = name;
            Specialisation = specialisation;
            LevelTraning = levelTraning;
            NameFactory = nameFactory;
            DateResultGek = dateResultGek;
            DateIssueDocument = dateIssueDocument;
            PresidentGek = presidentGek;
            Secretary = secretary;
            FormStudy = formStudy;
            ProtocolGek = protocolGek;
            OrderDropStudent = orderDropStudent;
            OrderDropStudentDate = orderDropStudentDate;
            MoreInfo = moreInfo;
            Note = note;
            IsLoadToFrdo = isLoadToFrdo;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Specialisation { get; set; }
        public string LevelTraning { get; set; }//Базовый уровень, профильный уровень
        public string NameFactory { get; set; }
        public DateTime? DateResultGek { get;set; }
        public DateTime? DateIssueDocument { get; set; }
        public string PresidentGek { get; set; }
        public string Secretary { get; set; }
        public string FormStudy { get; set; }
        public string ProtocolGek { get; set; }
        public string OrderDropStudent { get; set; }
        public DateTime? OrderDropStudentDate { get; set; }
        public string MoreInfo { get; set; }
        public string Note { get; set; }
        public bool IsLoadToFrdo { get; set; }
        /////////////////////////////////////
        public string SpecialisationStrLong { 
            get {
                if ((from q in App.specialisations where q.Id == Specialisation select q.NameLong).ToList().Any())
                    return (from q in App.specialisations where q.Id == Specialisation select q.NameLong).ToList().Single();
                else
                    return "";
            }
            set {
                 Specialisation = ( from q in App.specialisations where q.NameLong==value select q.Id).ToList().Single();
            } 
        }
        public string SpecialisationStrShort
        {
            get
            {
                return (from q in App.specialisations where q.Id == Specialisation select q.NameShort).ToList().Single();
            }
            set
            {
                Specialisation = (from q in App.specialisations where q.NameShort == value select q.Id).ToList().Single();
            }
        }
       
        public string Qualification
        {
            get
            {
                if ((from q in App.specialisations where q.Id == Specialisation select q.Qualification).ToList().Any())
                    return (from q in App.specialisations where q.Id == Specialisation select q.Qualification).ToList().Single();
                else
                    return "";
            }
            set
            {
                var qualification = (from q in App.specialisations where q.Id==Specialisation select q.Qualification).ToList().Single();
            }
        }
        public string NormalPeriodStudy
        {
            get
            {
                if ((from q in App.specialisations where q.Id == Specialisation select q.NormalPeriodStudy).ToList().Any())
                    return (from q in App.specialisations where q.Id == Specialisation select q.NormalPeriodStudy).ToList().Single();
                else
                    return "";
            }
            set
            {

            }
        }
        public string SecretaryStr
        {
            get
            {
                if ((from q in App.secretaries where q.Id == Secretary select q.Name).ToList().Any())
                    return (from q in App.secretaries where q.Id == Secretary select q.Name).ToList().Single();
                else
                    return "";
            }
            set
            {

            }
        }
        public string Direction { 
            get {
                if ((from q in App.specialisations where q.Id == Specialisation select q.Direction).ToList().Any())
                    return (from q in App.specialisations where q.Id == Specialisation select q.Direction).ToList().Single();
                else
                    return "";
            }
            set { } 
        }
        public string SpecialisationYear
        {
            get
            {
                if (Id != "")
                    return (from q in App.specialisations where q.Id == Specialisation select q.SpecialisationYear).ToList().Single();
                else
                    return "";

            }
            set { }
        }
    }
}
