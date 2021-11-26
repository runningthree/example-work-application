using Prism.Events;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.Events {
  public class OutputCreatedEvent : PubSubEvent<Output> { }

  public class OutputRemovedEvent : PubSubEvent<Output> { }

  public class OutputChangedEvent : PubSubEvent<Output> { }
}