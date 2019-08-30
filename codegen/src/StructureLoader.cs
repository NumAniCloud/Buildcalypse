using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace BuildCalypse.CodeGen
{
    public class StructureLoader
    {
        public Structure[] Load(string path)
        {
            using var file = new FileStream(path, FileMode.Open);
            using var reader = new StreamReader(file);
            using var csv = new CsvReader(reader);
            csv.Configuration.HasHeaderRecord = true;
            return csv.GetRecords<Structure>().ToArray();
        }
    }
}