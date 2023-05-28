using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class OrganisationEntities
    {
        public OrganisationEntities(string id ="", string nameI ="", string nameP="", string nameLocality="", 
            string pastName="", string region="", 
            string director="", string ogrn="", string kpp="", string rightAct="")
        {
            Id = id;
            NameI = nameI;
            NameP = nameP;
            NameLocality = nameLocality;
            PastName = pastName;
            Region = region;
            Director = director;
            Ogrn = ogrn;
            Kpp = kpp;
            RightAct = rightAct;
        }

        public string Id { get; set; }
        public string NameI { get; set; }
        public string NameP { get; set; }  
        public string NameLocality { get; set; }
        public string PastName { get; set; }
        public string Region { get; set; }
        public string Director { get; set; }
        public string Ogrn { get; set; }
        public string Kpp { get; set; }
        public string RightAct { get; set; }
        
        
    }
}
