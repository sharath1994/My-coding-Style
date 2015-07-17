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
	public sealed partial class SplashScreen1
	{
		public SplashScreen1()
		{
			InitializeComponent();
			
			//Added to support default instance behavour in C#
			if (defaultInstance == null)
				defaultInstance = this;
		}
		
#region Default Instance
		
		private static SplashScreen1 defaultInstance;
		
		/// <summary>
		/// Added by the VB.Net to C# Converter to support default instance behavour in C#
		/// </summary>
		public static SplashScreen1 Default
		{
			get
			{
				if (defaultInstance == null)
				{
					defaultInstance = new SplashScreen1();
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
		
		//TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
		//  of the Project Designer ("Properties" under the "Project" menu).
		
		
		public void SplashScreen1_Load(object sender, System.EventArgs e)
		{
			//Set up the dialog text at runtime according to the application's assembly information.
			
			//TODO: Customize the application's assembly information in the "Application" pane of the project
			//  properties dialog (under the "Project" menu).
			
			//Application title
			
			
			//Format the version information using the text set into the Version control at design time as the
			//  formatting string.  This allows for effective localization if desired.
			//  Build and revision information could be included by using the following code and changing the
			//  Version control's designtime text to "Version {0}.{1:00}.{2}.{3}" or something similar.  See
			//  String.Format() in Help for more information.
			//
			//    Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
			
			
		}
		
	}
	
}
