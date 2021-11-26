using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PsmsConfigurator.WpfApp {
  public class ConfigurationSettingsManager {
    public ConfigurationSettingsManager() {
      var outputTypesSection = (Hashtable) ConfigurationManager.GetSection("OutputTypes");
      OutputTypes = outputTypesSection.Cast<DictionaryEntry>()
        .ToDictionary(entry => (byte) entry.Key, entry => (string) entry.Value);
      OutputUnusedType = Convert.ToByte(ConfigurationManager.AppSettings["output-unused-type"]);
    }
    
    public IDictionary<byte, string> OutputTypes { get; }

    public byte OutputUnusedType { get; }
  }
}