using System.IO;
using System.Threading.Tasks;

namespace BuildCalypse.CodeGen
{
    public class CodeExporter
    {
        public async Task Export(Structure[] structures, CodeGenResult result, string outputDirectory)
        {
            await ExportPlacerClusters(structures, result, outputDirectory);

            
        }

        private static async Task ExportPlacerClusters(Structure[] structures, CodeGenResult result, string outputDirectory)
        {
            var placerPath = Path.Combine(outputDirectory, "function/placers");
            Directory.CreateDirectory(placerPath);
            foreach (var item in structures)
            {
                var filePath = Path.Combine(placerPath, item.Id + ".function");
                using var file = new FileStream(filePath, FileMode.CreateNew);
                using var writer = new StreamWriter(file);

                foreach (var circuitItem in result.PlacerCircuitBuilders[item.Id])
                {
                    await writer.WriteLineAsync(circuitItem);
                }
                foreach (var cmdBlock in result.Placers[item.Id])
                {
                    await writer.WriteLineAsync(cmdBlock.GetCommandSetBlock());
                }
            }
        }
    }
}