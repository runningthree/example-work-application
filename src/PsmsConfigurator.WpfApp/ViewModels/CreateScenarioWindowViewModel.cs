using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class CreateScenarioWindowViewModel : BindableBase, IDialogAware {
    private IDictionary<ulong, string> _eventTypes;
    private ulong _selectedType;

    public CreateScenarioWindowViewModel() {
      ApplyCmd = new DelegateCommand(ApplyExecute);
      CancelCmd = new DelegateCommand(CancelExecute);
    }

    public string Title =>
      "Create scenario";

    public IDictionary<ulong, string> EventTypes {
      get => _eventTypes;
      set => SetProperty(ref _eventTypes, value);
    }

    public ulong SelectedType {
      get => _selectedType;
      set => SetProperty(ref _selectedType, value);
    }

    public DelegateCommand ApplyCmd { get; }

    private void ApplyExecute() {
      RequestClose?.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters {
        {"selected-type", SelectedType}
      }));
    }

    public DelegateCommand CancelCmd { get; }

    private void CancelExecute() =>
      RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));

    public event Action<IDialogResult> RequestClose;

    public bool CanCloseDialog() =>
      true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters) {
      EventTypes = parameters.GetValue<IDictionary<ulong, string>>("event-types");
      SelectedType = EventTypes.Keys.FirstOrDefault();
    }
  }
}