using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using PsmsConfigurator.Shared.Models;
using PsmsConfigurator.WpfApp.Events;
using PsmsConfigurator.WpfApp.Extensions;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class LoopsConfigurationViewModel : BindableBase {
    private readonly IContainerExtension _containerExtension;
    private readonly IEventAggregator _eventAggregator;
    private readonly Specification _specification;

    private Configuration _configuration;
    private LoopSettingsViewModel _selectedLoop;

    public LoopsConfigurationViewModel(IContainerExtension containerExtension,
      IEventAggregator eventAggregator, Specification specification) {
      _containerExtension = containerExtension;
      _eventAggregator = eventAggregator;
      _specification = specification;

      Loops = new ObservableCollection<LoopSettingsViewModel>();

      AddLoopCmd = new DelegateCommand(AddLoopExecute, CanAddLoopExecute);
      RemoveLoopCmd = new DelegateCommand<LoopSettingsViewModel>(RemoveLoopExecute, 
        CanRemoveLoopExecute);

      Subscribe();
    }

    public ObservableCollection<LoopSettingsViewModel> Loops { get; }

    public LoopSettingsViewModel SelectedLoop {
      get => _selectedLoop;
      set => SetProperty(ref _selectedLoop, value);
    }

    public DelegateCommand AddLoopCmd { get; }

    private void AddLoopExecute() {
      var loop = _configuration.Loops
        .FirstOrDefault(l => !l.Actual);
      if (loop == null)
        return;
      loop.Actual = true;
      AddLoopSettings(loop);
      _eventAggregator.GetEvent<LoopCreatedEvent>()
        .Publish(loop);
    }

    private bool CanAddLoopExecute() =>
      _configuration.Loops.Any(loop => !loop.Actual);

    public DelegateCommand<LoopSettingsViewModel> RemoveLoopCmd { get; }

    private void RemoveLoopExecute(LoopSettingsViewModel loopSettings) {
      loopSettings.Model.Actual = false;
      Loops.Remove(loopSettings);
      _eventAggregator.GetEvent<LoopRemovedEvent>()
        .Publish(loopSettings.Model);
    }

    private bool CanRemoveLoopExecute(LoopSettingsViewModel settings) =>
      _configuration.Loops.Count(loop => loop.Actual) > _specification.Loops.MinCount;

    public ushort DelayMaxValue =>
      _specification.Loops.DelayMaxValue;

    public ushort DelayMinValue =>
      _specification.Loops.DelayMinValue;
    
    private void AddLoopSettings(Loop loop) {
      var settings = _containerExtension.CreateLoopSettings(loop);
      Loops.Add(settings);
      SelectedLoop = settings;
    }

    private void Subscribe() {
      _eventAggregator.GetEvent<LoopCreatedEvent>()
        .Subscribe(OnLoopCreated);
      _eventAggregator.GetEvent<LoopRemovedEvent>()
        .Subscribe(OnLoopRemoved);
    }

    private void OnLoopCreated(Loop loop) {
      AddLoopCmd.RaiseCanExecuteChanged();
      RemoveLoopCmd.RaiseCanExecuteChanged();
    }

    private void OnLoopRemoved(Loop loop) {
      AddLoopCmd.RaiseCanExecuteChanged();
      RemoveLoopCmd.RaiseCanExecuteChanged();
    }

    public void Initialize(Configuration configuration) {
      _configuration = configuration;
      foreach (var loop in configuration.Loops.Where(l => l.Actual))
        AddLoopSettings(loop);
    }
  }
}