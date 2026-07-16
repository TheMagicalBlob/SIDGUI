using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace sidgui
{
    public partial class SIDGUI
    {
        //=================================\\
        //--|   Variable Declarations   |--\\
        //=================================\\
        #region [Variable Declarations]

        //#
        //## Form Functionality Globals
        //#
        #region [Form Functionality Globals]

        /// <summary> Boolean global for keeping track of the current mouse state. </summary>
        public static bool MouseIsDown = false;

        /// <summary> Variable for Smooth Form Dragging. </summary>
        public static Point MouseDif;

        /// <summary> MainPage Form Pointer/Reference. </summary>
        public static SIDGUI Venat;
        #endregion
        #endregion








        //======================================================\\
        //---|   Form Functionality Function Delcarations   |---\\
        //======================================================\\
        #region [Form Functionality Function Delcarations]

        /// <summary>
        /// Post-InitializeComponent Configuration. <br/><br/>
        /// Create Assign Anonymous Event Handlers to Parent and Children.
        /// </summary>
        public void InitializeAdditionalEventHandlers(Form Venat)
        {
            var controls = Venat.Controls.Cast<Control>().ToArray();


            // Setup variables used for decorations like the SeparatorLines and border
            InitializeFormDecorations<Form>(Venat, controls);


            // Set appropriate event handlers for the controls on the form as well
            foreach (var item in controls)
            {
                item.KeyDown += (sender, arg) => FormKeyboardInputHandler(((Control) sender).Name, arg.KeyData, arg.Control, arg.Shift);

                item.MouseDown += new MouseEventHandler((sender, e) =>
                {
                    MouseDif = new Point(MousePosition.X - Venat.Location.X, MousePosition.Y - Venat.Location.Y);
                    MouseIsDown = true;
                });
                item.MouseUp += new MouseEventHandler((sender, e) =>
                {
                    MouseIsDown = false;
                });



                // Avoid applying MouseMove and KeyDown event handlers to text containers (to retain the ability to drag-select text)
                if (item.GetType() == typeof(sidgui.TextBox) || item.GetType() == typeof(sidgui.RichTextBox))
                {
                    item.KeyDown += (sender, arg) =>
                    {
                        if (arg.KeyData == Keys.Escape)
                        {
                            Focus();
                        }
                    };
                }
                // Add the event handler to everything that's not a text container
                else {
                    item.MouseMove += new MouseEventHandler((sender, e) => MoveForm());
                }
            }





            MinimizeBtn.Click += new EventHandler((sender, e) => Venat.WindowState = FormWindowState.Minimized);
            MinimizeBtn.MouseEnter += new EventHandler((sender, e) => ((Control)sender).ForeColor = Color.FromArgb(90, 100, 255));
            MinimizeBtn.MouseLeave += new EventHandler((sender, e) => ((Control)sender).ForeColor = Color.FromArgb(0, 0, 0));
            ExitBtn.Click += new EventHandler((sender, e) => Environment.Exit(0));
            ExitBtn.MouseEnter += new EventHandler((sender, e) => ((Control)sender).ForeColor = Color.FromArgb(230, 100, 100));
            ExitBtn.MouseLeave += new EventHandler((sender, e) => ((Control)sender).ForeColor = Color.FromArgb(0, 0, 0));


            // Set Event Handlers for Form Dragging
            MouseDown += new MouseEventHandler((sender, e) =>
            {
                MouseDif = new Point(MousePosition.X - Location.X, MousePosition.Y - Location.Y);

                MouseIsDown = true;
            });

            MouseUp += new MouseEventHandler((sender, e) =>
            {
                MouseIsDown = false;
            });

            MouseMove += new MouseEventHandler((sender, e) => MoveForm());

            KeyDown += (sender, arg) => FormKeyboardInputHandler(((Control)sender).Name, arg.KeyData, arg.Control, arg.Shift);
        }



        /// <summary>
        /// Testing random input crap
        /// </summary>
        private void FormKeyboardInputHandler(string sender, Keys arg, bool ctrl, bool shift)
        {
            echo($"Input [{arg}] Received by Control [{sender}]");
        }




        /// <summary>
        /// Handle Form Dragging for Borderless Form.
        /// </summary>
        public static void MoveForm()
        {
            if (MouseIsDown && Venat != null)
            {
                Venat.Location = new Point(MousePosition.X - MouseDif.X, MousePosition.Y - MouseDif.Y);

                Venat.Update();
            }
        }






        /// <summary>
        /// //!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MouseDownFunc(object sender = null, EventArgs e = null)
        {
            if (Venat != null)
            {
                MouseDif = new Point(MousePosition.X - Venat.Location.X, MousePosition.Y - Venat.Location.Y);
                MouseIsDown = true;
            }
        }






        /// <summary>
        /// //!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MouseUpFunc(object sender = null, EventArgs e = null)
        {
            MouseIsDown = false;
        }
        #endregion












        //=======================================================\\
        //---|   Logging/Output Functionality Declarations   |---\\
        //=======================================================\\
        #region [Logging/Output Functionality Declarations]
#pragma warning disable IDE1006 // bug off, these ones are lowercase 

        /// <summary>
        /// Echo a provided string (or string representation of an object) to the standard console output, followed by a newline.
        /// <br/> Just prints a newline if no message is provided.
        /// </summary>
        /// <param name="message"> The object to print the string representation of. </param>
        public static void echo(object message = null)
        {
#if DEBUG
            string str;

            Console.WriteLine(str = message?.ToString() ?? string.Empty);
            Debug.WriteLineIf(!Console.IsOutputRedirected, str);
#endif
        }






        /// <summary>
        /// Echo a provided string (or string representation of an object) to the standard console output.
        /// <br/> Prints a single space char (0x20) if no message is provided.
        /// </summary>
        /// <param name="message"> The object to print the string representation of. </param>
        public static void _echo(object message = null)
        {
#if DEBUG
            string str;

            Console.Write(str = message?.ToString() ?? string.Empty);
            Debug.WriteIf(!Console.IsOutputRedirected, str);
#endif
        }
#pragma warning restore IDE1006
        #endregion
    }
}
