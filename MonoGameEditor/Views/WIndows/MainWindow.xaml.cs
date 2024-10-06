using MonoGameEditor.MonoGameControls;
using System;
using System.Windows;

namespace MonoGameEditor;

public partial class MainWindow : Window
{
    public MonoGameContentControl ContentControl { get; init; }

    public MainWindow()
    {
        InitializeComponent();

        ContentControl = this.mg_control;
    }   
}
