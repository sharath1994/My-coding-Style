using System.Collections.Generic;
using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;

namespace Serial_app
{
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]public partial class Form1 : System.Windows.Forms.Form
	{
		
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
		
		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.Load += new System.EventHandler(Form1_Load);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.TextBox2 = new System.Windows.Forms.TextBox();
			this.TextBox1 = new System.Windows.Forms.TextBox();
			this.TextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1_KeyPress);
			this.Button2 = new System.Windows.Forms.Button();
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button1 = new System.Windows.Forms.Button();
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button3 = new System.Windows.Forms.Button();
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.ComboBox1 = new System.Windows.Forms.ComboBox();
			this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
			this.ComboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox1_KeyPress);
			this.ComboBox2 = new System.Windows.Forms.ComboBox();
			this.ip = new System.Windows.Forms.Label();
			this.op = new System.Windows.Forms.Label();
			this.SerialPort1 = new System.IO.Ports.SerialPort(this.components);
			this.SerialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.SerialPort1_ErrorReceived);
			this.SerialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataReceived);
			this.SuspendLayout();
			//
			//TextBox2
			//
			this.TextBox2.Location = new System.Drawing.Point(12, 25);
			this.TextBox2.Multiline = true;
			this.TextBox2.Name = "TextBox2";
			this.TextBox2.Size = new System.Drawing.Size(355, 101);
			this.TextBox2.TabIndex = 0;
			//
			//TextBox1
			//
			this.TextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.TextBox1.Location = new System.Drawing.Point(12, 155);
			this.TextBox1.Multiline = true;
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new System.Drawing.Size(355, 114);
			this.TextBox1.TabIndex = 1;
			//
			//Button2
			//
			this.Button2.Location = new System.Drawing.Point(548, 82);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(75, 23);
			this.Button2.TabIndex = 2;
			this.Button2.Text = "Write";
			this.Button2.UseVisualStyleBackColor = true;
			//
			//Button1
			//
			this.Button1.Location = new System.Drawing.Point(548, 12);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(75, 23);
			this.Button1.TabIndex = 3;
			this.Button1.Text = "Init";
			this.Button1.UseVisualStyleBackColor = true;
			//
			//Button3
			//
			this.Button3.Location = new System.Drawing.Point(548, 139);
			this.Button3.Name = "Button3";
			this.Button3.Size = new System.Drawing.Size(75, 23);
			this.Button3.TabIndex = 4;
			this.Button3.Text = "Close";
			this.Button3.UseVisualStyleBackColor = true;
			//
			//ComboBox1
			//
			this.ComboBox1.FormattingEnabled = true;
			this.ComboBox1.Location = new System.Drawing.Point(391, 35);
			this.ComboBox1.Name = "ComboBox1";
			this.ComboBox1.Size = new System.Drawing.Size(121, 21);
			this.ComboBox1.TabIndex = 5;
			//
			//ComboBox2
			//
			this.ComboBox2.FormattingEnabled = true;
			this.ComboBox2.Items.AddRange(new object[] {"9600", "38400", "57600", "115200"});
			this.ComboBox2.Location = new System.Drawing.Point(391, 170);
			this.ComboBox2.Name = "ComboBox2";
			this.ComboBox2.Size = new System.Drawing.Size(121, 21);
			this.ComboBox2.TabIndex = 6;
			//
			//ip
			//
			this.ip.AutoSize = true;
			this.ip.Location = new System.Drawing.Point(12, 136);
			this.ip.Name = "ip";
			this.ip.Size = new System.Drawing.Size(31, 13);
			this.ip.TabIndex = 7;
			this.ip.Text = "Input";
			//
			//op
			//
			this.op.AutoSize = true;
			this.op.Location = new System.Drawing.Point(12, 9);
			this.op.Name = "op";
			this.op.Size = new System.Drawing.Size(39, 13);
			this.op.TabIndex = 8;
			this.op.Text = "Output";
			//
			//SerialPort1
			//
			//
			//Form1
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF((float) (6.0F), (float) (13.0F));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(635, 281);
			this.Controls.Add(this.op);
			this.Controls.Add(this.ip);
			this.Controls.Add(this.ComboBox2);
			this.Controls.Add(this.ComboBox1);
			this.Controls.Add(this.Button3);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.Button2);
			this.Controls.Add(this.TextBox1);
			this.Controls.Add(this.TextBox2);
			this.Icon = (System.Drawing.Icon) (resources.GetObject("$this.Icon"));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Serial Communication";
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		internal System.Windows.Forms.TextBox TextBox2;
		internal System.Windows.Forms.TextBox TextBox1;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.ComboBox ComboBox1;
		internal System.Windows.Forms.ComboBox ComboBox2;
		internal System.Windows.Forms.Label ip;
		internal System.Windows.Forms.Label op;
		internal System.IO.Ports.SerialPort SerialPort1;
		
	}
	
}
