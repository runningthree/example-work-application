using System.Windows.Controls;
using PsmsConfigurator.WpfApp.ViewModels;

namespace PsmsConfigurator.WpfApp.Views {
  public partial class GroupsConfiguration : UserControl {
    public GroupsConfiguration() {
      InitializeComponent();
    }

    public GroupsConfigurationViewModel ConfigurationViewModel => 
      (GroupsConfigurationViewModel) DataContext;
  }
}