using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKeyFormatUtil
{
	class LogUtil
	{
		private System.Windows.Forms.RichTextBox tbox_Log;
		private static LogUtil instance;
		public SetTextBoxValue LogFunc;
		private LogUtil(SetTextBoxValue setValueFunc)
		{
			this.LogFunc = setValueFunc;
		}
		public static LogUtil GetInstance(SetTextBoxValue setValueFunc) 
		{
			if (instance == null)
			{
				instance = new LogUtil(setValueFunc);
			}
			return instance;
		}
		public void Log(string info)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(DateTime.Now.ToString("yyyyMMdd HHmmss"));
			sb.Append("：");
			sb.AppendLine(info);
			LogFunc(sb.ToString());
			
			//tbox_Log.Text = tbox_Log.Text + sb.ToString();
		}

		
	}
}
