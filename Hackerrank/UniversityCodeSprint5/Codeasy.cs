using System;
using System.IO;
using System.Runtime.InteropServices;

public static class HelloWorld2 {
    static string exePath;
    public static void Main2() {
        exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        while (true)
            StartupNotepad();

        Console.WriteLine("===================== Done =====================");
    }


    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern bool CreateProcess(
         string lpApplicationName,
         string lpCommandLine,
         ref SECURITY_ATTRIBUTES lpProcessAttributes,
         ref SECURITY_ATTRIBUTES lpThreadAttributes,
         bool bInheritHandles,
         uint dwCreationFlags,
         IntPtr lpEnvironment,
         string lpCurrentDirectory,
         [In] ref STARTUPINFO lpStartupInfo,
         out PROCESS_INFORMATION lpProcessInformation);

    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES {
        public int nLength;
        public IntPtr lpSecurityDescriptor;
        public int bInheritHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESS_INFORMATION {
        public IntPtr hProcess;
        public IntPtr hThread;
        public int dwProcessId;
        public int dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct STARTUPINFO {
        public Int32 cb;
        public string lpReserved;
        public string lpDesktop;
        public string lpTitle;
        public Int32 dwX;
        public Int32 dwY;
        public Int32 dwXSize;
        public Int32 dwYSize;
        public Int32 dwXCountChars;
        public Int32 dwYCountChars;
        public Int32 dwFillAttribute;
        public Int32 dwFlags;
        public Int16 wShowWindow;
        public Int16 cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    public static void StartupNotepad() {
        const uint NORMAL_PRIORITY_CLASS = 0x0020;

        bool retValue;
        string Application = Environment.GetEnvironmentVariable("windir") + @"\Notepad.exe";
        Application = @"C:\codeasy\Codeasy.CodeChecker.HandlerSvc.exe";
        Console.WriteLine(File.Exists(Application));
        Console.WriteLine(Application);
        string CommandLine = "";
        PROCESS_INFORMATION pInfo = new PROCESS_INFORMATION();
        STARTUPINFO sInfo = new STARTUPINFO();
        SECURITY_ATTRIBUTES pSec = new SECURITY_ATTRIBUTES();
        SECURITY_ATTRIBUTES tSec = new SECURITY_ATTRIBUTES();
        pSec.nLength = Marshal.SizeOf(pSec);
        tSec.nLength = Marshal.SizeOf(tSec);

        retValue = CreateProcess(Application, CommandLine, ref pSec, ref tSec, false, NORMAL_PRIORITY_CLASS, IntPtr.Zero, null, ref sInfo, out pInfo);
        Console.WriteLine(retValue);
    }
}