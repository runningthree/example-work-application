using System.Collections.Generic;
using Prism.Events;
using PsmsConfigurator.WpfApp.Parameters;

namespace PsmsConfigurator.WpfApp.ViewModels
{
    public class ComboParameterViewModel : ValueParameterViewModelBase
    {
        public ComboParameterViewModel(IEventAggregator eventAggregator, ComboParameter model)
          : base(eventAggregator, model)
        {
            Items = model.Items;
        }

        public IDictionary<int, string> Items { get; }
    }
}
