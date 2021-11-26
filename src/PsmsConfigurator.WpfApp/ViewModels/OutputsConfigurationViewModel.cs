using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
  public class OutputsConfigurationViewModel : BindableBase {
    private readonly IContainerExtension _containerExtension;
    private readonly IEventAggregator _eventAggregator;
    private readonly IDialogService _dialogService;
    private readonly Specification _specification;

    private Configuration _configuration;
    private OutputSettingsViewModel _selectedOutput;

    public OutputsConfigurationViewModel(IContainerExtension containerExtension,
      IEventAggregator eventAggregator, IDialogService dialogService, Specification specification) {
      _containerExtension = containerExtension;
      _eventAggregator = eventAggregator;
      _dialogService = dialogService;
      _specification = specification;

      Outputs = new ObservableCollection<OutputSettingsViewModel>();

      AddOutputCmd = new DelegateCommand(AddOutputExecute, CanAddOutputExecute);
      RemoveOutputCmd = new DelegateCommand<OutputSettingsViewModel>(RemoveOutputExecute, CanRemoveOutputExecute);

      Subscribe();
    }

    public ObservableCollection<OutputSettingsViewModel> Outputs { get; }

    public OutputSettingsViewModel SelectedOutput {
      get => _selectedOutput;
      set => SetProperty(ref _selectedOutput, value);
    }

    public DelegateCommand AddOutputCmd { get; }

    private void AddOutputExecute() {
      var output = _configuration.Outputs
        .FirstOrDefault(o => !o.Actual);
      if (output == null)
        return;
      output.Actual = true;
      AddOutputSettings(output);
      _eventAggregator.GetEvent<OutputCreatedEvent>()
        .Publish(output);
    }

    private bool CanAddOutputExecute() =>
      _configuration.Outputs.Any(output => !output.Actual);

    private void AddOutputSettings(Output output) {
      var settings = _containerExtension.CreateOutputSettings(output);
      Outputs.Add(settings);
      SelectedOutput = settings;
    }

    public DelegateCommand<OutputSettingsViewModel> RemoveOutputCmd { get; }

    private void RemoveOutputExecute(OutputSettingsViewModel settings) {
      var model = settings.Model;
      var messageBuilder = new StringBuilder();
      if (_configuration.Scenes.Any(scene => scene.EventType != _specification.Scenes.EventTypeDefaultValue &&
                                             scene.Output == model.Index))
        messageBuilder.AppendLine("Выход используется. При удалении выхода все сцены, " +
                                  "которые его используют, будут удалены.");
      messageBuilder.AppendLine($"Вы уверены, что хотите удалить {model.Name}?");
      _dialogService.ShowRemoveMessageBox("Удаление выхода", messageBuilder.ToString(), result => {
        if (result.Result != ButtonResult.OK)
          return;
        model.Actual = false;
        Outputs.Remove(settings);
        _eventAggregator.GetEvent<OutputRemovedEvent>()
          .Publish(model);
      });
    }

    private bool CanRemoveOutputExecute(OutputSettingsViewModel settings) =>
      _configuration.Outputs.Count(output => output.Actual) > _specification.Outputs.MinCount;

    private void Subscribe() {
      _eventAggregator.GetEvent<OutputCreatedEvent>()
        .Subscribe(OnOutputCreated);
      _eventAggregator.GetEvent<OutputRemovedEvent>()
        .Subscribe(OnOutputRemoved);
    }

    private void OnOutputCreated(Output output) {
      AddOutputCmd.RaiseCanExecuteChanged();
      RemoveOutputCmd.RaiseCanExecuteChanged();
    }

    private void OnOutputRemoved(Output output) {
      AddOutputCmd.RaiseCanExecuteChanged();
      RemoveOutputCmd.RaiseCanExecuteChanged();
    }

    public void Initialize(Configuration configuration) {
      _configuration = configuration;
      foreach (var output in configuration.Outputs.Where(o => o.Actual))
        AddOutputSettings(output);
    }
  }
}