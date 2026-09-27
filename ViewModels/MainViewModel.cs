using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ngnet_native.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Title { get; set; } = "NgNetNative Proof-of-Concept!";

    [ObservableProperty]
    public partial Uri? BackendUri { get; set; }
}
