using PsmsConfigurator.WpfApp.Parameters;
using Prism.Events;

namespace PsmsConfigurator.WpfApp.ViewModels
{
    public abstract class ValueParameterViewModelBase : ParameterViewModelBase
    {
        private readonly IValueParameter _valueParameter;

        protected ValueParameterViewModelBase(IEventAggregator eventAggregator, IValueParameter model)
          : base(eventAggregator, model)
        {
            _valueParameter = model;
        }

        public virtual object Value
        {
            get => _valueParameter.Value;
            set
            {
                _valueParameter.Value = value;
                SendPropertyChangedMessage(TriggerPropertyType.Value, value);
                RaisePropertyChanged(nameof(Value));
            }
        }

        protected override void ExecuteTriggerAction(PropertyChangeTrigger trigger)
        {
            base.ExecuteTriggerAction(trigger);
            if (trigger.Action == TriggerActionType.Reset)
                Value = _valueParameter.Default;
        }
    }
}
