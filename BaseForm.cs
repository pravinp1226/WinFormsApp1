using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinFormsApp1
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public void Control_Enter(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            ctrl.BackColor = Color.Yellow;
        }
        public void Control_Leave(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            ctrl.BackColor = Color.White;
        }

        public void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctrl = (Control)sender;


            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyChar == 27)
            {
                SendKeys.Send("+{TAB}");
            }

            if (ctrl.Tag != null && ctrl.Tag.ToString() == "int")
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b'))
                {
                    e.KeyChar = (char)0;
                }
            }

            else if (ctrl.Tag != null && ctrl.Tag.ToString() == "string")
            {
                string validstr = "abcdefghijklmnopqrstuvwxyz @-\b./";
                if (!(validstr.Contains(e.KeyChar.ToString().ToLower())))
                {
                    e.KeyChar = (char)0;
                }

                if (ctrl.Text.Split(' ').Length > 2 && e.KeyChar == ' ')

                    e.KeyChar = (char)0;

                if (ctrl.Text.Length > 0 && ctrl.Text.Substring(ctrl.Text.Length - 1) == " " && e.KeyChar == ' ')
                    e.KeyChar = (char)0;
            }

            else if (ctrl.Tag != null && ctrl.Tag.ToString() == "double")
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == '\b'))
                {
                    e.KeyChar = (char)0;
                }
                if (ctrl.Text.Contains('.') && e.KeyChar == '.')
                    e.KeyChar = (char)0;

                int dotpos = ctrl.Text.IndexOf('.');
                if (dotpos >= 0 && e.KeyChar != '\b')
                {
                    if (ctrl.Text.Substring(dotpos).Length > 2)
                        e.KeyChar = (char)0;
                }

            }
        }

        public void ClearControl(Control FocusControl, Control.ControlCollection ctrl, bool ClearFocusControl = true)
        {
            foreach (Control item in ctrl)

            {
                if (item is TextBox)
                    item.Text = "";

                if (item.GetType().Name == "ComboBox") 
                {
                    ComboBox cmb = (ComboBox)item;
                    cmb.SelectedIndex = -1;
                }

                if(item is RichTextBox)
                {
                    item.Text = "";
                }
               
            }
            FocusControl.Focus();
        }

    }
}
