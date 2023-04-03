using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace choinka
{
    public class IkonaWZasobniku
    {
        private NotifyIcon notifyIcon;
        private System.Windows.Window okno;

        public IkonaWZasobniku(System.Windows.Window okno)
        {
            this.okno = okno;
            string nazwaIkony = "choinka.ico";
            string nazwaAplikacji = Application.ProductName;
            System.Windows.Resources.StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri(@"/" + 
                nazwaAplikacji + ";component/" + nazwaIkony, UriKind.RelativeOrAbsolute));
            Icon ikona = new Icon(sri.Stream);

            ContextMenuStrip menu = twórzMenu();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = ikona;
            notifyIcon.Text = "Choinka 2023";
            notifyIcon.ContextMenuStrip = menu;
            notifyIcon.Visible = true;

            notifyIcon.DoubleClick += (s, e) =>
            {
                notifyIcon.BalloonTipTitle = "Choinka 2023";
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipText = "Wesołych świąt!";
                notifyIcon.ShowBalloonTip(3000);
            };

            okno.MouseRightButtonDown += (s, e) =>
            {
                System.Windows.Point p = okno.PointToScreen(e.GetPosition(okno));
                menu.Show((int)p.X, (int)p.Y);

            };
        }
        private ContextMenuStrip twórzMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem ukryjToolStripMenuItem = new ToolStripMenuItem("Ukryj");
            ukryjToolStripMenuItem.Click += (s, e) => { okno.Hide(); };
            menu.Items.Add(ukryjToolStripMenuItem);

            ToolStripMenuItem przywróćToolStripMenuItem = new ToolStripMenuItem("Przywróć");
            przywróćToolStripMenuItem.Click += (s, e) => { okno.Show(); };
            menu.Items.Add(przywróćToolStripMenuItem);

            ToolStripMenuItem zamknijToolStripMenuItem = new ToolStripMenuItem("Zamknij");
            zamknijToolStripMenuItem.Click += (s, e) => { okno.Close(); };
            menu.Items.Add(zamknijToolStripMenuItem);

            return menu;
        }

        public bool Widoczna
        {
            get
            {
                return notifyIcon.Visible;
            }
            set
            {
                notifyIcon.Visible = value;
            }
        }
        public void Usuń()
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            notifyIcon = null;
        }
    }
}
