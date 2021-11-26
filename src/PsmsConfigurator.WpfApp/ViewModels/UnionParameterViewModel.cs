using System;
using System.Linq;
using Prism.Events;
using PsmsConfigurator.WpfApp.Parameters;

namespace PsmsConfigurator.WpfApp.ViewModels
{
    public class UnionParameterViewModel : ParameterViewModelBase
    {
        private readonly UnionParameter _unionParameter;

        public UnionParameterViewModel(IEventAggregator eventAggregator, UnionParameter model)
          : base(eventAggregator, model)
        {
            _unionParameter = model;
            Parameters = model.Parameters
              .Select(parameter => CreateParameter(eventAggregator, parameter))
              .ToArray();
        }

        public ParameterViewModelBase[] Parameters { get; }

        private static ParameterViewModelBase CreateParameter(IEventAggregator aggregator, IParameter parameter)
        {
            switch (parameter)
            {
                case BoolParameter boolParameter:
                    return new BooleanParameterViewModel(aggregator, boolParameter);
                case IntParameter intParameter:
                    return new IntParameterViewModel(aggregator, intParameter);
                case ComboParameter comboParameter:
                    return new ComboParameterViewModel(aggregator, comboParameter);
                case UnionParameter unionParameter:
                    return new UnionParameterViewModel(aggregator, unionParameter);
                default:
                    throw new NotSupportedException("Unknown parameter type.");
            }
        }
    }
}
