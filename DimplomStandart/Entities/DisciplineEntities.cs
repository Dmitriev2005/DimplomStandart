using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class DisciplineEntities
    {
        public DisciplineEntities(string id = "", string name = "", string type = "",string idSpecialisation = "",string countHour="") 
        { 
            Id = id; 
            Name = name; 
            Type = type;
            IdSpecialisation = idSpecialisation;
            CountHour = countHour;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string CountHour { get; set; }
        public string IdSpecialisation { get; set; }
        ////////////
        public string SpecialisationStr { get
            {
                if ((from q in App.specialisations where q.Id == IdSpecialisation select q.NameShort).Any())
                    return (from q in App.specialisations where q.Id == IdSpecialisation select q.NameShort).ToList().Single();
                else
                    return "";
            }
            set => IdSpecialisation = (from q in App.specialisations where q.NameShort == value select q.Id).ToList().Single();
        }
       

    }
}
