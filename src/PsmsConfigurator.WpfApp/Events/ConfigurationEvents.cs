using System;
using Prism.Events;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.Events {
  public class ConfigurationEventArgs : EventArgs {
    public ConfigurationEventArgs(Configuration configuration, string fileName) {
      Configuration = configuration;
      FileName = fileName;
    }

    public Configuration Configuration { get; }
    public string FileName { get; }
  }

  public class ConfigurationCreatedEvent : PubSubEvent<ConfigurationEventArgs> { }
  public class ConfigurationOpenedEvent : PubSubEvent<ConfigurationEventArgs> { }
  public class ConfigurationSavedEvent : PubSubEvent<ConfigurationEventArgs> { }
}