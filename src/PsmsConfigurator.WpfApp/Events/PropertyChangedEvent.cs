using System;
using PsmsConfigurator.WpfApp.Parameters;
using Prism.Events;

namespace PsmsConfigurator.WpfApp.Events
{
    public class PropertyChangedEvent : PubSubEvent<PropertyChangedEventArgs> { }

    public class PropertyChangedEventArgs : EventArgs
    {
        public string Tag { get; set; }
        public TriggerPropertyType Property { get; set; }
        public object Value { get; set; }
    }
}
