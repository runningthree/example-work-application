using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PsmsConfigurator.Shared.Models;
using PsmsConfigurator.WpfApp.Events;
using PsmsConfigurator.WpfApp.Extensions;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class ScenariosConfigurationViewModel : BindableBase {
    private readonly IDialogService _dialogService;
    private readonly IContainerExtension _containerExtension;
    private readonly IEventAggregator _eventAggregator;
    private readonly Specification _specification;

    private Configuration _configuration;
    private ScenarioSettingsViewModel _selectedScenario;
    private IDictionary<ulong, string> _eventTypes;
    private IDictionary<ulong, string> _outputs;

    public ScenariosConfigurationViewModel(IDialogService dialogService,
      IContainerExtension containerExtension, IEventAggregator eventAggregator,
      Specification specification) {
      _dialogService = dialogService;
      _containerExtension = containerExtension;
      _eventAggregator = eventAggregator;
      _specification = specification;

      Scenarios = new ObservableCollection<ScenarioSettingsViewModel>();

      AddScenarioCmd = new DelegateCommand(AddScenarioExecute, CanAddScenarioExecute);
      AddSceneCmd = new DelegateCommand<ScenarioSettingsViewModel>(AddSceneExecute, CanAddSceneExecute);
      RemoveSceneCmd = new DelegateCommand<Scene>(RemoveSceneExecute);

      Subscribe();
    }

    public ObservableCollection<ScenarioSettingsViewModel> Scenarios { get; }

    public ScenarioSettingsViewModel SelectedScenario {
      get => _selectedScenario;
      set => SetProperty(ref _selectedScenario, value);
    }

    public IDictionary<ulong, string> EventTypes {
      get => _eventTypes;
      set => SetProperty(ref _eventTypes, value);
    }

    private IDictionary<ulong, string> UnusedEventTypes {
      get {
        var unusedEventTypes = new Dictionary<ulong, string>();
        foreach (var eventType in EventTypes)
          if (_configuration.Scenes.All(settings => settings.EventType != eventType.Key))
            unusedEventTypes[eventType.Key] = eventType.Value;
        return unusedEventTypes;
      }
    }

    public IDictionary<ulong, string> Outputs {
      get => _outputs;
      set => SetProperty(ref _outputs, value);
    }

    public IDictionary<ulong, string> Modes =>
      ScenesSpec.ModeTypes;

    public ulong DurationMaxValue =>
      ScenesSpec.DurationMaxValue;

    public ulong DurationMinValue =>
      ScenesSpec.DurationMinValue;

    public ulong DelayMaxValue =>
      ScenesSpec.DelayMaxValue;

    public ulong DelayMinValue =>
      ScenesSpec.DelayMinValue;

    public DelegateCommand AddScenarioCmd { get; }

    private void AddScenarioExecute() {
      _dialogService.ShowDialog("CreateScenarioWindow", new DialogParameters {
        {"event-types", UnusedEventTypes}
      }, result => {
        if (result.Result != ButtonResult.OK)
          return;
        var selectedType = result.Parameters
          .GetValue<uint>("selected-type");
        var scenarioSettings = AddScenario(selectedType);
        AddSceneExecute(scenarioSettings);
      });
    }

    private ScenarioSettingsViewModel AddScenario(ulong eventType) {
      var scenarioSettings = _containerExtension
        .CreateScenarioSettings(eventType);
      scenarioSettings.Header = EventTypes[eventType];
      Scenarios.Add(scenarioSettings);
      SelectedScenario = scenarioSettings;
      return scenarioSettings;
    }

    private bool CanAddScenarioExecute() =>
      _configuration.Scenes.Any(scene => scene.EventType == ScenesSpec.EventTypeDefaultValue) &&
      UnusedEventTypes.Count > 0 && Outputs.Count > 0;

    public DelegateCommand<ScenarioSettingsViewModel> AddSceneCmd { get; }

    private void AddSceneExecute(ScenarioSettingsViewModel scenarioSettings) {
      var scene = _configuration.Scenes
        .FirstOrDefault(s => s.EventType == ScenesSpec.EventTypeDefaultValue);
      if (scene == null)
        return;
      var output = _configuration.Outputs
        .FirstOrDefault(o => o.Actual);
      if (output == null)
        return;
      scene.EventType = scenarioSettings.EventType;
      scene.Output = output.Index;
      scenarioSettings.AddScene(scene);
      _eventAggregator.GetEvent<SceneCreatedEvent>()
        .Publish(scene);
    }

    private bool CanAddSceneExecute(ScenarioSettingsViewModel scenarioVm) =>
      _configuration.Scenes.Any(s => s.EventType == ScenesSpec.EventTypeDefaultValue);

    public DelegateCommand<Scene> RemoveSceneCmd { get; }

    private void RemoveSceneExecute(Scene scene) =>
      RemoveScene(scene);

    private void RemoveScene(Scene scene) {
      var scenario = Scenarios.FirstOrDefault(s => s.EventType == scene.EventType);
      scene.EventType = ScenesSpec.EventTypeDefaultValue;
      scenario?.Scenes.Remove(scene);
      _eventAggregator.GetEvent<SceneRemovedEvent>()
        .Publish(scene);
    }

    private ScenesConfigurationSpecification ScenesSpec =>
      _specification.Scenes;

    private void UpdateEventTypes() {
      var eventTypes = new Dictionary<ulong, string>();
      foreach (var defaultEventType in _specification.Scenes.StandardEventTypes)
        eventTypes[ScenesSpec.DefaultEventTypesIndex + defaultEventType.Key] = defaultEventType.Value;
      foreach (var manualStartEventType in ScenesSpec.ManualStartEventTypes)
        eventTypes[ScenesSpec.ManualStartEventTypesIndex + manualStartEventType.Key] =
          manualStartEventType.Value;
      var includedGroups = _configuration.Groups
        .Where(group => group.Actual && group.LoopStates.Any(state => state.Included));
      foreach (var group in includedGroups)
        eventTypes[ScenesSpec.GroupEventTypeStartIndex + @group.Index] = group.Name;
      var actualLoops = _configuration.Loops
        .Where(loop => loop.Actual);
      foreach (var loop in actualLoops)
        eventTypes[ScenesSpec.LoopEventTypesStartIndex + loop.Index] = loop.Name;
      EventTypes = eventTypes;
    }

    private void UpdateOutputs() =>
      Outputs = _configuration.Outputs
        .Where(output => output.Actual)
        .ToDictionary(output => (ulong) output.Index, output => output.Name);

    private void Subscribe() {
      _eventAggregator.GetEvent<SceneCreatedEvent>()
        .Subscribe(OnSceneCreated);
      _eventAggregator.GetEvent<SceneRemovedEvent>()
        .Subscribe(OnSceneRemoved);
      _eventAggregator.GetEvent<OutputCreatedEvent>()
        .Subscribe(OnOutputCreated);
      _eventAggregator.GetEvent<OutputRemovedEvent>()
        .Subscribe(OnOutputRemoved);
      _eventAggregator.GetEvent<GroupCreatedEvent>()
        .Subscribe(OnGroupCreated);
      _eventAggregator.GetEvent<GroupRemovedEvent>()
        .Subscribe(OnGroupRemoved);
      _eventAggregator.GetEvent<GroupLoopStateChangedEvent>()
        .Subscribe(OnGroupLoopStateChanged);
    }

    private void OnSceneCreated(Scene scene) {
      UpdateEventTypes();
      AddScenarioCmd.RaiseCanExecuteChanged();
      AddSceneCmd.RaiseCanExecuteChanged();
    }

    private void OnSceneRemoved(Scene scene) {
      var emptyScenarios = Scenarios
        .Where(s => s.Scenes.Count <= 0)
        .ToArray();
      foreach (var emptyScenario in emptyScenarios) {
        if (SelectedScenario == emptyScenario)
          SelectedScenario = null;
        Scenarios.Remove(emptyScenario);
      }

      UpdateEventTypes();
      AddScenarioCmd.RaiseCanExecuteChanged();
      AddSceneCmd.RaiseCanExecuteChanged();
    }

    private void OnOutputCreated(Output output) =>
      UpdateOutputs();

    private void OnOutputRemoved(Output output) {
      var scenes = _configuration.Scenes
        .Where(settings => settings.Output == output.Index)
        .ToArray();
      foreach (var scene in scenes) {
        RemoveScene(scene);
      }

      UpdateOutputs();
    }

    private void OnGroupCreated(Group group) =>
      UpdateEventTypes();

    private void OnGroupRemoved(Group group) {
      var scenes = _configuration.Scenes
        .Where(settings => settings.EventType == ScenesSpec.GroupEventTypeStartIndex + group.Index)
        .ToArray();
      foreach (var scene in scenes)
        RemoveScene(scene);
      UpdateEventTypes();
    }

    private void OnGroupLoopStateChanged(GroupLoopStateChangedEventArgs args) {
      if (!args.State.Included) {
        var isActiveGroup = args.Owner.LoopStates
          .Any(state => state.Included);
        if (!isActiveGroup) {
          var eventType = args.Owner.Index + ScenesSpec.GroupEventTypeStartIndex;
          var scenes = _configuration.Scenes
            .Where(settings => settings.EventType == eventType)
            .ToArray();
          foreach (var scene in scenes)
            RemoveScene(scene);
        }
      }

      UpdateEventTypes();
    }

    public void Initialize(Configuration configuration) {
      _configuration = configuration;
      UpdateEventTypes();
      UpdateOutputs();
      var types = configuration.Scenes
        .Where(scene => scene.EventType != ScenesSpec.EventTypeDefaultValue)
        .Select(settings => settings.EventType)
        .Distinct()
        .ToArray();
      foreach (var type in types) {
        var scenes = configuration.Scenes
          .Where(settings => settings.EventType == type)
          .ToArray();
        if (scenes.Length <= 0)
          continue;
        var scenarioSettings = AddScenario(type);
        foreach (var scene in scenes)
          scenarioSettings.AddScene(scene);
      }
    }
  }
}