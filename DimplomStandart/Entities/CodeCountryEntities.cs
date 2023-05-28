using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class CodeCountryEntities
    {
        public CodeCountryEntities(string id="", string name="", string code="")
        {
            Id = id;
            Name = name;
            Code = code;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get;set; }
    }
}
