using System.Drawing;
using System;
using System.Windows.Forms;
using System.Linq;

namespace sidgui
{
    partial class SIDGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ExitBtn = new System.Windows.Forms.Button();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.propertySelectionPanel = new sidgui.GroupBox();
            this.propertyEditorPanel = new sidgui.GroupBox();
            this.InfoBtn = new System.Windows.Forms.Button();
            this.DecoderOutputBox = new sidgui.RichTextBox();
            this.ModeLabel = new sidgui.Label();
            this.EncoderModeBtn = new System.Windows.Forms.Button();
            this.DecoderModeBtn = new System.Windows.Forms.Button();
            this.EntryBox = new sidgui.TextBox();
            this.ProcessEntryBtn = new System.Windows.Forms.Button();
            this.unnamedPanel = new sidgui.Panel();
            this.DebugClearSIDBasesBtn = new System.Windows.Forms.Button();
            this.LoadSIDBaseBtn = new System.Windows.Forms.Button();
            this.SeparatorLine1 = new sidgui.Label();
            this.EncoderOutputBox = new sidgui.RichTextBox();
            this.ClearActiveOutputBtn = new System.Windows.Forms.Button();
            this.unnamedPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(183)))), ((int)(((byte)(245)))));
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitBtn.Font = new System.Drawing.Font("Gadugi", 8.25F, System.Drawing.FontStyle.Bold);
            this.ExitBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ExitBtn.Location = new System.Drawing.Point(864, 4);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(22, 22);
            this.ExitBtn.TabIndex = 8;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = false;
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(183)))), ((int)(((byte)(245)))));
            this.MinimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MinimizeBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.MinimizeBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.MinimizeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MinimizeBtn.Location = new System.Drawing.Point(839, 4);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(22, 22);
            this.MinimizeBtn.TabIndex = 7;
            this.MinimizeBtn.Text = "-";
            this.MinimizeBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.MinimizeBtn.UseVisualStyleBackColor = false;
            // 
            // propertySelectionPanel
            // 
            this.propertySelectionPanel.Location = new System.Drawing.Point(0, 0);
            this.propertySelectionPanel.Name = "propertySelectionPanel";
            this.propertySelectionPanel.Size = new System.Drawing.Size(200, 100);
            this.propertySelectionPanel.TabIndex = 0;
            this.propertySelectionPanel.TabStop = false;
            // 
            // propertyEditorPanel
            // 
            this.propertyEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.propertyEditorPanel.Name = "propertyEditorPanel";
            this.propertyEditorPanel.Size = new System.Drawing.Size(200, 100);
            this.propertyEditorPanel.TabIndex = 0;
            this.propertyEditorPanel.TabStop = false;
            // 
            // InfoBtn
            // 
            this.InfoBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(183)))), ((int)(((byte)(245)))));
            this.InfoBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.InfoBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.InfoBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.InfoBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.InfoBtn.Location = new System.Drawing.Point(814, 4);
            this.InfoBtn.Name = "InfoBtn";
            this.InfoBtn.Size = new System.Drawing.Size(22, 22);
            this.InfoBtn.TabIndex = 24;
            this.InfoBtn.Text = "?";
            this.InfoBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.InfoBtn.UseVisualStyleBackColor = false;
            this.InfoBtn.Click += new System.EventHandler(this.InfoBtn_Click);
            // 
            // DecoderOutputBox
            // 
            this.DecoderOutputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(14)))));
            this.DecoderOutputBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.DecoderOutputBox.Location = new System.Drawing.Point(9, 142);
            this.DecoderOutputBox.Name = "DecoderOutputBox";
            this.DecoderOutputBox.Size = new System.Drawing.Size(872, 459);
            this.DecoderOutputBox.TabIndex = 26;
            this.DecoderOutputBox.Text = "";
            // 
            // ModeLabel
            // 
            this.ModeLabel.AutoSize = true;
            this.ModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.ModeLabel.IsSeparatorLine = false;
            this.ModeLabel.Location = new System.Drawing.Point(22, 5);
            this.ModeLabel.Name = "ModeLabel";
            this.ModeLabel.Size = new System.Drawing.Size(47, 17);
            this.ModeLabel.StretchToFitForm = false;
            this.ModeLabel.TabIndex = 25;
            this.ModeLabel.Text = "Mode";
            // 
            // EncoderModeBtn
            // 
            this.EncoderModeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.EncoderModeBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.EncoderModeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EncoderModeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncoderModeBtn.Location = new System.Drawing.Point(10, 53);
            this.EncoderModeBtn.Name = "EncoderModeBtn";
            this.EncoderModeBtn.Size = new System.Drawing.Size(75, 23);
            this.EncoderModeBtn.TabIndex = 1;
            this.EncoderModeBtn.Text = "Encoder";
            this.EncoderModeBtn.UseVisualStyleBackColor = false;
            this.EncoderModeBtn.Click += new System.EventHandler(this.EnterEncoderMode);
            // 
            // DecoderModeBtn
            // 
            this.DecoderModeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.DecoderModeBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.DecoderModeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DecoderModeBtn.Location = new System.Drawing.Point(10, 28);
            this.DecoderModeBtn.Name = "DecoderModeBtn";
            this.DecoderModeBtn.Size = new System.Drawing.Size(75, 23);
            this.DecoderModeBtn.TabIndex = 2;
            this.DecoderModeBtn.Text = "Decoder";
            this.DecoderModeBtn.UseVisualStyleBackColor = false;
            this.DecoderModeBtn.Click += new System.EventHandler(this.EnterDecoderMode);
            // 
            // EntryBox
            // 
            this.EntryBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.EntryBox.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F);
            this.EntryBox.ForeColor = System.Drawing.SystemColors.Window;
            this.EntryBox.Location = new System.Drawing.Point(108, 55);
            this.EntryBox.Name = "EntryBox";
            this.EntryBox.Size = new System.Drawing.Size(598, 21);
            this.EntryBox.TabIndex = 27;
            this.EntryBox.TabStop = false;
            this.EntryBox.Text = "C7B0F9AD15CF8832";
            this.EntryBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.EntryBoxEnterButtonPressed);
            // 
            // ProcessEntryBtn
            // 
            this.ProcessEntryBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ProcessEntryBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ProcessEntryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessEntryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessEntryBtn.Location = new System.Drawing.Point(712, 54);
            this.ProcessEntryBtn.Name = "ProcessEntryBtn";
            this.ProcessEntryBtn.Size = new System.Drawing.Size(75, 23);
            this.ProcessEntryBtn.TabIndex = 28;
            this.ProcessEntryBtn.Text = "Mode";
            this.ProcessEntryBtn.UseVisualStyleBackColor = false;
            this.ProcessEntryBtn.Click += new System.EventHandler(this.ProcessEntryBtn_Click);
            // 
            // unnamedPanel
            // 
            this.unnamedPanel.Controls.Add(this.DebugClearSIDBasesBtn);
            this.unnamedPanel.Controls.Add(this.LoadSIDBaseBtn);
            this.unnamedPanel.Controls.Add(this.SeparatorLine1);
            this.unnamedPanel.Controls.Add(this.ProcessEntryBtn);
            this.unnamedPanel.Controls.Add(this.EntryBox);
            this.unnamedPanel.Controls.Add(this.DecoderModeBtn);
            this.unnamedPanel.Controls.Add(this.EncoderModeBtn);
            this.unnamedPanel.Controls.Add(this.ModeLabel);
            this.unnamedPanel.Location = new System.Drawing.Point(9, 8);
            this.unnamedPanel.Name = "unnamedPanel";
            this.unnamedPanel.Size = new System.Drawing.Size(799, 90);
            this.unnamedPanel.TabIndex = 3;
            // 
            // DebugClearSIDBasesBtn
            // 
            this.DebugClearSIDBasesBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DebugClearSIDBasesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.DebugClearSIDBasesBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.DebugClearSIDBasesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DebugClearSIDBasesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugClearSIDBasesBtn.Location = new System.Drawing.Point(237, 29);
            this.DebugClearSIDBasesBtn.Name = "DebugClearSIDBasesBtn";
            this.DebugClearSIDBasesBtn.Size = new System.Drawing.Size(114, 23);
            this.DebugClearSIDBasesBtn.TabIndex = 31;
            this.DebugClearSIDBasesBtn.Text = "Unload all SIDBases";
            this.DebugClearSIDBasesBtn.UseVisualStyleBackColor = false;
            this.DebugClearSIDBasesBtn.Click += new System.EventHandler(this.DebugClearSIDBasesBtn_Click);
            // 
            // LoadSIDBaseBtn
            // 
            this.LoadSIDBaseBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.LoadSIDBaseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.LoadSIDBaseBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.LoadSIDBaseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadSIDBaseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadSIDBaseBtn.Location = new System.Drawing.Point(108, 29);
            this.LoadSIDBaseBtn.Name = "LoadSIDBaseBtn";
            this.LoadSIDBaseBtn.Size = new System.Drawing.Size(126, 23);
            this.LoadSIDBaseBtn.TabIndex = 30;
            this.LoadSIDBaseBtn.Text = "Load another SIDBase";
            this.LoadSIDBaseBtn.UseVisualStyleBackColor = false;
            this.LoadSIDBaseBtn.Click += new System.EventHandler(this.LoadSIDBaseBtn_Click);
            // 
            // SeparatorLine1
            // 
            this.SeparatorLine1.IsSeparatorLine = true;
            this.SeparatorLine1.Location = new System.Drawing.Point(91, 1);
            this.SeparatorLine1.Name = "SeparatorLine1";
            this.SeparatorLine1.Size = new System.Drawing.Size(11, 88);
            this.SeparatorLine1.StretchToFitForm = false;
            this.SeparatorLine1.TabIndex = 27;
            this.SeparatorLine1.Text = "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n";
            // 
            // EncoderOutputBox
            // 
            this.EncoderOutputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(14)))));
            this.EncoderOutputBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.EncoderOutputBox.Location = new System.Drawing.Point(9, 142);
            this.EncoderOutputBox.Name = "EncoderOutputBox";
            this.EncoderOutputBox.Size = new System.Drawing.Size(872, 459);
            this.EncoderOutputBox.TabIndex = 26;
            this.EncoderOutputBox.Text = "";
            // 
            // ClearActiveOutputBtn
            // 
            this.ClearActiveOutputBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClearActiveOutputBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ClearActiveOutputBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearActiveOutputBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearActiveOutputBtn.Location = new System.Drawing.Point(11, 115);
            this.ClearActiveOutputBtn.Name = "ClearActiveOutputBtn";
            this.ClearActiveOutputBtn.Size = new System.Drawing.Size(45, 23);
            this.ClearActiveOutputBtn.TabIndex = 29;
            this.ClearActiveOutputBtn.Text = "Clear";
            this.ClearActiveOutputBtn.UseVisualStyleBackColor = false;
            this.ClearActiveOutputBtn.Click += new System.EventHandler(this.ClearActiveOutputBtn_Click);
            // 
            // SIDGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(2)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(890, 611);
            this.Controls.Add(this.ClearActiveOutputBtn);
            this.Controls.Add(this.EncoderOutputBox);
            this.Controls.Add(this.DecoderOutputBox);
            this.Controls.Add(this.InfoBtn);
            this.Controls.Add(this.unnamedPanel);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.MinimizeBtn);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SIDGUI";
            this.unnamedPanel.ResumeLayout(false);
            this.unnamedPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion



        //================================\\
        //--|   Control Declarations   |--\\
        //================================\\
        #region [Control Declarations]

        public Button[] DropdownMenu = new Button[2];
        private GroupBox propertySelectionPanel;
        #endregion
        public Button ExitBtn;
        public Button MinimizeBtn;
        private GroupBox propertyEditorPanel;
        public Button InfoBtn;
        private RichTextBox DecoderOutputBox;
        private Label ModeLabel;
        public Button EncoderModeBtn;
        public Button DecoderModeBtn;
        private TextBox EntryBox;
        public Button ProcessEntryBtn;
        private Panel unnamedPanel;
        private RichTextBox EncoderOutputBox;
        private Label SeparatorLine1;
        public Button ClearActiveOutputBtn;
        public Button LoadSIDBaseBtn;
        public Button DebugClearSIDBasesBtn;
    }
}

