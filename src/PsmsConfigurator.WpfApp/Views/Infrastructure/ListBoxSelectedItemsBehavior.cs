using System.Windows.Controls;
using System.Windows.Interactivity;

namespace PsmsConfigurator.WpfApp.Views.Infrastructure {
  public class ListBoxSelectedItemsBehavior : Behavior<ListBox> {
    protected override void OnAttached() => 
      AssociatedObject.SelectionChanged += AssociatedObjectSelectionChanged;

    protected override void OnDetaching() => 
      AssociatedObject.SelectionChanged -= AssociatedObjectSelectionChanged;

    private void AssociatedObjectSelectionChanged(object sender, SelectionChangedEventArgs e) {
      if (e.AddedItems.Count <= 0)
        return;
      AssociatedObject.ScrollIntoView(e.AddedItems[0]);
    }
  }
}