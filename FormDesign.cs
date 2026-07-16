using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidgui
{
    public partial class SIDGUI
    {
        //==========================================================\\
        //--|   Global Look/Feel-Related Variable Declarations   |--\\
        //==========================================================\\
        #region [Global Look/Feel-Related Variable Declarations]

        public static readonly Color BC_Container = Color.FromArgb(7, 2, 10);
        public static readonly Color BC_TextBox = Color.FromArgb(11, 10, 14);
        public static readonly Color BC_Special = Color.FromArgb(125, 183, 245);
        
        public static readonly Color FC_Accents = Color.FromArgb(210, 240, 250); // Why did I choose this colour specifically? I forget.
        public static readonly Pen FormDecorationPen = new Pen(FC_Accents); // Colouring for Border Drawing
        
        public static readonly Font MainFont = new Font("Gadugi", 8.25f, FontStyle.Bold); // For the vast majority of controls; anything the user doesn't edit, really.
        public static readonly Font TextFont = new Font("Segoe UI Semibold", 7.5f); // For option controls with customized contents
        public static readonly Font DefaultTextFont = new Font("Segoe UI Semibold", 9f, FontStyle.Italic); // For option controls in default states


        /// <summary>
        /// The horizontal offset of any floating subforms spawned within the main form.
        /// </summary>
        public static int SubformVerticalOffset = 50;

#if DEBUG
        /// <summary>
        /// Disable drawing of form border/separator lines.
        /// </summary>
        public static bool noDraw;
#endif


        /// <summary>
        /// An enum for determining which colour & font settings to check for (or force) for a control
        /// </summary>
        public enum ControlDesignRoles : int
        {
            /// <summary>
            /// Controls to be ignored during style checks and assignments
            /// </summary>
            None = -1,

            /// <summary>
            /// Any background container object, such as Forms, GroupBoxes, and Panels
            /// </summary>
            Container,

            /// <summary>
            /// Larger labels meant for marking page sections or form titles
            /// </summary>
            TitleLabel,

            /// <summary>
            /// Smaller status or info labels
            /// </summary>
            InfoLabel,

            /// <summary>
            /// Text boxes for the user to input data
            /// </summary>
            InputTextBox,

            /// <summary>
            /// Text boxes used for displaying detailed info to the user
            /// </summary>
            OutputTextBox,

            /// <summary>
            /// Essentially any button that's neither set to None, nor SmallButton
            /// </summary>
            LargeButton,

            /// <summary>
            /// Smaller buttons such as the minimize/exit buttons, or a drop down arrow
            /// </summary>
            SmallButton
        }
        #endregion










        //===============================================\\
        //--|   UI Decoration Function Declarations   |--\\
        //===============================================\\
        #region [UI Decoration Function Declarations]

        /// <summary>
        /// Draw a thin border over the for edges on repaint.
        /// <br/>Draw a thin line from one end of the painted control to the other.
        ///</summary>
        public static void DrawContainerDecorations<T>(object Venat, PaintEventArgs @event)
        {
            if (Venat == null || @event == null)
            {
                echo($"WARNING: One or more provided arguments were null. ({nameof(Venat)} == null: {Venat == null} / {nameof(@event)} == null: {@event == null})");
                return;
            }

#if DEBUG
            if (noDraw)
            {
                return;
            }
#endif
            if (typeof(T) == typeof(Form))
            {
                var form = (Form) Venat;

                @event.Graphics?.Clear(form.BackColor); // Clear line bounds with the current form's background colour

                //## Draw Vertical Lines
                foreach (var line in form.VSeparatorLines ?? Array.Empty<Point[]>())
                {
                    @event?.Graphics?.DrawLine(FormDecorationPen, line[0], line[1]);
                }

                //## Draw Horizontal Lines
                foreach (var line in form.HSeparatorLines ?? Array.Empty<Point[]>())
                {
                    @event?.Graphics?.DrawLine(FormDecorationPen, line[0], line[1]);
                }

                // Draw a thin (1 pixel) border around the form with the current Pen
                @event?.Graphics?.DrawLines(FormDecorationPen, new[]
                {
                    Point.Empty,
                    new Point(form.Width-1, 0),
                    new Point(form.Width-1, form.Height-1),
                    new Point(0, form.Height-1),
                    Point.Empty
                });
            }
            else if (typeof(T) == typeof(GroupBox))
            {

                var groupBox = (GroupBox) Venat;

                @event.Graphics?.Clear(groupBox.BackColor); // Clear line bounds with the current form's background colour

                //## Draw Vertical Lines
                foreach (var line in groupBox.VSeparatorLines ?? Array.Empty<Point[]>())
                {
                    @event?.Graphics?.DrawLine(FormDecorationPen, line[0], line[1]);
                }

                //## Draw Horizontal Lines
                foreach (var line in groupBox.HSeparatorLines ?? Array.Empty<Point[]>())
                {
                    @event?.Graphics?.DrawLine(FormDecorationPen, line[0], line[1]);
                }

                // Draw a thin (1 pixel) border around the form with the current Pen
                @event?.Graphics?.DrawLines(FormDecorationPen, new[]
                {
                Point.Empty,
                new Point(groupBox.Width-1, 0),
                new Point(groupBox.Width-1, groupBox.Height-1),
                new Point(0, groupBox.Height-1),
                Point.Empty
            });
            }
            else if (typeof(T) == typeof(Panel))
            {
                var panel = (Panel) Venat;

                @event.Graphics?.Clear(panel.BackColor); // Clear line bounds with the current form's background colour

                //## Draw Vertical Lines
                foreach (var line in panel.VSeparatorLines ?? Array.Empty<Point[]>())
                {
                    @event?.Graphics?.DrawLine(FormDecorationPen, line[0], line[1]);
                }

                //## Draw Horizontal Lines
                foreach (var line in panel.HSeparatorLines ?? Array.Empty<Point[]>())
                {
                    @event?.Graphics?.DrawLine(FormDecorationPen, line[0], line[1]);
                }

                // Draw a thin (1 pixel) border around the form with the current Pen
                @event?.Graphics?.DrawLines(FormDecorationPen, new[]
                {
                Point.Empty,
                new Point(panel.Width-1, 0),
                new Point(panel.Width-1, panel.Height-1),
                new Point(0, panel.Height-1),
                Point.Empty
            });
            }
            else {
                throw new Exception($"ERROR: ({nameof(DrawContainerDecorations)}) Invalid type argument provided. ({typeof(T).Name} is not a Form, GroupBox, or Panel)");
            }

        }






        /// <summary>
        /// //!
        /// </summary>
        /// <param name="Venat"></param>
        /// <param name="Controls"></param>
        private void InitializeFormDecorations<T>(object Venat, Control[] Controls = null)
        {
            if (Controls == null)
            {
                switch (typeof(T))
                {
                    case var x when x == typeof(Form):
                        Controls = ((Form) Venat).Controls.Cast<Control>().ToArray();
                        break;

                    case var x when x == typeof(GroupBox):
                        Controls = ((GroupBox) Venat).Controls.Cast<Control>().ToArray();
                        break;

                    case var x when x == typeof(Panel):
                        Controls = ((Panel) Venat).Controls.Cast<Control>().ToArray();
                        break;
                    
                    
                    default:
                        throw new Exception($"ERROR: ({nameof(InitializeFormDecorations)}) Invalid type argument provided. ({typeof(T).Name} is not a Form, GroupBox, or Panel)");
                }

            }

            var hSeparatorLineScanner = new List<Point[]>();
            var vSeparatorLineScanner = new List<Point[]>();

            // Apply the separator drawing function to any separator lines
            foreach (var line in Controls.OfType<sidgui.Label>()) // Ensure a copy of the control array's being iterated through to avoid breaking the loop my removing items it it while iterating over them
            {
                if (line.IsSeparatorLine)
                {
                    if (line.Size.Width > line.Size.Height)
                    {
                        // Horizontal Lines
                        hSeparatorLineScanner.Add(new Point[2]
                        {
                            new Point(
                                line.StretchToFitForm ? 1 : line.Location.X,
                                line.Location.Y + 7
                            ),
                            new Point(
                                line.StretchToFitForm ? line.Parent.Width - 2 : line.Location.X + line.Width,
                                line.Location.Y + 7
                            )
                        });
                    }
                    else {
                        // Vertical Lines (the + 3 is to center the line with the displayed lines in the editor)
                        vSeparatorLineScanner.Add(new Point[2]
                        {
                            new Point(
                                line.Location.X + 3,
                                line.StretchToFitForm ? 1 : line.Location.Y
                            ),
                            new Point(
                                line.Location.X + 3,
                                line.StretchToFitForm ? line.Parent.Height - 2 : line.Location.Y + line.Height
                            )
                        });
                    }

                    ((Control) Venat).Controls.Remove(line);
                }
            }

            switch (typeof(T))
            {
                case var x when x == typeof(Form):
                    var form = (Form) Venat;

                    if (hSeparatorLineScanner.Count > 0)
                    {
                        form.HSeparatorLines = hSeparatorLineScanner.ToArray();
                    }
                    if (vSeparatorLineScanner.Count > 0)
                    {
                        form.VSeparatorLines = vSeparatorLineScanner.ToArray();
                    }


                    form.Paint += (venat, yoshiP) => DrawContainerDecorations<Form>(venat, yoshiP);
                    break;

                case var x when x == typeof(GroupBox):
                    var groupBox = (GroupBox)Venat;

                    if (hSeparatorLineScanner.Count > 0)
                    {
                        groupBox.HSeparatorLines = hSeparatorLineScanner.ToArray();
                    }
                    if (vSeparatorLineScanner.Count > 0)
                    {
                        groupBox.VSeparatorLines = vSeparatorLineScanner.ToArray();
                    }


                    groupBox.Paint += (venat, yoshiP) => DrawContainerDecorations<GroupBox>(venat, yoshiP);
                    break;

                case var x when x == typeof(Panel):
                    var panel = (Panel) Venat;

                    if (hSeparatorLineScanner.Count > 0)
                    {
                        panel.HSeparatorLines = hSeparatorLineScanner.ToArray();
                    }
                    if (vSeparatorLineScanner.Count > 0)
                    {
                        panel.VSeparatorLines = vSeparatorLineScanner.ToArray();
                    }


                    panel.Paint += (venat, yoshiP) => DrawContainerDecorations<Panel>(venat, yoshiP);
                    break;


                default:
                    throw new Exception($"ERROR: ({nameof(InitializeFormDecorations)}) Invalid type argument provided. ({typeof(T).Name} is not a Form, GroupBox, or Panel)");
            }
        }






        /// <summary>
        /// //!
        /// </summary>
        /// <param name="Venat"></param>
        public void RunStyleConsistencyCheck(SIDGUI Venat)
        {
            var controls = Venat.Controls;

            foreach (Control control in controls)
            {
                switch ((ControlDesignRoles) (control.Tag ?? ControlDesignRoles.None))
                {
                /*
                    case var x when x == typeof(Form) || x == typeof(GroupBox) || x == typeof(Panel):
                        break;

                    case var x when x == typeof(Button):
                        break;


                    case var x when x == typeof(TextBox):
                        break;


                    case var x when x == typeof(RichTextBox):
                        break;
                */


                    case ControlDesignRoles.None:
                        echo($"Ignoring control \"{control.Name}\". (design role is none)");
                        break;
                    
                    case ControlDesignRoles.Container:
                        break;
                    
                    case ControlDesignRoles.TitleLabel:
                        break;
                    
                    case ControlDesignRoles.InfoLabel:
                        break;
                    
                    case ControlDesignRoles.InputTextBox:
                        break;
                    
                    case ControlDesignRoles.OutputTextBox:
                        break;

                    case ControlDesignRoles.LargeButton:
                        break;

                    case ControlDesignRoles.SmallButton:
                        break;

                    default:
                        echo($"Unhandled {nameof(ControlDesignRoles).Substring(0, nameof(ControlDesignRoles).Length - 2)} \"{(ControlDesignRoles) control.Tag}\" for control {control.Name ?? "null"}.");
                        break;
                }
            }
        }






        /// <summary>
        /// //!
        /// </summary>
        /// <param name="Venat"></param>
        public void ForceStyleConsistency(SIDGUI Venat)
        {
            var controls = Venat.Controls;

            foreach (Control control in controls)
            {

            }
        }
        #endregion
    }
}
