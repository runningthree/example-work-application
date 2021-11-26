using System;
using Prism.Events;
using PsmsConfigurator.WpfApp.Parameters;

namespace PsmsConfigurator.WpfApp.ViewModels
{
    public class IntParameterViewModel : ValueParameterViewModelBase
    {
        private IntParameter _intParameter;

        public IntParameterViewModel(IEventAggregator eventAggregator, IntParameter model)
          : base(eventAggregator, model)
        {
            _intParameter = model;
        }

        public override object Value
        {
            get => Convert.ToDouble(_intParameter.Value);
            set => base.Value = Convert.ToInt32(value);
        }

        public double Minimum =>
          _intParameter.MinValue;

        public double Maximum =>
          _intParameter.MaxValue;
    }
}
