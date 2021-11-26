using Prism.Events;
using Prism.Mvvm;
using PsmsConfigurator.WpfApp.Events;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class GroupLoopStateViewModel : BindableBase {
    private readonly IEventAggregator _eventAggregator;

    private Group _owner;
    private string _name;
    
    public GroupLoopStateViewModel(IEventAggregator eventAggregator) {
      _eventAggregator = eventAggregator;
    }
    
    public GroupLoopState Model { get; private set; }

    public string Name {
      get => _name;
      set => SetProperty(ref _name, value);
    }

    public bool Included {
      get => Model.Included;
      set {
        Model.Included = value;
        _eventAggregator.GetEvent<GroupLoopStateChangedEvent>()
          .Publish(new GroupLoopStateChangedEventArgs(Model, _owner));
      }
    }
    
    public void Initialize(GroupLoopState groupLoopState, Group owner) {
      Model = groupLoopState;
      _owner = owner;
    }
  }
}