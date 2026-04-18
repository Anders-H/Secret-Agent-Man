using System;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Application = System.Windows.Application;

namespace SecretAgentMan;

public static class VideoIntroPlayer
{
    public static void Play(string videoFileName)
    {
        var basePath = AppContext.BaseDirectory;
        var videoPath = Path.Combine(basePath, videoFileName);

        if (!File.Exists(videoPath))
            return;

        var app = new Application { ShutdownMode = ShutdownMode.OnExplicitShutdown };
        var window = new VideoIntroWindow(videoPath);
        window.ShowDialog();

        if (Application.Current != null)
            Application.Current.Shutdown();
    }
}

public sealed class VideoIntroWindow : Window
{
    private readonly MediaElement _media;

    public VideoIntroWindow(string videoPath)
    {
        Title = string.Empty;
        WindowStyle = WindowStyle.None;
        ResizeMode = ResizeMode.NoResize;
        WindowState = WindowState.Maximized;
        Background = Brushes.Black;
        Focusable = true;
#if DEBUG
        ShowInTaskbar = true;
        Topmost = false;
#else
        ShowInTaskbar = false;
        Topmost = true;
#endif

        _media = new MediaElement
        {
            Source = new Uri(videoPath, UriKind.Absolute),
            LoadedBehavior = MediaState.Manual,
            UnloadedBehavior = MediaState.Stop,
            Stretch = Stretch.Uniform,
            Volume = 1.0,
        };

        _media.MediaEnded += OnMediaEnded;
        _media.MediaFailed += OnMediaFailed;
        Content = _media;
        KeyDown += CloseOnKeyDown;
        MouseLeftButtonDown += (_, _) => CloseWindow();
        Closing += (_, _) =>
        {
            var windowInteropHelper = new WindowInteropHelper(this);
            Buffer.FlushMouseBuffer(windowInteropHelper.EnsureHandle());
            Buffer.FlushKeyboardBuffer(windowInteropHelper.EnsureHandle());
        };

        // Starta uppspelning när fönstret är inläst
        Loaded += (_, _) => _media.Play();
    }

    private void CloseOnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.LeftCtrl || e.Key == System.Windows.Input.Key.Space || e.Key == System.Windows.Input.Key.Escape)
        {
            KeyDown -= CloseOnKeyDown;
            var windowInteropHelper = new WindowInteropHelper(this);
            Buffer.FlushMouseBuffer(windowInteropHelper.EnsureHandle());
            Buffer.FlushKeyboardBuffer(windowInteropHelper.EnsureHandle());
            CloseWindow();
        }
    }

    // Videon är klar – starta mjuk fade-out
    private void OnMediaEnded(object sender, RoutedEventArgs e) =>
        CloseWindow();

    // Uppspelning misslyckades – stäng direkt utan att krascha
    private void OnMediaFailed(object sender, ExceptionRoutedEventArgs e) =>
        CloseWindow();

    private void CloseWindow()
    {
        _media.Stop();
        Close();
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct WindowsMessage
{
    public IntPtr handle;
    public uint msg;
    public IntPtr wParam;
    public IntPtr lParam;
    public uint time;
    public Point p;
}

public class Buffer
{
    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern bool PeekMessage(out WindowsMessage message, IntPtr handle, uint filterMin, uint filterMax, uint flags);

    private static void FlushBuffer(IntPtr handle, uint first, uint last)
    {
        const uint pmRemove = 1;
        do
        {

            if (!PeekMessage(out var windowsMessage, handle, first, last, pmRemove))
                return;

        } while (true);
    }

    public static void FlushKeyboardBuffer(IntPtr handle)
    {
        const uint wmKeyFirst = 256;
        const uint wmKeyLast = 264;

        FlushBuffer(handle, wmKeyFirst, wmKeyLast);
    }

    public static void FlushMouseBuffer(IntPtr handle)
    {
        const uint wmMouseFirst = 512;
        const uint wmMouseLast = 526;

        FlushBuffer(handle, wmMouseFirst, wmMouseLast);
    }
}