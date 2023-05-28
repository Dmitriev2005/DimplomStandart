using DimplomStandart.Windows.DataReference.StudentWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class ItogDisciplineEntities
    {
        public ItogDisciplineEntities(string id = "", string idDiscipline = "", string countHour = "",
            string score = "", string idStudent = "", string moreInfo = "", string idGroup = "")
        {
            Id = id;
            IdDiscipline = idDiscipline;
            IdStudent = idStudent;
            CountHour = countHour;
            Score = score;
            MoreInfo = moreInfo;
            IdGroup = idGroup;
        }

        public string Id { get; set; }
        public string IdDiscipline { get; set; }
        public string IdStudent { get; set; }
        public string IdGroup { get; set; }
        public string Score { get; set; }
        public string MoreInfo { get; set; }
        ////////////////////  ///////////////
        public string IdDisciplineStr {
            get
            {
                if (Id != "")
                    return (from q in App.disciplines where q.Id == IdDiscipline select q.Name).ToList().Single();
                else
                    return "";

            }
            set
            {

            }
        }
        public string CountHour {
            get
            {
                if (Id != "")
                    return (from q in App.disciplines where q.Id == IdDiscipline select q.CountHour).ToList().Single();
                else
                    return "";

            }
            set { }
        }
        public string Type
        {
            get
            {
                if (Id != "")
                    return (from q in App.disciplines where q.Id == IdDiscipline select q.Type).ToList().Single();
                else
                    return "";
            }
            set { }

        }



    }
}
