using Avalonia.Controls;

namespace ngnet_native.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void TheWebView_EnvironmentRequested(object? sender, WebViewEnvironmentRequestedEventArgs e)
    {
#if DEBUG
        e.EnableDevTools = true;
#endif
    }
}