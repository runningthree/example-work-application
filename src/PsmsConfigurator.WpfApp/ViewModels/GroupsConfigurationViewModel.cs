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
  public class GroupsConfigurationViewModel : BindableBase {
    private readonly IContainerExtension _containerExtension;
    private readonly IDialogService _dialogService;
    private readonly IEventAggregator _eventAggregator;
    private readonly Specification _specification;

    private Configuration _configuration;
    private GroupSettingsViewModel _selectedGroup;

    public GroupsConfigurationViewModel(IContainerExtension containerExtension,
      IDialogService dialogService, IEventAggregator eventAggregator,
      Specification specification) {
      _containerExtension = containerExtension;
      _dialogService = dialogService;
      _eventAggregator = eventAggregator;
      _specification = specification;

      Groups = new ObservableCollection<GroupSettingsViewModel>();

      AddGroupCmd = new DelegateCommand(AddGroupExecute, CanAddGroupExecute);
      RemoveGroupCmd = new DelegateCommand<GroupSettingsViewModel>(RemoveGroupExecute, CanRemoveGroupExecute);

      Subscribe();
    }

    public ObservableCollection<GroupSettingsViewModel> Groups { get; }

    public GroupSettingsViewModel SelectedGroup {
      get => _selectedGroup;
      set => SetProperty(ref _selectedGroup, value);
    }

    public DelegateCommand AddGroupCmd { get; }

    private void AddGroupExecute() {
      var group = _configuration.Groups
        .FirstOrDefault(g => !g.Actual);
      if (group == null)
        return;
      group.Actual = true;
      AddGroupSettings(group);
    }

    private bool CanAddGroupExecute() =>
      _configuration.Groups.Any(group => !group.Actual);

    private void AddGroupSettings(Group group) {
      var settings = _containerExtension
        .CreateGroupSettings(group, _configuration);
      Groups.Add(settings);
      SelectedGroup = settings;
      _eventAggregator.GetEvent<GroupCreatedEvent>()
        .Publish(group);
    }

    public DelegateCommand<GroupSettingsViewModel> RemoveGroupCmd { get; }

    private void RemoveGroupExecute(GroupSettingsViewModel groupSettingsViewModel) {
      var messageBuilder = new StringBuilder();
      var eventType = _specification.Scenes.GroupEventTypeStartIndex + groupSettingsViewModel.Model.Index;
      if (_configuration.Scenes.Any(settings => settings.EventType == eventType))
        messageBuilder.AppendLine("Группа используется. При удалении выхода все сцены, " +
                                  "которые его используют, будут удалены.");
      messageBuilder.AppendLine($"Вы уверены, что хотите удалить {groupSettingsViewModel.Name}?");
      _dialogService.ShowRemoveMessageBox("Удаление группы", messageBuilder.ToString(), result => {
        if (result.Result != ButtonResult.OK)
          return;
        groupSettingsViewModel.Model.Actual = false;
        Groups.Remove(groupSettingsViewModel);
        _eventAggregator.GetEvent<GroupRemovedEvent>()
          .Publish(groupSettingsViewModel.Model);
      });
    }

    private bool CanRemoveGroupExecute(GroupSettingsViewModel settings) =>
      _configuration.Groups.Count(group => group.Actual) > _specification.Groups.MinCount;

    private void Subscribe() {
      _eventAggregator.GetEvent<GroupCreatedEvent>()
        .Subscribe(OnGroupCreated);
      _eventAggregator.GetEvent<GroupRemovedEvent>()
        .Subscribe(OnGroupRemoved);
    }

    private void OnGroupCreated(Group group) {
      AddGroupCmd.RaiseCanExecuteChanged();
      RemoveGroupCmd.RaiseCanExecuteChanged();
    }

    private void OnGroupRemoved(Group group) {
      AddGroupCmd.RaiseCanExecuteChanged();
      RemoveGroupCmd.RaiseCanExecuteChanged();
    }

    public void Initialize(Configuration configuration) {
      _configuration = configuration;
      foreach (var groupSettings in configuration.Groups.Where(g => g.Actual))
        AddGroupSettings(groupSettings);
    }
  }
}