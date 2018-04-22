using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace UKeyFormatUtil
{
	
	delegate void SetTextBoxValue(string value);
	public partial class Form1 : Form
	{
		IDAEnrollLib.JITECCEnrollClass sm2Enroll;

		static uint SGD_SM4_ECB = 0x00000401;
		static uint SECURE_USER_ACCOUNT = 0x00000010;
		LogUtil log = null;

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll", EntryPoint = "GetWindowText")]
		public static extern int GetWindowText(
			IntPtr hWnd,
			StringBuilder lpString,
			int nMaxCount
		);
		

		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, StringBuilder text);


		[DllImport("SKFAPI_HT20485.dll", EntryPoint = "SKF_WaitForDevEvent", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_WaitForDevEvent(StringBuilder devName, ref UInt32 length,ref UInt32 eventType);

		[DllImport("HH_SKFAPI.dll", EntryPoint = "SKF_WaitForDevEvent", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_WaitForDevEvent_HH(StringBuilder devName, ref UInt32 length, ref UInt32 eventType);
		//[DllImport("user32.dll")]
		//public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_WaitForDevEvent", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_WaitForDevEvent_LM(StringBuilder devName, ref UInt32 length, ref UInt32 eventType);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_EnumDev", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_EnumDev(bool bPresent, StringBuilder szName, ref UInt32 length);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_ConnectDev", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_ConnectDev(StringBuilder szName, ref IntPtr devHandle);
		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_GenRandom", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_GenRandom(IntPtr devHandle, [Out]byte[] random,UInt32 length);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_SetSymmKey", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_SetSymmKey(IntPtr devHandle, byte[] pbKey, UInt32 algId, ref IntPtr hKeyHandle);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_EncryptInit", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_EncryptInit(IntPtr hKeyHandle, BLOCKCIPHERPARAM encryptParam);
		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_Encrypt", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_Encrypt(IntPtr hKeyHandle, byte[] pbData,UInt32 dataLen,[Out] byte[] encryptedData,ref UInt32 outLen);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_DevAuth", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_DevAuth(IntPtr devHandle, byte[] authData, UInt32 dataLen);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_EnumApplication", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_EnumApplication(IntPtr devHandle, StringBuilder szAppName, ref UInt32 dataLen);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_DeleteApplication", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_DeleteApplication(IntPtr devHandle, StringBuilder szAppName);

		[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_CreateApplication", SetLastError = true, CharSet = CharSet.Ansi)]
		public static extern int SKF_CreateApplication(IntPtr devHandle, StringBuilder szAppName, StringBuilder adminPin, uint adminPinRetryCount, StringBuilder userPin, uint userPinRetryCount, uint createFileRight, ref IntPtr hApplication);

		//SKF_CreateApplication(DEVHANDLE hDev, LPSTR szAppName, LPSTR szAdminPin, DWORD dwAdminPinRetryCount,LPSTR szUserPin, DWORD dwUserPinRetryCount,DWORD dwCreateFileRights, HAPPLICATION *phApplication)
		//SKF_DeleteApplication(DEVHANDLE hDev, LPSTR szAppName)
		//[DllImport("gm3000_skf_hubca.dll", EntryPoint = "SKF_EnumApplication", SetLastError = true, CharSet = CharSet.Ansi)]
		//public static extern int SKF_EnumApplication(IntPtr devHandle, byte[] szAppName, ref UInt32 dataLen);
		//SKF_EnumApplication(DEVHANDLE hDev, LPSTR szAppName,ULONG*pulSize)
		public struct BLOCKCIPHERPARAM
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public byte[] IV;
			public int IVLen;
			public int PaddingType;
			public int FeedBitLength;
		}
		delegate void GetForm();
		delegate void DoListenFunc();
		public Form1()
		{
			InitializeComponent();
			u_init();
		}
		private void u_init()
		{
			sm2Enroll = new IDAEnrollLib.JITECCEnrollClass();
			
			log = LogUtil.GetInstance(new SetTextBoxValue(SetValue));
		}
        

		public void SetValue(string value)
		{
			if (tbox_result.InvokeRequired)
			{
				SetTextBoxValue setValueFunc = new SetTextBoxValue(SetValue);
				this.Invoke(setValueFunc, new object[] { value });
			}
			else
			{
                //MessageBox.Show(tbox_result.Lines.Length.ToString());
                if (tbox_result.Lines.Length > 50)
                {
                    string[] lines = tbox_result.Lines;
                    Array.Resize(ref lines, 50);
                    tbox_result.Lines = lines;
                }
				tbox_result.Text = tbox_result.Text.Insert(0, value.ToString());
                tbox_result.SelectAll();
                tbox_result.SelectionColor = Color.Black;
                HighLightStr(tbox_result, "格式化成功", Color.Green, -1);
                HighLightStr(tbox_result, "创建应用成功", Color.Green, -1);
                HighLightStr(tbox_result, "创建应用出错", Color.Red, -1);
                HighLightStr(tbox_result, "格式化出错", Color.Red, -1);
                HighLightStr(tbox_result, "加载IDAEnroll出错", Color.Red, -1);
			}
		}
        private void HighLightStr(RichTextBox richTextBox1, string str, Color c, int iPreBegin)
        {
            int iBegin = -1;
            if (iPreBegin == -1)
            {
                //only use the first row to search
                iBegin = richTextBox1.Find(str, 0, 50, RichTextBoxFinds.MatchCase);
                //iBegin = richTextBox1.Find(str);
                if (iBegin >= 0)
                {
                    richTextBox1.Select(iBegin, str.Length);
                    richTextBox1.SelectionColor = c;
                    //HighLightStr(richTextBox1 ,str, c, iBegin + str.Length);
                }
            }
            else
            {
                iBegin = richTextBox1.Find(str, iPreBegin, RichTextBoxFinds.MatchCase);
                if (iBegin == -1)
                {
                    return;
                }
                richTextBox1.Select(iBegin, str.Length);
                richTextBox1.SelectionColor = c;
                //HighLightStr(richTextBox1,str, c, iBegin + str.Length);
            }

        }
		private UKeyInfo GetUkeyInfoHT()
		{
			UKeyInfo ukeyInfoHT = new UKeyInfo();
			ukeyInfoHT.AdminPin = "88888888";
			ukeyInfoHT.AdminPinCount = 10;
			ukeyInfoHT.AppName = "HBCAAPPLICATION_RSA";
			ukeyInfoHT.CertType = "SM2";
			ukeyInfoHT.CreateFlag = 1;
			ukeyInfoHT.UKeyName = "海泰";
			ukeyInfoHT.UKeyType = "海泰Key 1.0";
			ukeyInfoHT.UserPin = "11111111";
			ukeyInfoHT.UserPinCount = 10;
			return ukeyInfoHT;
            
		}
		private UKeyInfo GetUkeyInfoHH()
		{
			UKeyInfo ukeyInfoHT = new UKeyInfo();
			ukeyInfoHT.AdminPin = "88888888";
			ukeyInfoHT.AdminPinCount = 10;
			ukeyInfoHT.AppName = "HBCAAPPLICATION_RSA";
			ukeyInfoHT.CertType = "SM2";
			ukeyInfoHT.CreateFlag = 1;
			ukeyInfoHT.UKeyName = "华虹";
			ukeyInfoHT.UKeyType = "华虹GM3000";
			ukeyInfoHT.UserPin = "11111111";
			ukeyInfoHT.UserPinCount = 10;
			return ukeyInfoHT;
		}
		private UKeyInfo GetUkeyInfoLM()
		{
			UKeyInfo ukeyInfoHT = new UKeyInfo();
			ukeyInfoHT.AdminPin = "88888888";
			ukeyInfoHT.AdminPinCount = 10;
			ukeyInfoHT.AppName = "HBCAAPPLICATION_RSA";
			ukeyInfoHT.CertType = "SM2";
			ukeyInfoHT.CreateFlag = 1;
			ukeyInfoHT.UKeyName = "龙脉";
			ukeyInfoHT.UKeyType = "龙脉GM3000";
			ukeyInfoHT.UserPin = "11111111";
			ukeyInfoHT.UserPinCount = 10;
			return ukeyInfoHT;
		}

		#region TEST
		private void readUkeyType()
		{
			ushort i = 0;
			string ukeyType = "";
			ukeyType = sm2Enroll.EnumUSBKeyName("SM2", i++);
			while (ukeyType != null && !"".Equals(ukeyType))
			{
				tbox_result.Text = tbox_result.Text + "\r\n" + ukeyType;
				ukeyType = sm2Enroll.EnumUSBKeyName("SM2", i);
				i = (ushort)(i + 1);
			}
		}
		#endregion
		#region Format UKEY
		public void GetForm1()
		{
			System.Threading.Thread.Sleep(300);
			StringBuilder sb = new StringBuilder();
			IntPtr form = FindWindow(null, "输入PIN码");
			//GetWindowText(form, sb, 1024);
			//MessageBox.Show(sb.ToString());
			IntPtr control = FindWindowEx(form, IntPtr.Zero, null, "");
			//GetWindowText(control, sb, 1024);
			//MessageBox.Show(sb.ToString());
			SendMessage(control, 0x000C, 0, new StringBuilder("1234567812345678"));

			IntPtr control_ok = FindWindowEx(form, IntPtr.Zero, null, "确定");
			SendMessage(control_ok, 0xF5, 0, 0);
		}
		private void DoListening()
		{
			StringBuilder sb = new StringBuilder("", 50);
            UInt32 length = 50;
			UInt32 eventType = 1;
			int iRet = -1;
			iRet = SKF_WaitForDevEvent(sb, ref length, ref eventType);
			while (true)
			{
				if (eventType == 1)
				{
					UKeyInfo ukeyInfoHT = GetUkeyInfoHT();
					DoFormat(ukeyInfoHT);
				}
				Application.DoEvents();
				iRet = SKF_WaitForDevEvent(sb, ref length, ref eventType);
			}
		}
		private void DoFormat(UKeyInfo ukeyInfoHT)
		{
			try
			{
				uint iRet = sm2Enroll.SetUSBKeyType(ukeyInfoHT.AppName, UKeyInfo.SM2, ukeyInfoHT.UKeyType);
				if (iRet == 0)
				{
					GetForm g = new GetForm(GetForm1);
					System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(g));
					t.Start();

					iRet = sm2Enroll.FormatUSBKey();
					//System.Threading.Thread.Sleep(2000);
					if (iRet == 0)
					{
						log.Log("格式化成功!");
						System.Threading.Thread t1 = new System.Threading.Thread(new System.Threading.ThreadStart(g));
						t1.Start();

						bool bSucc = CreateUSBApp(ukeyInfoHT);
						if (bSucc)
						{
							log.Log("创建应用成功!");
						}
						else
						{
							log.Log("创建应用出错，错误代码：" + sm2Enroll.GetErrorCode().ToString() + "，错误信息：" + sm2Enroll.GetErrorMsg() + "，请用UKEY对应的格式化工具格式化。");
						}
					}
					else
					{
						log.Log("格式化出错，错误代码：" + sm2Enroll.GetErrorCode().ToString() + "，错误信息：" + sm2Enroll.GetErrorMsg()+"，请用UKEY对应的格式化工具格式化。");
					}

				}
				else
				{
					log.Log("加载IDAEnroll出错，错误代码：" + sm2Enroll.GetErrorCode().ToString() + "，错误信息：" + sm2Enroll.GetErrorMsg());
				}

			}
			catch (Exception ex)
			{
				throw;
			}
		}
		private bool CreateUSBApp(UKeyInfo ukeyInfo)
		{
			uint iRet = 1;
			
			iRet = sm2Enroll.CreateUSBKEYApplication(1, ukeyInfo.AppName, ukeyInfo.AdminPin, ukeyInfo.AdminPinCount, ukeyInfo.UserPin, ukeyInfo.UserPinCount);
			if (iRet == 0)
			{
				return true;
			}
			return false;
		}
		#endregion
		private void btn_test_Click(object sender, EventArgs e)
		{
			DoListenFunc doListen = new DoListenFunc(DoListening);
			System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(doListen));
			t.IsBackground = true;
			log.Log("格式化开始!");
			t.Start();
		}


		private void button1_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder("",1024);
			UInt32 length = 1024;
			UInt32 eventType = 1;
			int iRet = -1;
			iRet = SKF_WaitForDevEvent_HH(sb,ref length, ref eventType);
			MessageBox.Show(sb.ToString());
			MessageBox.Show(eventType.ToString());
			
		}

		

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			//System.Environment.Exit(0);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DoListenFunc doListen = new DoListenFunc(DoListening_HH);
			System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(doListen));
			t.IsBackground = true;
			log.Log("格式化开始!");
			t.Start();
		}
		private void DoListening_HH()
		{
			StringBuilder sb = new StringBuilder("", 50);
			UInt32 length = 1024;
			UInt32 eventType = 1;
			int iRet = -1;
			iRet = SKF_WaitForDevEvent_HH(sb, ref length, ref eventType);
			while (true)
			{
				if (eventType == 1)
				{
					UKeyInfo uKeyInfo_hh = GetUkeyInfoHH();
					DoFormat(uKeyInfo_hh);
				}
				else if (eventType == 2)
				{
					UKeyInfo uKeyInfo_hh = GetUkeyInfoHH();
					DoFormat(uKeyInfo_hh);
				}
				Application.DoEvents();
				iRet = SKF_WaitForDevEvent_HH(sb, ref length, ref eventType);
			}
		}

		private void DoListening_LM()
		{
			StringBuilder sb = new StringBuilder("", 50);
			UInt32 length = 1024;
			UInt32 eventType = 1;
			int iRet = -1;
			iRet = SKF_WaitForDevEvent_LM(sb, ref length, ref eventType);
			while (true)
			{
				if (eventType == 1)
				{
					//UKeyInfo uKeyInfo_hh = GetUkeyInfoLM();
					//DoFormat(uKeyInfo_hh);
					uint length1 = 0;
					SKF_EnumDev(true, null, ref length1);
					log.Log("格式化成功!" + length1.ToString());
				}
				Application.DoEvents();
				iRet = SKF_WaitForDevEvent_LM(sb, ref length, ref eventType);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				UKeyInfo uKeyInfo_hh = GetUkeyInfoHH();
				DoFormat(uKeyInfo_hh);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				//DoListenFunc doListen = new DoListenFunc(DoListening_LM);
				//System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(doListen));
				//t.IsBackground = true;
				//log.Log("格式化开始!");
				//t.Start();
				int iRet = -1;
				uint length1 = 0;
				IntPtr hModule = DynamicLibUtil.LoadLibrary("gm3000_skf_hubca.dll");
				IntPtr skf = DynamicLibUtil.GetProcAddress(hModule, "SKF_EnumDev");
				SKFDelegae.SKF_EnumDev skfCon = (SKFDelegae.SKF_EnumDev)Marshal.GetDelegateForFunctionPointer(skf, typeof(SKFDelegae.SKF_EnumDev));
				iRet = skfCon(true, null, ref length1);
				//iRet = SKF_EnumDev(true, null, ref length1);
				if (iRet != 0) 
				{
					MessageBox.Show("枚举设备失败");
					return;
				}
				StringBuilder sb = new StringBuilder();
				SKF_EnumDev(true, sb, ref length1);
				log.Log("格式化成功!" + sb.ToString());
				IntPtr devHandle = IntPtr.Zero;
				iRet = -1;
				iRet = SKF_ConnectDev(sb, ref devHandle);
				if (iRet != 0)
				{
					MessageBox.Show("连接设备失败"+iRet.ToString());
					return;
				}
				byte[] randomData = new byte[32];
				iRet = -1;
				iRet = SKF_GenRandom(devHandle, randomData, 8);
				if (iRet != 0)
				{
					MessageBox.Show("产生随机数失败" + iRet.ToString());
					return;
				}
				iRet = -1;
				byte[] data = System.Text.Encoding.ASCII.GetBytes("1234567812345678");
				IntPtr hKeyHandle = IntPtr.Zero;
				iRet = SKF_SetSymmKey(devHandle, data, SGD_SM4_ECB,ref hKeyHandle);
				if (iRet != 0)
				{
					MessageBox.Show("获取加密句柄失败" + iRet.ToString());
					return;
				}
				BLOCKCIPHERPARAM encParam = new BLOCKCIPHERPARAM();
				encParam.IV = new byte[32];
				iRet = -1;
				iRet = SKF_EncryptInit(hKeyHandle, encParam);
				if (iRet != 0)
				{
					MessageBox.Show("设置加密参数失败" + iRet.ToString());
					return;
				}
				UInt32 encDataLen = 256;
				byte[] encryptResult = new byte[encDataLen];
				iRet = -1;
				iRet = SKF_Encrypt(hKeyHandle, randomData, (UInt32)16, encryptResult, ref encDataLen);
				if (iRet != 0)
				{
					MessageBox.Show("加密失败1," + iRet.ToString());
					return;
				}
				
				
				//iRet = -1;
				//iRet = SKF_Encrypt(hKeyHandle, randomData, (UInt32)randomData.Length, encryptResult, ref encDataLen);
				//if (iRet != 0)
				//{
				//	MessageBox.Show("加密失败2," + iRet.ToString());
				//	return;
				//}
				iRet = -1;
				iRet = SKF_DevAuth(devHandle, encryptResult, encDataLen);
				if (iRet != 0)
				{
					MessageBox.Show("设备认证失败," + iRet.ToString());
					return;
				}
				StringBuilder appName = new StringBuilder(256);
				UInt32 dataLen = 0;
				iRet = -1;
				iRet = SKF_EnumApplication(devHandle, null, ref dataLen);
				if (iRet != 0)
				{
					MessageBox.Show("枚举应用失败1," + iRet.ToString());
					return;
				}
				iRet = -1;
				iRet = SKF_EnumApplication(devHandle, appName, ref dataLen);
				if (iRet != 0)
				{
					MessageBox.Show("枚举应用失败2," + iRet.ToString());
					return;
				}
				if (appName.ToString().Trim().Length != 0)
				{
					iRet = -1;
					iRet = SKF_DeleteApplication(devHandle, appName);
					if (iRet != 0)
					{
						MessageBox.Show("删除应用失败," + iRet.ToString());
						return;
					}
				}
				
				iRet = -1;
				appName = new StringBuilder("HBCAAPPLICATION_RSA");
				StringBuilder adminPin =new StringBuilder("88888888");
				StringBuilder userPin = new StringBuilder("11111111");
				uint adminPinRetryCount = 10;
				uint userPinRetryCount = 10;
				IntPtr hApplication = IntPtr.Zero;
				iRet = SKF_CreateApplication(devHandle, appName, adminPin, adminPinRetryCount, userPin, userPinRetryCount, SECURE_USER_ACCOUNT, ref hApplication);
				if (iRet != 0)
				{
					MessageBox.Show("创建应用失败," + iRet.ToString());
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

	}
}
