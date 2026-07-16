using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;



/*
 * Consists of Overrides/Extensions for existing classes. New classes go in the main SIDGUI.cs code file
 */
namespace sidgui
{
	//=====================================\\
	//---|   Custom Class Extensions   |---\\
	//=====================================\\
	#region [Custom Class Extensions]


	/// <summary>
	/// Custom RichTextBox class because bite me.
	/// </summary>
	public class RichTextBox : System.Windows.Forms.RichTextBox
	{
		/// <summary>
		/// Appends Text to The Current Text of A Text Box, Followed By The Standard Line Terminator.
		/// <br/>
		/// </summary>
		/// <param name="str"> The String to Output. </param>
		public void AppendLine(string str = "")
		{
            AppendText(str + '\n');
			Update();

			if (Scroll)
			{
				ScrollToCaret();
			}
		}




		/// <summary>
		/// Update a specific <paramref name="Line"/> in the LogWindow's output with the provided <paramref name="Message"/>
		/// </summary>
		/// <param name="Message"></param>
		/// <param name="Line"></param>
		/// <param name="Scroll"></param>
		public void UpdateLine(string Message, int Line)
		{
			while (Line >= Lines.Length)
			{
				AppendText("\n");
			}

			var lines = Lines;
			lines[Line] = Message ?? " ";

			Lines = lines;
		}


		new public void AppendText(string Message)
		{
            base.AppendText(Message ?? string.Empty);

			Update();

			if (Scroll)
			{
				ScrollToCaret();
			}
		}

		public bool Scroll;
	}



	/// <summary>
	/// Custom TextBox Class to Better Handle Default TextBox Contents.
	/// </summary>
	public class TextBox : System.Windows.Forms.TextBox
	{
		/// <summary> Create a new winforms TextBox control. </summary>
		public TextBox()
		{
			BackColor = Color.FromArgb(42, 42, 42);
			Font = SIDGUI.TextFont;
			ForeColor = SystemColors.Window;
			TabIndex = 3;
			TabStop = false;
		}


		public override string Text
		{
			get => base.Text;

			set => base.Text = value?.Replace("\"", string.Empty);
		}
	}








	//! Disabled because it turns out this makes single clicks send two events. Can't actually have a Button.Click event on single click, and Button.DoubleClick on a double click. Two click events will always be sent on a single click. This platform is just terrible sometimes
/*
	/// <summary>
	/// Custom Button class extension to auto-enable double-clicking for all Buttons
	/// </summary>
	public class Button : System.Windows.Forms.Button
	{
		public Button()
		{
			// Enable double-click functionality
			SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
		}
	}
*/





	public class Label : System.Windows.Forms.Label
	{
		public bool IsSeparatorLine { get; set; } = false;


		public bool StretchToFitForm
		{
			get => _stretchToFitForm && IsSeparatorLine;
			set => _stretchToFitForm = value;
		}
		private bool _stretchToFitForm = false;
	}







    /// <summary>
    /// Small Form class extention for horizontal & vertical SeparatorLines
    /// </summary>
    public class Form : System.Windows.Forms.Form
    {
        /// <summary> An array of Point() arrays with the start and end points of a line to draw. </summary>
        public Point[][] HSeparatorLines;

        /// <summary> An array of Point() arrays with the start and end points of a line to draw. </summary>
        public Point[][] VSeparatorLines;
    }


    /// <summary>
    /// Small Panel class extention for horizontal & vertical SeparatorLines
    /// </summary>
    public class Panel : System.Windows.Forms.Panel
    {
        /// <summary> An array of Point() arrays with the start and end points of a line to draw. </summary>
        public Point[][] HSeparatorLines;

        /// <summary> An array of Point() arrays with the start and end points of a line to draw. </summary>
        public Point[][] VSeparatorLines;
    }


    /// <summary>
    /// Small GroupBox class extention for horizontal & vertical SeparatorLines (as well as some auto-assigned clear event. I assume I added that for a reason and won't touch it for now)
    /// </summary>
    public class GroupBox : System.Windows.Forms.GroupBox
    {
        public GroupBox() : base()
        {
            Paint += RemoveGroupBoxBorderAndText;
		}

        private void RemoveGroupBoxBorderAndText(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.Clear(((GroupBox)sender).BackColor);
		}


		/// <summary>
		/// The expected vertical offset (in pixels) from the top of the GroupBox, to the bottom of the title border
		/// </summary>
		public static readonly int GroupBoxContentsOffset = 6;


        /// <summary> An array of Point() arrays with the start and end points of a line to draw. </summary>
        public Point[][] HSeparatorLines;

        /// <summary> An array of Point() arrays with the start and end points of a line to draw. </summary>
        public Point[][] VSeparatorLines;
    }

    #endregion
}
