using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UKeyFormatUtil
{
	
	class DynamicLibUtil
	{
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string lpFileName);
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
		[DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
		public static extern bool FreeLibrary(IntPtr hModule); 
	}
}
