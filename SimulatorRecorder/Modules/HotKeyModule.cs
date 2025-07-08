using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

public static class HotKeyModule
{
    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    private const int HOTKEY_ID = 1;
    private const uint VK_SPACE = 0x20;
    private const uint MOD_NONE = 0x0000;

    public static event Action OnHotKeyPressed = null!;

    public static void RegisterHotKey(IntPtr hWnd)
    {
        RegisterHotKey(hWnd, HOTKEY_ID, MOD_NONE, VK_SPACE);
    }

    public static void UnregisterHotKey(IntPtr hWnd)
    {
        UnregisterHotKey(hWnd, HOTKEY_ID);
    }

    public static void ProcessHotKeyMessage(ref Message m)
    {
        const int WM_HOTKEY = 0x0312;
        if (m.Msg == WM_HOTKEY && (int)m.WParam == HOTKEY_ID)
        {
            OnHotKeyPressed.Invoke();
        }
    }

    public static void SendHotKeySignal()
    {
        using var udpClient = new UdpClient();
        byte[] data = Encoding.UTF8.GetBytes("SPACE_PRESSED");
        udpClient.Send(data, data.Length, "127.0.0.1", 55555);
    }
}
