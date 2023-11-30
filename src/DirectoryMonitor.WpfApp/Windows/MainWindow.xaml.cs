using ControlzEx.Theming;
using DirectoryMonitor.ViewLib.Extensions;
using DirectoryMonitor.ViewLib.Input;
using DirectoryMonitor.ViewLib.ObjectModels;
using WPFSharp.Globalizer;

namespace DirectoryMonitor.WpfApp.Windows;

public partial class MainWindow
{
    private readonly ILogger<MainWindow> _logger;

    public MainWindow(
        ILogger<MainWindow> logger)
    {
        _logger = logger;

        AvailableLanguages = AvailableLanguages.Instance;
        ThemeManager = ThemeManager.Current;
        SelectedTheme = ThemeManager.Current.DetectTheme();
        BaseColors = ThemeManager.Current.ToSortableObservableCollection();

        InitializeComponent();

        this.DataContext = this;

        ThemeManager.ThemeChanged += ThemeManagerOnThemeChanged;
    }

    private static readonly DependencyProperty AvailableLanguagesProperty = DependencyProperty.Register(
        nameof(AvailableLanguages), typeof(AvailableLanguages), typeof(MainWindow), new PropertyMetadata(default(AvailableLanguages)));
    internal AvailableLanguages AvailableLanguages
    {
        get => (AvailableLanguages) GetValue(AvailableLanguagesProperty);
        init => SetValue(AvailableLanguagesProperty, value);
    }

    private static readonly DependencyProperty ThemeManagerProperty = DependencyProperty.Register(
        nameof(ThemeManager), typeof(ThemeManager), typeof(MainWindow), new PropertyMetadata(default(ThemeManager)));
    internal ThemeManager ThemeManager
    {
        get => (ThemeManager) GetValue(ThemeManagerProperty);
        init => SetValue(ThemeManagerProperty, value);
    }

    private static readonly DependencyProperty SelectedThemeProperty = DependencyProperty.Register(
        nameof(SelectedTheme), typeof(Theme), typeof(MainWindow), new PropertyMetadata(default(Theme)));
    internal Theme? SelectedTheme
    {
        get => (Theme?) GetValue(SelectedThemeProperty);
        set => SetValue(SelectedThemeProperty, value);
    }

    private static readonly DependencyProperty BaseColorsProperty = DependencyProperty.Register(
        nameof(BaseColors), typeof(SortableObservableCollection<ThemeBaseColor>), typeof(MainWindow), new PropertyMetadata(default(SortableObservableCollection<ThemeBaseColor>)));
    internal SortableObservableCollection<ThemeBaseColor> BaseColors
    {
        get => (SortableObservableCollection<ThemeBaseColor>) GetValue(BaseColorsProperty);
        init => SetValue(BaseColorsProperty, value);
    }

    public ICommand ApplicationExitCommand =>
        new RelayCommand(
            execute: ExecuteApplicationExitCommand,
            canExecute: () => true
        );

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

    private void ExecuteApplicationExitCommand()
    {
        Application.Current.Shutdown(0);
        _logger.LogDebug("Exiting application");
    }

    private void ExecuteSelectLanguageCommand(object? inLanguage)
    {
        var language = inLanguage as string;
        if (string.IsNullOrWhiteSpace(language))
            return;

        GlobalizedApplication.Instance.GlobalizationManager.SwitchLanguage(language);
        _logger.LogDebug("Language switched to {Language}", language);
    }

    private void ExecuteSelectThemeCommand(ThemeColorScheme? colorScheme)
    {
        if (colorScheme?.Theme is null)
            return;

        ThemeManager.Current.ChangeTheme(this, colorScheme.Theme);
        _logger.LogDebug("Theme switched to {Theme}", colorScheme.Theme.DisplayName);
    }

    private void ThemeManagerOnThemeChanged(object? sender, ThemeChangedEventArgs e)
    {
        SelectedTheme = e.NewTheme;
        _logger.LogDebug("Theme switched from {OldTheme} to {NewTheme}", e.OldTheme?.DisplayName, e.NewTheme.DisplayName);
    }
}