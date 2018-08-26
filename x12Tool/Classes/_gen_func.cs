using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace x12Tool {
    public static class _gen_func {

        public static void SetFormatByControlType(Object baseControl) {
            foreach (var control in ((Control)baseControl).Controls) {
                try {
                    if (control is TextBox || control is ComboBox) {
                        ((Control)control).BackColor = GLOBALS.Config.Values.TextBoxBackgroundColor;
                        ((Control)control).ForeColor = GLOBALS.Config.Values.TextBoxForegroundColor;
                        ((Control)control).Font = GLOBALS.Config.Values.TextBoxFont;
                    }
                    if (control is MenuStrip || control is ContextMenuStrip || control is ToolStripMenuItem || control is Button || control is Form) {
                        ((Control)control).BackColor = GLOBALS.Config.Values.MenuBackgroundColor;
                        ((Control)control).ForeColor = GLOBALS.Config.Values.MenuForegroundColor;
                        ((Control)control).Font = GLOBALS.Config.Values.MenuFont;
                    }
                    if (control is SplitContainer) {
                        ((Control)control).BackColor = GLOBALS.Config.Values.InfoPanelBackgroundColor;
                        ((Control)control).ForeColor = GLOBALS.Config.Values.InfoPanelForgroundColor;
                        ((Control)control).Font = GLOBALS.Config.Values.InfoPanelFont;
                    }
                } catch {

                }
                SetFormatByControlType(control);
            }
        }

        public static TabPage get_tab_by_name(String Name, TabControl TabControl) {
            foreach (TabPage Tab in TabControl.TabPages) {
                if (Tab.Name == Name) {
                    return Tab;
                }
            }
            return null;
        }

        public static void checklistbox_all_checked_state(CheckedListBox checked_list_box, bool state) {
            for (int i = 0; i < checked_list_box.Items.Count; i++) {
                checked_list_box.SetItemChecked(i, state);
            }
        }

        public static void colorize_list_view(ListView lv, Config c) {
            lv.BeginUpdate();
            for (int i = 0; i < lv.Items.Count; i += 2) {
                lv.Items[i].BackColor = c.Values.EditorBackgroundColor;
                foreach (ListViewItem.ListViewSubItem s in lv.Items[i].SubItems) {
                    s.BackColor = c.Values.EditorSelectionHighlightColor;
                }
            }
            for (int i = 1; i < lv.Items.Count; i += 2) {
                lv.Items[i].BackColor = c.Values.EditorSelectionHighlightColor;
                foreach (ListViewItem.ListViewSubItem s in lv.Items[i].SubItems) {
                    s.BackColor = c.Values.EditorBackgroundColor;
                }
            }
            lv.EndUpdate();
        }

        public static void smart_column_resize(ListView lv) {
            lv.BeginUpdate();
            foreach (ColumnHeader c in lv.Columns) {
                if (c.Width != 0) {
                    int column_width_a = 0;
                    int column_width_b = 0;
                    lv.AutoResizeColumn(c.Index, ColumnHeaderAutoResizeStyle.ColumnContent);
                    column_width_a = c.Width;
                    lv.AutoResizeColumn(c.Index, ColumnHeaderAutoResizeStyle.HeaderSize);
                    column_width_b = c.Width;

                    if (column_width_a > column_width_b) {
                        c.Width = column_width_a;
                    }
                    else {
                        c.Width = column_width_b;
                    }
                }
            }
            lv.Columns[lv.Columns.Count - 1].Width = -2;
            lv.EndUpdate();
        }

        public static void ensure_directory_structure(String path) {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public static String get_save_path(String Default_FileName, String filter, String initial_directory) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = initial_directory;
            sfd.FileName = Default_FileName;
            sfd.Filter = filter;
            if (sfd.ShowDialog() == DialogResult.OK) {
                return sfd.FileName;
            } else {
                return null;
            }
        }

        public static void recursive_reset(Control initial_control) {
            foreach (Control c in initial_control.Controls) {
                if (c is TextBox) {
                    c.Text = "";
                }
                recursive_reset(c);
            }
        }

        public static Decimal string_to_num(String s) {
            Decimal i = (Decimal.TryParse(s, out i)) ? i : 0;
            return i;
        }
    }
}