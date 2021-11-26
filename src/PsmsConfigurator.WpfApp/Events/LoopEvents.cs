using Prism.Events;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.Events {
  public class LoopCreatedEvent : PubSubEvent<Loop> { }

  public class LoopRemovedEvent : PubSubEvent<Loop> { }

  public class LoopChangedEvent : PubSubEvent<Loop> { }
}