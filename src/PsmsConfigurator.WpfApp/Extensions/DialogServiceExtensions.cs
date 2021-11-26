using System;
using Prism.Services.Dialogs;
using PsmsConfigurator.WpfApp.Views;

namespace PsmsConfigurator.WpfApp.Extensions {
  public static class DialogServiceExtensions {
    public static void ShowRemoveMessageBox(this IDialogService dialogService, string title, string message, Action<IDialogResult> action) =>
      dialogService.Show(nameof(CustomMessageBox), new DialogParameters {
        {"title", title},
        {"message", message},
        {"apply-text", "Удалить"},
        {"cancel-text", "Закрыть"}
      }, action);
  }
}