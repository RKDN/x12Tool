using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace x12Tool {
    public class DoubleBufferedTreeView : TreeView {
        const int WM_PRINTCLIENT = 792;
        const int PRF_CLIENT = 4;

        protected override CreateParams CreateParams {
            get {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        public DoubleBufferedTreeView() : base() {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e) {
            if (GetStyle(ControlStyles.UserPaint)) {
                Message m = new Message();
                m.HWnd = Handle;
                m.Msg = WM_PRINTCLIENT;
                m.WParam = e.Graphics.GetHdc();
                m.LParam = (IntPtr)PRF_CLIENT;
                DefWndProc(ref m);
                e.Graphics.ReleaseHdc(m.WParam);
            }
            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            this.BeginUpdate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            this.EndUpdate();
           base.OnMouseUp(e);
        }
    }
}
