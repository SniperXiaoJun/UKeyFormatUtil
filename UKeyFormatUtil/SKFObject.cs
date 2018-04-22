using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UKeyFormatUtil
{
	class SKFObject
	{
		private string dllName;
		private IntPtr hModule;
		private IntPtr skf_enumDevP;
		private IntPtr skf_connectDevP;
		private IntPtr skf_createApplicationP;
		private IntPtr skf_deleteApplicationP;
		private IntPtr skf_devAuthP;
		private IntPtr skf_encryptP;
		private IntPtr skf_encryptInitP;
		private IntPtr skf_enumApplicationP;
		private IntPtr skf_genRandomP;
		private IntPtr skf_setSymmKeyP;
		private IntPtr skf_waitForDevEventP;

		public SKFDelegae.SKF_EnumDev skf_enumDev;
		public SKFDelegae.SKF_ConnectDev skf_connectDev;
		public SKFDelegae.SKF_CreateApplication skf_createApplication;
		public SKFDelegae.SKF_DeleteApplication skf_deleteApplication;
		public SKFDelegae.SKF_DevAuth skf_devAuth;
		public SKFDelegae.SKF_Encrypt skf_encrypt;
		public SKFDelegae.SKF_EncryptInit skf_encryptInit;
		public SKFDelegae.SKF_EnumApplication skf_enumApplication;
		public SKFDelegae.SKF_GenRandom skf_genRandom;
		public SKFDelegae.SKF_SetSymmKey skf_setSymmKey;
		public SKFDelegae.SKF_WaitForDevEvent skf_waitForDevEvent;
		public SKFObject(string dllName) 
		{
			this.dllName = dllName;
			this.hModule = IntPtr.Zero;
		}
		~SKFObject()
		{
			if (this.hModule != IntPtr.Zero)
			{
				DynamicLibUtil.FreeLibrary(this.hModule);
			}
		}
		public int newInstance()
		{
			hModule = DynamicLibUtil.LoadLibrary(this.dllName);
			if (hModule == IntPtr.Zero)
			{
				return 1001;
			}
			this.skf_enumDevP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			this.skf_enumDev = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_enumDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_connectDevP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_ConnectDev");
			this.skf_connectDev = (SKFDelegae.SKF_ConnectDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_ConnectDev));

			this.skf_createApplicationP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_CreateApplication");
			this.skf_createApplication = (SKFDelegae.SKF_CreateApplication)Marshal.GetDelegateForFunctionPointer(skf_createApplicationP, typeof(SKFDelegae.SKF_CreateApplication));

			this.skf_deleteApplicationP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_DeleteApplication");
			this.skf_deleteApplication = (SKFDelegae.SKF_DeleteApplication)Marshal.GetDelegateForFunctionPointer(skf_deleteApplicationP, typeof(SKFDelegae.SKF_DeleteApplication));

			this.skf_devAuthP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_DevAuth");
			this.skf_devAuth = (SKFDelegae.SKF_DevAuth)Marshal.GetDelegateForFunctionPointer(skf_devAuthP, typeof(SKFDelegae.SKF_DevAuth));

			this.skf_encryptP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_Encrypt");
			this.skf_encrypt = (SKFDelegae.SKF_Encrypt)Marshal.GetDelegateForFunctionPointer(skf_encryptP, typeof(SKFDelegae.SKF_Encrypt));

			this.skf_encryptInitP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EncryptInit");
			this.skf_encryptInit = (SKFDelegae.SKF_EncryptInit)Marshal.GetDelegateForFunctionPointer(skf_encryptInitP, typeof(SKFDelegae.SKF_EncryptInit));

			this.skf_enumApplicationP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumApplication");
			this.skf_enumApplication = (SKFDelegae.SKF_EnumApplication)Marshal.GetDelegateForFunctionPointer(skf_enumApplicationP, typeof(SKFDelegae.SKF_EnumApplication));

			this.skf_genRandomP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_GenRandom");
			this.skf_genRandom = (SKFDelegae.SKF_GenRandom)Marshal.GetDelegateForFunctionPointer(skf_genRandomP, typeof(SKFDelegae.SKF_GenRandom));

			this.skf_setSymmKeyP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_SetSymmKey");
			this.skf_setSymmKey = (SKFDelegae.SKF_SetSymmKey)Marshal.GetDelegateForFunctionPointer(skf_setSymmKeyP, typeof(SKFDelegae.SKF_SetSymmKey));

			this.skf_waitForDevEventP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_WaitForDevEvent");
			this.skf_waitForDevEvent = (SKFDelegae.SKF_WaitForDevEvent)Marshal.GetDelegateForFunctionPointer(skf_waitForDevEventP, typeof(SKFDelegae.SKF_WaitForDevEvent));
			return 0;
		}
	}
}
