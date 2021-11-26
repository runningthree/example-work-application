using System;
using Prism.Events;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.Events {
  public class GroupLoopStateChangedEventArgs : EventArgs {
    public GroupLoopStateChangedEventArgs(GroupLoopState groupLoopState, 
      Group owner) {
      State = groupLoopState;
      Owner = owner;
    }
    
    public GroupLoopState State { get; }
    public Group Owner { get; }
    public bool Value { get; }
  }
  public class GroupLoopStateChangedEvent : PubSubEvent<GroupLoopStateChangedEventArgs> { }
}