using ControlzEx.Theming;
using DirectoryMonitor.ViewLib.Input;
using DirectoryMonitor.ViewLib.ObjectModels;
using WPFSharp.Globalizer;

namespace DirectoryMonitor.WpfApp.Windows;

public partial class MainWindow
{
    private readonly ILogger<MainWindow> _logger;

    public MainWindow(
        AvailableLanguages availableLanguages,
        AvailableThemes availableThemes,
        ILogger<MainWindow> logger)
    {
        _logger = logger;

        AvailableLanguages = availableLanguages;
        AvailableThemes = availableThemes;

        InitializeComponent();

        this.DataContext = this;

        GlobalizedApplication.Instance.GlobalizationManager.ResourceDictionaryChangedEvent += GlobalizationManagerResourceDictionaryChanged;
        ThemeManager.Current.ThemeChanged += ThemeManagerOnThemeChanged;
    }

    private static readonly DependencyProperty AvailableLanguagesProperty = DependencyProperty.Register(
        nameof(AvailableLanguages), typeof(AvailableLanguages), typeof(MainWindow), new PropertyMetadata(default(AvailableLanguages)));
    internal AvailableLanguages AvailableLanguages
    {
        get => (AvailableLanguages) GetValue(AvailableLanguagesProperty);
        init => SetValue(AvailableLanguagesProperty, value);
    }

    private static readonly DependencyProperty AvailableThemesProperty = DependencyProperty.Register(
        nameof(AvailableThemes), typeof(AvailableThemes), typeof(MainWindow), new PropertyMetadata(default(AvailableThemes)));
    internal AvailableThemes AvailableThemes
    {
        get => (AvailableThemes) GetValue(AvailableThemesProperty);
        init => SetValue(AvailableThemesProperty, value);
    }

    public ICommand SelectLanguageCommand =>
        new RelayCommand<object>(
            execute: ExecuteSelectLanguageCommand,
            canExecute: _ => true
        );

    public ICommand SelectThemeCommand =>
        new RelayCommand<ThemeColorScheme>(
            execute: ExecuteSelectThemeCommand,
            canExecute: _ => true
        );

    private void ExecuteSelectLanguageCommand(object? inLanguage)
    {
        var language = inLanguage as string;
        if (string.IsNullOrWhiteSpace(language))
            return;

        _logger.LogDebug("Language {Language} selected", language);
        GlobalizedApplication.Instance.GlobalizationManager.SwitchLanguage(language);
    }

    private void ExecuteSelectThemeCommand(ThemeColorScheme? colorScheme)
    {
        if (colorScheme?.Theme is null)
            return;

        _logger.LogDebug("Theme {Theme} selected", colorScheme.Theme.DisplayName);
        ThemeManager.Current.ChangeTheme(GlobalizedApplication.Instance, colorScheme.Theme);
    }

    private void GlobalizationManagerResourceDictionaryChanged(object source, ResourceDictionaryChangedEventArgs e)
    {
        //_logger.LogDebug("Language switched from {OldLanguage} to {NewLanguage}", SelectedLanguage, AvailableLanguages.SelectedLanguage);
    }

    private void ThemeManagerOnThemeChanged(object? sender, ThemeChangedEventArgs e)
    {
        _logger.LogDebug("Theme switched from {OldTheme} to {NewTheme}", e.OldTheme?.DisplayName, e.NewTheme.DisplayName);
    }
}