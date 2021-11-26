using Prism.Events;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.Events {
  public class GroupCreatedEvent : PubSubEvent<Group> { }

  public class GroupRemovedEvent : PubSubEvent<Group> { }

  public class GroupChangedEvent : PubSubEvent<Group> { }
}