using VoiceTyping.Audio;
using VoiceTyping.Input;

namespace VoiceTyping;

class Program
{
    static void Main()
    {
        Console.WriteLine("Voice Typing Tool running.");
        Console.WriteLine("Press Ctrl+Win to start/stop recording.");
        Console.WriteLine("Press Ctrl+C to exit.");

        var recorder = new AudioRecorder();

        bool isListening = false;

        using var keyboardHook = new GlobalKeyboardHook();

        keyboardHook.HotkeyPressed += () =>
        {
            if (!isListening)
            {
                Console.WriteLine(">>> LISTENING");

                recorder.Start(
                    Path.Combine(
                        AppContext.BaseDirectory,
                        "recording.wav"));

                isListening = true;
            }
            else
            {
                Console.WriteLine(">>> STOPPED");

                recorder.Stop();

                isListening = false;
            }
        };

        keyboardHook.Start();

        keyboardHook.RunMessageLoop();
    }
}