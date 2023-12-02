using DirectoryMonitor.ViewLib.Extensions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace DirectoryMonitor.WpfApp.Extensions;

public static class MetroWindowExtensions
{
    public static MessageDialogResult ShowOkMessageBox(this MetroWindow window, string title, string message) =>
        window.ShowModalMessageExternal(
            Application.Current.TryFindStringResource(title, title),
            message,
            MessageDialogStyle.Affirmative,
            new MetroDialogSettings
        {
            AffirmativeButtonText = Application.Current.TryFindStringResource("ButtonName_Ok", "OK"),
            ColorScheme = MetroDialogColorScheme.Accented,
            DefaultButtonFocus = MessageDialogResult.Affirmative,
            DialogResultOnCancel = MessageDialogResult.Affirmative
        });

    public static MessageDialogResult ShowOkCancelMessageBox(this MetroWindow window, string title, string message) =>
        window.ShowModalMessageExternal(
            Application.Current.TryFindStringResource(title, title),
            message,
            MessageDialogStyle.AffirmativeAndNegative,
            new MetroDialogSettings
        {
            AffirmativeButtonText = Application.Current.TryFindStringResource("ButtonName_Ok", "OK"),
            NegativeButtonText = Application.Current.TryFindStringResource("ButtonName_Cancel", "Cancel"),
            ColorScheme = MetroDialogColorScheme.Accented,
            DefaultButtonFocus = MessageDialogResult.Affirmative,
            DialogResultOnCancel = MessageDialogResult.Negative
        });

    public static MessageDialogResult ShowYesNoMessageBox(this MetroWindow window, string title, string message) =>
        window.ShowModalMessageExternal(
            Application.Current.TryFindStringResource(title, title),
            message,
            MessageDialogStyle.AffirmativeAndNegative,
            new MetroDialogSettings
        {
            AffirmativeButtonText = Application.Current.TryFindStringResource("ButtonName_Yes", "Yes"),
            NegativeButtonText = Application.Current.TryFindStringResource("ButtonName_No", "No"),
            ColorScheme = MetroDialogColorScheme.Accented,
            DefaultButtonFocus = MessageDialogResult.Negative,
            DialogResultOnCancel = MessageDialogResult.Negative
        });

    public static MessageDialogResult ShowYesNoCancelMessageBox(this MetroWindow window, string title, string message) =>
        window.ShowModalMessageExternal(
            Application.Current.TryFindStringResource(title, title),
            message,
            MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary,
            new MetroDialogSettings
        {
            AffirmativeButtonText = Application.Current.TryFindStringResource("ButtonName_Yes", "Yes"),
            NegativeButtonText = Application.Current.TryFindStringResource("ButtonName_No", "No"),
            FirstAuxiliaryButtonText = Application.Current.TryFindStringResource("ButtonName_Cancel", "Cancel"),
            ColorScheme = MetroDialogColorScheme.Accented,
            DefaultButtonFocus = MessageDialogResult.FirstAuxiliary,
            DialogResultOnCancel = MessageDialogResult.FirstAuxiliary
        });
}