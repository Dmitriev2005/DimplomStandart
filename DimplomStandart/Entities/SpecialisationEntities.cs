using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DimplomStandart.Entities
{
    public class SpecialisationEntities
    {
        public SpecialisationEntities(string id="", string nameShort = "", string nameLong = "", 
            string normalPeriodStudy = "", string qualification = "", bool isProfession = false, string direction="")
        {
            Id = id;
            NameShort = nameShort;
            NameLong = nameLong;
            NormalPeriodStudy = normalPeriodStudy;
            Qualification = qualification;
            IsProfession = isProfession;
            Direction = direction;
            StrIsProfession = isProfession ? "Да" : "Нет";

        }
        public string Id { get; set; } 
        public string NameShort { get; set; }
        public string NameLong { get; set; }
        public string NormalPeriodStudy { get; set; }
        public string Qualification { get;  set; }
        public bool IsProfession { get; set; }
        public string Direction { get; set; }

        public string StrIsProfession { 
            get { return IsProfession ? "Да" : "Нет"; }  
            set { strIsProfession = value;} 
        }
        private string strIsProfession;
    }
}
