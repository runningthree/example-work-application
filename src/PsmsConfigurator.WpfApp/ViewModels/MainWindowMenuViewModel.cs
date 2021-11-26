using System.IO;
using Microsoft.Win32;
using NLog;
using Prism.Commands;
using Prism.Events;
using PsmsConfigurator.Shared.Models;
using PsmsConfigurator.WpfApp.Converters;
using PsmsConfigurator.WpfApp.Events;
using PsmsConfigurator.WpfApp.Factories;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class MainWindowMenuViewModel {
    private const string DefaultFileName = "Новая конфигурация";
    private const string DialogFilter = "Конфигурация (*.cfg)|*.cfg|Все файлы (*.*)|*.*";

    private readonly IEventAggregator _eventAggregator;
    private readonly ConfigurationFactory _configurationFactory;
    private readonly ConfigurationConverter _configurationConverter;
    private readonly Specification _specification;
    private readonly Logger _logger;

    private Configuration _configuration;
    private string _configurationFile;

    public MainWindowMenuViewModel(IEventAggregator eventAggregator,
      ConfigurationFactory configurationFactory,
      ConfigurationConverter configurationConverter,
      Specification specification) {
      _eventAggregator = eventAggregator;
      _configurationFactory = configurationFactory;
      _configurationConverter = configurationConverter;
      _specification = specification;
      _logger = LogManager.GetCurrentClassLogger();

      CreateConfigCmd = new DelegateCommand(CreateConfig);
      OpenConfigCmd = new DelegateCommand(OpenConfig);
      SaveConfigCmd = new DelegateCommand(SaveConfigExecute, CanSaveConfigExecute);

      Subscribe();
    }

    public DelegateCommand CreateConfigCmd { get; }

    private void CreateConfig() {
      _logger.Info("Create configuration button pressed");
      try {
        var configuration = _configurationFactory.Create(_specification);
        _eventAggregator.GetEvent<ConfigurationCreatedEvent>()
          .Publish(new ConfigurationEventArgs(configuration, DefaultFileName));
      }
      catch (System.Exception e) {
        _logger.Error(e);
      }
    }

    public DelegateCommand OpenConfigCmd { get; }

    private void OpenConfig() {
      _logger.Info("Open configuration button pressed");
      try {
        var openFileDialog = new OpenFileDialog {
          Filter = DialogFilter,
          RestoreDirectory = true,
          Multiselect = false
        };
        var dialogResult = openFileDialog.ShowDialog();
        if (dialogResult != true)
          return;
        var fileName = openFileDialog.FileName;
        var configuration = _configurationConverter
          .Convert(File.ReadAllBytes(fileName), _specification);
        _eventAggregator.GetEvent<ConfigurationCreatedEvent>()
          .Publish(new ConfigurationEventArgs(configuration,
            Path.GetFileNameWithoutExtension(fileName)));
      }
      catch (System.Exception e) {
        _logger.Error(e);
      }
    }

    public DelegateCommand SaveConfigCmd { get; }

    private void SaveConfigExecute() {
      _logger.Info("Save configuration button pressed");
      try {
        var saveFileDialog = new SaveFileDialog {
          Filter = DialogFilter,
          RestoreDirectory = true,
          FileName = _configurationFile
        };
        var dialogResult = saveFileDialog.ShowDialog();
        if (dialogResult != true)
          return;
        var fileName = saveFileDialog.FileName;
        File.WriteAllBytes(fileName, _configurationConverter
          .Convert(_configuration, _specification));
        _eventAggregator.GetEvent<ConfigurationSavedEvent>()
          .Publish(new ConfigurationEventArgs(_configuration,
            Path.GetFileNameWithoutExtension(fileName)));
      }
      catch (System.Exception e) {
        _logger.Error(e);
      }
    }

    private bool CanSaveConfigExecute() =>
      _configuration != null;

    private void Subscribe() {
      _eventAggregator.GetEvent<ConfigurationCreatedEvent>()
        .Subscribe(OnConfigurationCreated);
      _eventAggregator.GetEvent<ConfigurationOpenedEvent>()
        .Subscribe(OnConfigurationOpened);
      _eventAggregator.GetEvent<ConfigurationSavedEvent>()
        .Subscribe(OnConfigurationSaved);
    }

    private void OnConfigurationCreated(ConfigurationEventArgs args) {
      _configuration = args.Configuration;
      _configurationFile = args.FileName;
      SaveConfigCmd.RaiseCanExecuteChanged();
    }

    private void OnConfigurationOpened(ConfigurationEventArgs args) {
      _configuration = args.Configuration;
      _configurationFile = args.FileName;
      SaveConfigCmd.RaiseCanExecuteChanged();
    }

    private void OnConfigurationSaved(ConfigurationEventArgs args) {
      _configuration = args.Configuration;
      _configurationFile = args.FileName;
    }
  }
}