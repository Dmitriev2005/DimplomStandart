using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class SecretaryEntities
    {
        public SecretaryEntities(string id = "", string name = "", string specialisation = "")
        {
            Id = id;
            Name = name;
            Specialisation = specialisation;

        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Specialisation { get; set; }
        public string SpecialisationStr { 
            get
            {
                return (from q in App.specialisations where 
                        q.Id == Specialisation select q.NameLong).ToList()[0].ToString();
               
            }
            set 
            {
                Specialisation = (from q in App.specialisations where q.NameLong == value select q.Id).ToList().Single();
            } 
        }

        private string specialisationStr;

    }
}
