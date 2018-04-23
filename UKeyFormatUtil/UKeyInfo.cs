using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKeyFormatUtil
{
	class UKeyInfo
	{
		public static readonly string SM2 = "SM2";
		public string UKeyName;
		public string UKeyType;
		public string CertType;

		public string AppName;
		public string AdminPin;
		public uint AdminPinCount;
		public string UserPin;
		public uint UserPinCount;

		public int CreateFlag;
		public string SKFDllName;
		public uint AuthAlg;
	}
}
