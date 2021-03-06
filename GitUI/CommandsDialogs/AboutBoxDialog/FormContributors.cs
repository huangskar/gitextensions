﻿using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace GitUI.CommandsDialogs.AboutBoxDialog
{
    public class FormContributors : GitExtensionsForm
    {
        private readonly static string[] tabCaptions = new string[]
        {
            "The Coders", "The Translators", "The Designers"
        };

        private readonly TextBox[] _textboxes = new TextBox[tabCaptions.Length];
        private readonly TabPage[] _tabPages = new TabPage[tabCaptions.Length];
        private TabControl _tabControl;

        public FormContributors()
        {
            SetupForm();
            Translate();
        }

        private TextBox getNewTextBox()
        {
            TextBox tb = new TextBox();
            tb.BackColor = Color.White;
            tb.Dock = DockStyle.Fill;
            tb.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            tb.Margin = new System.Windows.Forms.Padding(0);
            tb.Multiline = true;
            tb.ReadOnly = true;
            tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            tb.TabStop = false;
            return tb;
        }

        private TabPage getNewTabPage(TextBox tb, string caption)
        {
            TabPage tp = new TabPage();
            tp.Margin = new System.Windows.Forms.Padding(0);
            tp.Text = caption;
            tp.Controls.Add(tb);
            return tp;
        }

        private TabControl getNewTabControl()
        {
            TabControl tc = new TabControl();
            tc.Dock = DockStyle.Fill;
            tc.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            tc.ItemSize = new Size(150, 26);
            tc.Margin = new Padding(0);
            tc.Padding = new Point(0, 0);
            tc.SelectedIndex = 0;
            tc.SizeMode = TabSizeMode.Fixed;
            return tc;
        }

        private void SetupForm()
        {
            SuspendLayout();
            Controls.Clear();

            _tabControl = getNewTabControl();
            _tabControl.SuspendLayout();

            for (int i = 0; i < tabCaptions.Length; i++)
            {
                _textboxes[i] = getNewTextBox();
                _tabPages[i] = getNewTabPage(_textboxes[i], tabCaptions[i]);
                _tabControl.Controls.Add(_tabPages[i]);
            }

            Controls.Add(_tabControl);

            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(624, 442);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thanks to...";

            ResumeLayout(false);
        }

        public void LoadContributors(string coders, string translators, string designers, string others)
        {
            const string NEWLINES = @"\r\n?|\n";
            _textboxes[0].Text = Regex.Replace(coders, NEWLINES, " ");
            _textboxes[1].Text = Regex.Replace(translators, NEWLINES, " ");
            _textboxes[2].Text = Regex.Replace(designers, NEWLINES, " ");
        }
    }
}
