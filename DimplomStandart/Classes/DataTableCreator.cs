using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Classes
{
    public class DataTableCreator
    {
        public DataTable GiveMeDataTable(string query)
        {
            NpgsqlCommand command = new NpgsqlCommand(query,App.Connection());
            NpgsqlDataReader reader = command.ExecuteReader();
            DataTable newDataTable = new DataTable();

            newDataTable.Load(reader);

            return newDataTable;
        }
    }
}
