using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VoiceTyping.Input;

public class GlobalKeyboardHook : IDisposable
{
    private const int WH_KEYBOARD_LL = 13;

    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KEYUP = 0x0101;
    private const int WM_SYSKEYDOWN = 0x0104;
    private const int WM_SYSKEYUP = 0x0105;

    private const int VK_LCONTROL = 0xA2;
    private const int VK_RCONTROL = 0xA3;
    private const int VK_LWIN = 0x5B;
    private const int VK_RWIN = 0x5C;

    private IntPtr hookId = IntPtr.Zero;

    private bool ctrlDown;
    private bool winDown;

    private readonly LowLevelKeyboardProc hookCallback;

    public event Action? HotkeyPressed;

    public GlobalKeyboardHook()
    {
        hookCallback = HookCallback;
    }

    public void Start()
    {
        if (hookId != IntPtr.Zero)
            return;

        hookId = SetWindowsHookEx(
            WH_KEYBOARD_LL,
            hookCallback,
            IntPtr.Zero,
            0);

        if (hookId == IntPtr.Zero)
        {
            throw new Win32Exception(
                Marshal.GetLastWin32Error());
        }

        Console.WriteLine(
            $"Global keyboard hook installed. Handle = {hookId}");
    }

    public void RunMessageLoop()
    {
        Console.WriteLine("Keyboard message loop starting...");

        int result = GetMessage(
            out MSG msg,
            IntPtr.Zero,
            0,
            0);

        Console.WriteLine(
            $"GetMessage returned: {result}");

        if (result == -1)
        {
            Console.WriteLine(
                $"GetMessage failed. Win32 error: {Marshal.GetLastWin32Error()}");

            return;
        }

        if (result == 0)
        {
            Console.WriteLine("GetMessage received WM_QUIT.");

            return;
        }

        Console.WriteLine(
            $"First message received: 0x{msg.message:X}");

        TranslateMessage(ref msg);
        DispatchMessage(ref msg);

        // Continue processing messages.
        while (GetMessage(
            out msg,
            IntPtr.Zero,
            0,
            0) > 0)
        {
            TranslateMessage(ref msg);
            DispatchMessage(ref msg);
        }

        Console.WriteLine("Keyboard message loop ended.");
    }

    private IntPtr HookCallback(
        int nCode,
        IntPtr wParam,
        IntPtr lParam)
    {
        if (nCode >= 0)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            Console.WriteLine(
                $"KEY: VK=0x{vkCode:X2}, message=0x{wParam.ToInt64():X}");

            bool keyDown =
                wParam == (IntPtr)WM_KEYDOWN ||
                wParam == (IntPtr)WM_SYSKEYDOWN;

            bool keyUp =
                wParam == (IntPtr)WM_KEYUP ||
                wParam == (IntPtr)WM_SYSKEYUP;

            if (keyDown)
            {
                if (vkCode == VK_LCONTROL ||
                    vkCode == VK_RCONTROL)
                {
                    ctrlDown = true;
                }

                if (vkCode == VK_LWIN ||
                    vkCode == VK_RWIN)
                {
                    if (ctrlDown && !winDown)
                    {
                        winDown = true;

                        Console.WriteLine(
                            ">>> CTRL+WIN DETECTED");

                        HotkeyPressed?.Invoke();
                    }
                    else
                    {
                        winDown = true;
                    }
                }
            }

            if (keyUp)
            {
                if (vkCode == VK_LCONTROL ||
                    vkCode == VK_RCONTROL)
                {
                    ctrlDown = false;
                }

                if (vkCode == VK_LWIN ||
                    vkCode == VK_RWIN)
                {
                    winDown = false;
                }
            }
        }

        return CallNextHookEx(
            IntPtr.Zero,
            nCode,
            wParam,
            lParam);
    }

    public void Dispose()
    {
        if (hookId != IntPtr.Zero)
        {
            UnhookWindowsHookEx(hookId);
            hookId = IntPtr.Zero;

            Console.WriteLine(
                "Global keyboard hook removed.");
        }
    }

    // ============================================================
    // Windows API
    // ============================================================

    private delegate IntPtr LowLevelKeyboardProc(
        int nCode,
        IntPtr wParam,
        IntPtr lParam);

    [DllImport(
        "user32.dll",
        SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(
        int idHook,
        LowLevelKeyboardProc lpfn,
        IntPtr hMod,
        uint dwThreadId);

    [DllImport(
        "user32.dll",
        SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(
        IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(
        IntPtr hhk,
        int nCode,
        IntPtr wParam,
        IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern int GetMessage(
        out MSG lpMsg,
        IntPtr hWnd,
        uint wMsgFilterMin,
        uint wMsgFilterMax);

    [DllImport("user32.dll")]
    private static extern bool TranslateMessage(
        ref MSG lpMsg);

    [DllImport("user32.dll")]
    private static extern IntPtr DispatchMessage(
        ref MSG lpMsg);

    [StructLayout(LayoutKind.Sequential)]
    private struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public UIntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public int pt_x;
        public int pt_y;
    }
}