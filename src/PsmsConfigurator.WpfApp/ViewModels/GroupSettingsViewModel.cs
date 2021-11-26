using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using Prism.Ioc;
using PsmsConfigurator.WpfApp.Events;
using PsmsConfigurator.WpfApp.Extensions;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class GroupSettingsViewModel {
    private readonly IContainerExtension _containerExtension;
    private readonly IEventAggregator _eventAggregator;

    private Configuration _configuration;

    public GroupSettingsViewModel(IContainerExtension containerExtension,
      IEventAggregator eventAggregator) {
      _containerExtension = containerExtension;
      _eventAggregator = eventAggregator;

      LoopsStates = new ObservableCollection<GroupLoopStateViewModel>();
      
      Subscribe();
    }

    public Group Model { get; private set; }

    public string Name =>
      Model.Name;

    public ObservableCollection<GroupLoopStateViewModel> LoopsStates { get; }

    private void AddLoopStateSettings(GroupLoopState state) {
      var loopStateSettings = _containerExtension
        .CreateGroupLoopStateSettings(state, Model);
      var loop = _configuration.Loops
        .FirstOrDefault(l => l.Index == state.LoopIndex);
      if (loop != null) 
        loopStateSettings.Name = loop.Name;
      LoopsStates.Add(loopStateSettings);
    }

    public void Initialize(Group group, Configuration configuration) {
      Model = group;
      _configuration = configuration;
      var actualLoopsIndexes = _configuration.Loops
        .Where(loop => loop.Actual)
        .Select(loop => loop.Index);
      var actualStates = group.LoopStates
        .Where(state => actualLoopsIndexes.Contains(state.LoopIndex));
      foreach (var loopState in actualStates) 
        AddLoopStateSettings(loopState);
    }

    private void Subscribe() {
      _eventAggregator.GetEvent<LoopCreatedEvent>()
        .Subscribe(OnLoopCreated);
      _eventAggregator.GetEvent<LoopRemovedEvent>()
        .Subscribe(OnLoopRemoved);
    }

    private void OnLoopCreated(Loop loop) {
      var loopState = Model.LoopStates
        .FirstOrDefault(gls => gls.LoopIndex == loop.Index);
      if (loopState == null)
        return;
      AddLoopStateSettings(loopState);
    }

    private void OnLoopRemoved(Loop loop) {
      var loopStateSettings = LoopsStates.FirstOrDefault(l => 
        l.Model.LoopIndex == loop.Index);
      if (loopStateSettings == null)
        return;
      LoopsStates.Remove(loopStateSettings);
    }
  }
}