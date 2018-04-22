using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UKeyFormatUtil
{
	public struct BLOCKCIPHERPARAM
	{
		[System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] IV;
		public int IVLen;
		public int PaddingType;
		public int FeedBitLength;
	}
	class SKFDelegae
	{
		public delegate int SKF_EnumDev(bool bPresent, StringBuilder szName, ref UInt32 length);
		public delegate int SKF_WaitForDevEvent(StringBuilder devName, ref UInt32 length, ref UInt32 eventType);
		public delegate int SKF_ConnectDev(StringBuilder szName, ref IntPtr devHandle);
		public delegate int SKF_GenRandom(IntPtr devHandle, byte[] random, UInt32 length);
		public delegate int SKF_SetSymmKey(IntPtr devHandle, byte[] pbKey, UInt32 algId, ref IntPtr hKeyHandle);
		public delegate int SKF_EncryptInit(IntPtr hKeyHandle, BLOCKCIPHERPARAM encryptParam);
		public delegate int SKF_Encrypt(IntPtr hKeyHandle, byte[] pbData, UInt32 dataLen, byte[] encryptedData, ref UInt32 outLen);
		public delegate int SKF_DevAuth(IntPtr devHandle, byte[] authData, UInt32 dataLen);
		public delegate int SKF_EnumApplication(IntPtr devHandle, StringBuilder szAppName, ref UInt32 dataLen);
		public delegate int SKF_DeleteApplication(IntPtr devHandle, StringBuilder szAppName);
		//public delegate int SKF_CreateApplication(IntPtr devHandle, StringBuilder szAppName, StringBuilder adminPin, uint adminPinRetryCount, StringBuilder userPin, uint userPinRetryCount, uint createFileRight, ref IntPtr hApplication);
		public delegate int SKF_CreateApplication(IntPtr devHandle, string szAppName, string adminPin, uint adminPinRetryCount, string userPin, uint userPinRetryCount, uint createFileRight, ref IntPtr hApplication);
	}
}
