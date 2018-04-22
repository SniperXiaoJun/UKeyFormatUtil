namespace UKeyFormatUtil
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tbox_result = new System.Windows.Forms.RichTextBox();
			this.btn_test = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tbox_result);
			this.groupBox1.Location = new System.Drawing.Point(28, 53);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(679, 384);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "状态";
			// 
			// tbox_result
			// 
			this.tbox_result.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbox_result.Location = new System.Drawing.Point(3, 17);
			this.tbox_result.Name = "tbox_result";
			this.tbox_result.Size = new System.Drawing.Size(673, 364);
			this.tbox_result.TabIndex = 0;
			this.tbox_result.Text = "";
			// 
			// btn_test
			// 
			this.btn_test.Location = new System.Drawing.Point(28, 24);
			this.btn_test.Name = "btn_test";
			this.btn_test.Size = new System.Drawing.Size(134, 23);
			this.btn_test.TabIndex = 1;
			this.btn_test.Text = "海泰-格式化(批量)";
			this.btn_test.UseVisualStyleBackColor = true;
			this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(632, 24);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(168, 24);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(138, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "华虹-格式化(批量)";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Visible = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(312, 24);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(113, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "华虹-单个格式化";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(441, 24);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(125, 23);
			this.button4.TabIndex = 5;
			this.button4.Text = "龙脉-格式化(批量)";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(781, 463);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btn_test);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "湖北CA格式化工具-SM2";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RichTextBox tbox_result;
		private System.Windows.Forms.Button btn_test;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	}
}

