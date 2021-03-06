﻿using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NtfsStreams {
	[SuppressUnmanagedCodeSecurity]
	static class NativeMethods {
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
		public struct StreamFindData {
			public long StreamSize;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260 + 36)]
			public string StreamName;
		}

		[DllImport("kernel32", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern IntPtr FindFirstStreamW(string filename, int infoLevels, out StreamFindData data, uint flags = 0);

		[DllImport("kernel32", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern bool FindNextStreamW(IntPtr hFind, out StreamFindData data);

		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		public static extern bool FindClose(IntPtr hFind);

		[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern SafeFileHandle CreateFile(string filename, uint access, uint share, IntPtr secAttributes, uint creation, uint flags, IntPtr hTemplateFile);

		public const uint GENERIC_READ = 0x80000000;
		public const uint OPEN_EXISTING = 3;
		public const uint FILE_SHARE_READ = 1;

		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr handle);

	}
}
