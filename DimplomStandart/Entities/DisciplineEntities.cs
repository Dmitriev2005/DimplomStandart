using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class DisciplineEntities
    {
        public DisciplineEntities(string id = "", string name = "", string type = "",string idGroup = "",string countHour="") 
        { 
            Id = id; 
            Name = name; 
            Type = type;
            IdGroup = idGroup;
            CountHour = countHour;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string CountHour { get; set; }
        public string IdGroup { get; set; }
        ////////////
        public string IdGroupStr { get
            {
                if ((from q in App.groups where q.Id == IdGroup select q.Name).Any())
                    return (from q in App.groups where q.Id == IdGroup select q.Name).ToList().Single();
                else
                    return "";
            }
            set => IdGroup = (from q in App.groups where q.Name == value select q.Id).ToList().Single();
        }

    }
}
