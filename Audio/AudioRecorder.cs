using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace VoiceTyping.Audio;

public class AudioRecorder
{
    private WasapiRecorder? recorder;
    private WaveFileWriter? writer;

    private long totalBytes;

    // Change this to part of your Bluetooth headset's microphone name.
    private const string PreferredMicrophone = "Headset";

    public bool IsRecording { get; private set; }

    public void Start(string outputFile)
    {
        if (IsRecording)
            return;

        totalBytes = 0;

        using var deviceEnumerator = new MMDeviceEnumerator();

        // Find all active recording devices.
        var devices = deviceEnumerator.EnumerateAudioEndPoints(
            DataFlow.Capture,
            DeviceState.Active);

        Console.WriteLine();
        Console.WriteLine("Available microphones:");

        MMDevice? selectedDevice = null;

        for (int i = 0; i < devices.Count; i++)
        {
            var device = devices[i];

            Console.WriteLine($"  [{i}] {device.FriendlyName}");

            // Select the Bluetooth headset microphone.
            if (device.FriendlyName.Contains(
                    PreferredMicrophone,
                    StringComparison.OrdinalIgnoreCase))
            {
                selectedDevice = device;
            }
        }

        Console.WriteLine();

        if (selectedDevice == null)
        {
            Console.WriteLine(
                $"ERROR: Could not find a microphone containing '{PreferredMicrophone}'.");

            Console.WriteLine(
                "No recording started.");

            return;
        }

        Console.WriteLine(
            $"Selected microphone: {selectedDevice.FriendlyName}");

        // Explicitly tell NAudio which microphone to use.
        recorder = new WasapiRecorderBuilder()
            .WithDevice(selectedDevice)
            .Build();

        Console.WriteLine(
            $"Audio format: {recorder.WaveFormat}");

        writer = new WaveFileWriter(
            outputFile,
            recorder.WaveFormat);

        recorder.DataAvailable += OnDataAvailable;
        recorder.RecordingStopped += OnRecordingStopped;

        recorder.StartRecording();

        IsRecording = true;

        Console.WriteLine(
            $"Recording started: {outputFile}");
    }

    public void Stop()
    {
        if (!IsRecording || recorder == null)
            return;

        recorder.StopRecording();
    }

    private void OnDataAvailable(
        ReadOnlySpan<byte> buffer,
        NAudio.CoreAudioApi.AudioClientBufferFlags flags,
        long devicePosition,
        long qpcPosition)
    {
        totalBytes += buffer.Length;

        /*if (buffer.Length > 0)
        {
            Console.WriteLine(
                $"Audio data received: {buffer.Length} bytes");
        }*/

        writer?.Write(buffer);
    }

    private void OnRecordingStopped(
        object? sender,
        StoppedEventArgs e)
    {
        writer?.Dispose();
        writer = null;

        recorder?.Dispose();
        recorder = null;

        IsRecording = false;

        Console.WriteLine(
            $"Recording stopped. Total audio data: {totalBytes} bytes");

        if (e.Exception != null)
        {
            Console.WriteLine(
                $"Recording error: {e.Exception.Message}");
        }
    }
}