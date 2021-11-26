using Prism.Events;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.Events {
  public class SceneCreatedEvent : PubSubEvent<Scene> { }
  
  public class SceneChangedEvent : PubSubEvent<Scene> { }
  
  public class SceneRemovedEvent : PubSubEvent<Scene> { }
}