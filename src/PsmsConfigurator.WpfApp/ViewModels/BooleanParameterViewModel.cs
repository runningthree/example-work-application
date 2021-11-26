using Prism.Events;
using PsmsConfigurator.WpfApp.Parameters;

namespace PsmsConfigurator.WpfApp.ViewModels
{
    public class BooleanParameterViewModel : ValueParameterViewModelBase
    {
        public BooleanParameterViewModel(IEventAggregator eventAggregator, BoolParameter boolParameter)
          : base(eventAggregator, boolParameter) { }
    }
}
