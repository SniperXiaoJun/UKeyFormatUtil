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

		SKFDelegae.SKF_EnumDev skf_enumDev;
		SKFDelegae.SKF_ConnectDev skf_connectDev;
		SKFDelegae.SKF_CreateApplication skf_createApplication;
		SKFDelegae.SKF_DeleteApplication skf_deleteApplication;
		SKFDelegae.SKF_DevAuth skf_devAuth;
		SKFDelegae.SKF_Encrypt skf_encrypt;
		SKFDelegae.SKF_EncryptInit skf_encryptInit;
		SKFDelegae.SKF_EnumApplication skf_enumApplication;
		SKFDelegae.SKF_GenRandom skf_genRandom;
		SKFDelegae.SKF_SetSymmKey skf_setSymmKey;
		SKFDelegae.SKF_WaitForDevEvent skf_waitForDevEvent;
		public SKFObject(string dllName) 
		{
			this.dllName = dllName;
			this.hModule = IntPtr.Zero;
			
			
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

			this.skf_connectDevP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			this.skf_createApplication = (SKFDelegae.SKF_CreateApplication)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_createApplicationP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_deleteApplicationP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_devAuthP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_encryptP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_encryptInitP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_enumApplicationP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_genRandomP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_setSymmKeyP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));

			this.skf_waitForDevEventP = DynamicLibUtil.GetProcAddress(this.hModule, "SKF_EnumDev");
			SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf_connectDevP, typeof(SKFDelegae.SKF_EnumDev));
			return 0;
		}
	}
}
