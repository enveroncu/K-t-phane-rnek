using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane.Controllers
{
    public static class Utility
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public static void draggable(MouseEventArgs e,Form form)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(form.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        public static void onClose()
        {
            Application.Exit();
           
        }

        public static void Clear(Control form)
        {
            foreach(Control item in form.Controls)
            {
                if (item is TextBox)
                {
                    TextBox i = item as TextBox;
                    i.Clear();

                }
                else if (item is ComboBox)
                {
                    ComboBox c = item as ComboBox;
                    c.SelectedIndex = -1;
                }
                else if (item is NumericUpDown)
                {
                    NumericUpDown n = item as NumericUpDown;
                    n.Value = 0;
                }
                else if (item is GroupBox)
                    Clear(item);
                else if (item is Panel)
                    Clear(item);
                else if (item is TabControl)
                    Clear(item);
            
            }
        }
    }
}
