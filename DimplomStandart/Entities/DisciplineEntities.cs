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
                if (Id != "")
                {
                    SpecialisationEntities specialisation = (from q in App.specialisations where q.Id == IdSpecialisation select q).ToList().Single();
                    return specialisation.NameShort + " " + specialisation.YearSpecialisation;

                }
                else
                    return "";
            }
            set
            {
                foreach (var item in App.specialisations)
                    if (item.NameShort == value.Remove(item.NameShort.Length)&&item.YearSpecialisation == value.Remove(0,item.NameShort.Length+1))
                        IdSpecialisation = item.Id;

            }

        }
       

    }
}
