using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Beyondmem;
using Memory;
using Guna.UI2.WinForms;

namespace AimbotOnOff
{
    public partial class Form1 : Form
    {
        Mem memory = new Mem();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private static IntPtr hookID = IntPtr.Zero;
        private LowLevelKeyboardProc hookCallback;

        private bool waitPressKey = false;
        public Form1()
        {
            InitializeComponent();
        }


        public static MemMirza Memlib = new MemMirza();


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        public string PID;
        Dictionary<long, byte[]> originalValues = new Dictionary<long, byte[]>();
        private async void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
            Memlib.OpenProcess(proc);

            Memlib.OpenProcess(Convert.ToInt32(PID));

            IEnumerable<long> longs = await Memlib.AoBScan2(("FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF"), true);

            if (longs == null)
                label5.Text = "No Address Found";
            Console.Beep(240, 300);

            foreach (long num in longs)
            {
                string str = num.ToString("X");
                {
                    Byte[] originalBytes = Memlib.AhReadMeFucker((num + 0x5c).ToString("X"), 4);

                    originalValues.Add(num, originalBytes);

                    Byte[] valueBytes = Memlib.AhReadMeFucker((num + 0x60).ToString("X"), 4);


                    Memlib.WriteMemory((num + 0x5c).ToString("X"), "int", BitConverter.ToInt32(valueBytes , 0).ToString());

                }
                label3.Text = "Aimbot Done";
                label3.ForeColor = Color.AliceBlue;
                Console.Beep(400, 300);
            }


        }

        private async void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (guna2ToggleSwitch1.Checked)
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                Memlib.OpenProcess(proc);

                Memlib.OpenProcess(Convert.ToInt32(PID));

                IEnumerable<long> longs = await Memlib.AoBScan2(("FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF"), true);

                if (longs == null)
                    label5.Text = "No Address Found";

                foreach (long num in longs)
                {
                    string str = num.ToString("X");
                    {
                        Byte[] originalBytes = Memlib.AhReadMeFucker((num + 0x5c).ToString("X"), 4);

                        originalValues.Add(num, originalBytes);

                        Byte[] valueBytes = Memlib.AhReadMeFucker((num + 0x60).ToString("X"), 4);


                        Memlib.WriteMemory((num + 0x5c).ToString("X"), "int", BitConverter.ToInt32(valueBytes, 0).ToString());

                    }
                    label3.Text = "Aimbot Done";
                    label3.ForeColor = Color.AliceBlue;
                }

            }
            else
            {
                Memlib.OpenProcess(Convert.ToInt32((PID)));
                foreach (var entity in originalValues)
                {
                    Memlib.WriteMemory((entity.Key + 0x5c).ToString("X"), "int", BitConverter.ToInt32(entity.Value, 0).ToString());
                }
                originalValues.Clear();
                label3.Text = "Disable Done";
                label3.ForeColor = Color.AliceBlue;

                Memlib.CloseProcess();

            }
        }
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
        uint dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        private WebClient webclient = new WebClient();
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    //Type Here Emulator Not Found
                    label5.Text = "Emulator Not Found";
                    Console.Beep(240, 300);
                }
                else
                {
                    string dllName = "ChamsRed.dll";
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    string adress = "https://cdn.glitch.global/74002823-d235-4cf1-ba34-36967b91f68e/ChamsRed.dll?v=1717190149207";
                    string fileName = $"C:\\Windows\\System32\\{dllName}";
                    bool flag = File.Exists(fileName);

                    if (flag)
                    {
                        File.Delete(fileName);
                    }
                    this.webclient.DownloadFile(adress, fileName);
                    Process targetProcess = Process.GetProcessesByName("HD-Player").FirstOrDefault();
                    if (targetProcess != null)
                    {
                        label5.Text = "Chams Red Injected";
                    }
                    IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
                    IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
                    UIntPtr bytesWritten;
                    WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(dllName), (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
                    CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
                }
            }
            catch
            {
                Console.Beep(240, 300);
                //Type Here Chams Is Already Injected or code invaible
                label5.Text = "Chams Red Is Already injected or link is inccorect!!";
            }

        }

        private async void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                label5.Text = "Emulator Not Found";
                Console.Beep(240, 300);
            }
            else
            {
                //Type Here Waiting Status
                label5.Text = "Wait to inject Awm Scop...";

                string search = "60 40 CD CC 8C 3F 8F C2 F5 3C CD CC CC 3D 07 00 00 00 00 00 00 00 00 00 00 00 00 00 F0 41 00 00 48 42 00 00 00 3F 33 33 13 40 00 00 B0 3F 00 00 80 3F 01";
                string replace = "60 40 CD CC 8C 3F 8F C2 F5 3C CD CC CC 3D 07 00 00 00 00 00 FF FF 00 00 00 00 00 00 F0 41 00 00 48 42 00 00 00 3F 33 33 13 40 00 00 B0 3F 00 00 80 3F 01";

                bool k = false;
                memory.OpenProcess("HD-Player");

                int i2 = 22000000;
                IEnumerable<long> wl = await memory.AoBScan(search, writable: true);
                string u = "0x" + wl.FirstOrDefault().ToString("X");
                if (wl.Count() != 0)
                {
                    for (int i = 0; i < wl.Count(); i++)
                    {
                        i2++;
                        memory.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                    }
                    k = true;
                }

                if (k == true)
                {
                    Console.Beep(400, 300);
                    //Type Here Code Inject Success Status
                    label5.Text = "Inject Awm Scop Success";

                }
                else
                {
                    //Type Here Code Inject Faild Status
                    label5.Text = "Inject Awm Scop Faild";
                    Console.Beep(240, 300);
                }
            }
        }
    }
}
