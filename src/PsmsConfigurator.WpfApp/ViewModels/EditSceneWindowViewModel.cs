using System;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class EditSceneWindowViewModel : BindableBase, IDialogAware {
    public const string TITLE_PARAMETER_KEY = "TITLE_PARAMETER_KEY";
    public const string EVENT_TYPE_PARAMETER_KEY = "EVENT_TYPE_PARAMETER_KEY";
    public const string OUT_NO_PARAMETER_KEY = "OUT_NO_PARAMETER_KEY";
    public const string OUT_BLINKING_PARAMETER_KEY = "OUT_BLINKING_PARAMETER_KEY";
    public const string DURATION_PARAMETER_KEY = "DURATION_PARAMETER_KEY";
    public const string DELAY_PARAMETER_KEY = "DELAY_PARAMETER_KEY";

    private readonly ApplicationState _applicationState;

    private string _title;
    private byte _eventType;
    private byte _outNo;
    private byte _outBlinking;
    private byte _duration;
    private byte _delay;

    public EditSceneWindowViewModel(ApplicationState applicationState) {
      _applicationState = applicationState;
      ApplyCmd = new DelegateCommand(Apply);
    }

    public string Title {
      get => _title;
      private set => SetProperty(ref _title, value);
    }

    public byte EventType {
      get => _eventType;
      set => SetProperty(ref _eventType, value);
    }

    public IDictionary<byte, string> EventTypeValues =>
      _applicationState.EventTypes;

    public byte OutNo {
      get => _outNo;
      set => SetProperty(ref _outNo, value);
    }

    public IDictionary<byte, string> Outputs =>
      _applicationState.Outputs;

    public byte OutBlinking {
      get => _outBlinking;
      set => SetProperty(ref _outBlinking, value);
    }

    public IDictionary<byte, string> OutBlinkingTypes =>
      _applicationState.OutBlinkingTypes;

    public byte Duration {
      get => _duration;
      set => SetProperty(ref _duration, value);
    }

    public byte Delay {
      get => _delay;
      set => SetProperty(ref _delay, value);
    }

    public event Action<IDialogResult> RequestClose;

    public DelegateCommand ApplyCmd { get; }

    private void Apply() =>
      RequestClose?.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters {
        {EVENT_TYPE_PARAMETER_KEY, EventType},
        {OUT_NO_PARAMETER_KEY, OutNo},
        {OUT_BLINKING_PARAMETER_KEY, OutBlinking},
        {DURATION_PARAMETER_KEY, Duration},
        {DELAY_PARAMETER_KEY, Delay}
      }));

    public bool CanCloseDialog() =>
      true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters) {
      Title = parameters.GetValue<string>(TITLE_PARAMETER_KEY);
      EventType = parameters.GetValue<byte>(EVENT_TYPE_PARAMETER_KEY);
      OutNo = parameters.GetValue<byte>(OUT_NO_PARAMETER_KEY);
      OutBlinking = parameters.GetValue<byte>(OUT_BLINKING_PARAMETER_KEY);
      Duration = parameters.GetValue<byte>(DURATION_PARAMETER_KEY);
      Delay = parameters.GetValue<byte>(DELAY_PARAMETER_KEY);
    }
  }
}