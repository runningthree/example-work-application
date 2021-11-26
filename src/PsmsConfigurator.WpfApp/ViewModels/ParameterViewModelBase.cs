using PsmsConfigurator.WpfApp.Parameters;
using PsmsConfigurator.WpfApp.Events;
using System.Linq;
using Prism.Events;
using Prism.Mvvm;

namespace PsmsConfigurator.WpfApp.ViewModels
{
    public abstract class ParameterViewModelBase : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        protected ParameterViewModelBase(IEventAggregator eventAggregator, IParameter model)
        {
            _eventAggregator = eventAggregator;
            Model = model;
            Subscribe();
        }

        public IParameter Model { get; }

        public string Name =>
          Model.Name;

        public bool Enable
        {
            get => Model.Enable;
            set
            {
                Model.Enable = value;
                SendPropertyChangedMessage(TriggerPropertyType.Enabled, value);
                RaisePropertyChanged(nameof(Enable));
            }
        }

        protected void SendPropertyChangedMessage(TriggerPropertyType propertyType, object value) =>
          _eventAggregator.GetEvent<PropertyChangedEvent>()
            .Publish(new PropertyChangedEventArgs
            {
                Tag = Model.Tag,
                Value = value,
                Property = propertyType
            });

        private void Subscribe()
        {
            _eventAggregator.GetEvent<PropertyChangedEvent>()
              .Subscribe(OnPropertyChanged);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (Model.PropertyChangeTriggers == null || Model.PropertyChangeTriggers.Length <= 0)
                return;
            var triggers = Model.PropertyChangeTriggers
              .Where(trigger => trigger.Property == args.Property &&
                                trigger.Tag == args.Tag &&
                                Equals(trigger.Value, args.Value))
              .ToArray();
            if (triggers.Length <= 0)
                return;
            foreach (var trigger in triggers)
            {
                ExecuteTriggerAction(trigger);
            }
        }

        protected virtual void ExecuteTriggerAction(PropertyChangeTrigger trigger)
        {
            switch (trigger.Action)
            {
                case TriggerActionType.Enable:
                    Enable = true;
                    break;
                case TriggerActionType.Disable:
                    Enable = false;
                    break;
            }
        }
    }
}
