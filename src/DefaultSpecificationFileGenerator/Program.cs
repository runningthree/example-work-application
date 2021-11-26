using System.IO;
using Newtonsoft.Json;
using PsmsConfigurator.Shared.Models;

namespace DefaultSpecificationFileGenerator {
  internal class Program {
    private const string FileName = "specification.json";
    
    private static void Main(string[] args) => 
      File.WriteAllText(FileName, JsonConvert.SerializeObject(Specification.Default, Formatting.Indented));
  }
}
