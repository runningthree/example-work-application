using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class CustomMessageBoxViewModel : BindableBase, IDialogAware {
    private string _title;
    private string _message;
    private string _applyText;
    private string _cancelText;

    public CustomMessageBoxViewModel() {
      ApplyCmd = new DelegateCommand(ApplyExecute);
      CancelCmd = new DelegateCommand(CancelExecute);
    }

    public string Title {
      get => _title;
      private set => SetProperty(ref _title, value);
    }

    public string Message {
      get => _message;
      private set => SetProperty(ref _message, value);
    }
    
    public event Action<IDialogResult> RequestClose;

    public string ApplyText {
      get => _applyText;
      set => SetProperty(ref _applyText, value);
    }

    public DelegateCommand ApplyCmd { get; }

    private void ApplyExecute() => 
      RequestClose?.Invoke(new DialogResult(ButtonResult.OK));

    public string CancelText {
      get => _cancelText;
      set => SetProperty(ref _cancelText, value);
    }
    
    public DelegateCommand CancelCmd { get; }
    
    private void CancelExecute() =>
      RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));

    public bool CanCloseDialog() => 
      true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters) {
      Title = parameters.GetValue<string>("title");
      Message = parameters.GetValue<string>("message");
      ApplyText = parameters.GetValue<string>("apply-text");
      CancelText = parameters.GetValue<string>("cancel-text");
    }
  }
}