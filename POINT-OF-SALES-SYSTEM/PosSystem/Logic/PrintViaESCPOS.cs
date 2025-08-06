using System.Runtime.InteropServices;
using System;

namespace PosSystem.Logic
{
    public class RawPrinterHelper
    {
        [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In] ref DOCINFOA pDI);

        [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        public static bool SendStringToPrinter(string printerName, string data)
        {
            IntPtr pPrinter = IntPtr.Zero;
            DOCINFOA di = new DOCINFOA();
            di.pDocName = "POS Printing";
            di.pDataType = "RAW";
            if (OpenPrinter(printerName, out pPrinter, IntPtr.Zero))
            {
                if (StartDocPrinter(pPrinter, 1, ref di))
                {
                    if (StartPagePrinter(pPrinter))
                    {
                        // Convert the string to an ANSI byte array
                        IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(data);
                        int dwWritten = 0;
                        WritePrinter(pPrinter, pBytes, data.Length, out dwWritten);
                        EndPagePrinter(pPrinter);
                        Marshal.FreeCoTaskMem(pBytes);
                    }
                    EndDocPrinter(pPrinter);
                }
                ClosePrinter(pPrinter);
                return true;
            }
            return false;
        }
    }
}
