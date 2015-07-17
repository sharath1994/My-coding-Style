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
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;
using System.Text;
using System.IO;


namespace Serial_app
{
	public partial class Form1
	{
		public Form1()
		{
			InitializeComponent();
			
			//Added to support default instance behavour in C#
			if (defaultInstance == null)
				defaultInstance = this;
		}
		
#region Default Instance
		
		private static Form1 defaultInstance;
		
		/// <summary>
		/// Added by the VB.Net to C# Converter to support default instance behavour in C#
		/// </summary>
		public static Form1 Default
		{
			get
			{
				if (defaultInstance == null)
				{
					defaultInstance = new Form1();
					defaultInstance.FormClosed += new FormClosedEventHandler(defaultInstance_FormClosed);
				}
				
				return defaultInstance;
			}
		}
		
		static void defaultInstance_FormClosed(object sender, FormClosedEventArgs e)
		{
			defaultInstance = null;
		}
		
#endregion
		
		Array myPort;
		
		public bool comOpen;
		
		private string readBuffer = string.Empty;
		
		bool stopMe;
		
		delegate void SetTextCallback(string text); //Calling safe Thread
		
		public void Form1_Load(object sender, EventArgs e)
		{
			myPort = System.IO.Ports.SerialPort.GetPortNames(); //Geting list of all opened ports
			ComboBox1.Items.AddRange((object[]) myPort);
			Button1.Enabled = false;
			Button2.Enabled = false;
			Button3.Enabled = false;
			
			
		}
		
		public void Button1_Click(object sender, EventArgs e)
		{
			SerialPort1.PortName = ComboBox1.Text;
			SerialPort1.BaudRate = int.Parse(ComboBox2.Text);
			Button2.Enabled = true;
			Button3.Enabled = true;
			TextBox1.Select();
			try
			{
				
				SerialPort1.Open(); //Connecting to Serialport
				SerialPort1.ReadTimeout = 10000;
				comOpen = SerialPort1.IsOpen;
				//Dim speech
				//speech = CreateObject("sapi.spvoice")
				//speech.speak("Connected to serialport Sucessful")
				
			}
			catch (Exception ex)
			{
				comOpen = false;
				MessageBox.Show("Error Open: " + ex.Message); //Exception if Port is not open
			}
		}
		
		public void Button2_Click(object sender, EventArgs e)
		{
			if (TextBox1.Text == null)
			{
				//Dim speech
				//speech = CreateObject("sapi.spvoice")
				//speech.speak("No text has been entered")
			}
			SerialPort1.Write(TextBox2.Text); //concatenate with \n
		}
		
		public void Button3_Click(object sender, EventArgs e)
		{
			Button1.Enabled = true;
			Button2.Enabled = false;
			if (MessageBox.Show("Do you really want to close the Serialport", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
			{
				try
				{
					SerialPort1.DiscardInBuffer();
					SerialPort1.Close();
				}
				catch (IOException)
				{
					//Dim speech
					//speech = CreateObject("sapi.spvoice")
					//speech.speak("Serialport Closed")
				}
			}
			else
			{
				Thread t = new Thread(new System.Threading.ThreadStart(ClosePort));
				t.Start();
			}
		}
		private void ClosePort()
		{
			if (SerialPort1.IsOpen)
			{
				SerialPort1.Close();
			}
		}
		
		public void SerialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			try
			{
				SerialPort1.Close();
				
			}
			catch (IOException)
			{
				
			}
		}
		
		public void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			Button1.Enabled = true;
			ComboBox2.Text = "9600";
		}
		
		
		public void SerialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			try
			{
				
				int i = SerialPort1.BytesToRead; //Declearing Number of bytes to read
				
				if (i >= 9)
				{
					byte[] data = new byte[i+ 1];
					
					//For Each item As Byte In data
					SerialPort1.Read(data, 0, i); //Reading data from Serialport
					//If comOpen = True Then
					ReceivedText(System.Convert.ToString((data[0]))); //Sending the recieved data to another thread for displaying
					ReceivedText(System.Convert.ToString((data[1])));
					ReceivedText(System.Convert.ToString((data[2])));
					ReceivedText(System.Convert.ToString((data[3])));
					ReceivedText(System.Convert.ToString((data[4])));
					ReceivedText(System.Convert.ToString((data[5])));
					ReceivedText(System.Convert.ToString((data[6])));
					ReceivedText(System.Convert.ToString((data[7])));
					ReceivedText(System.Convert.ToString((data[8])));
					ReceivedText(System.Convert.ToString((data[9])));
					
				}
				//Next
				//End If
			}
			catch (IOException)
			{
			}
			catch (IndexOutOfRangeException)
			{
			}
		}
		
		public void ReceivedText(string text) //displaying the data in text box using safe thread operation
		{
			if (this.TextBox1.InvokeRequired)
			{
				SetTextCallback x = new SetTextCallback(ReceivedText);
				this.Invoke(x, new object[] {(text)});
			}
			else
			{
				this.TextBox1.Text += int.Parse(text) - 48 + "\r\n" ; //append text
				
			}
			
		}
		
		public void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (Strings.Asc(e.KeyChar) == 13)
			{
				Button1.PerformClick();
			}
		}
		
		public void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (Strings.Asc(e.KeyChar) == 13)
			{
				SerialPort1.Write(TextBox2.Text);
			}
		}
	}
	
}
