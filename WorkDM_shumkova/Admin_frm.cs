using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkDM_shumkova
{
    public partial class Admin_frm :Form
    {
        public Admin_frm ()
        {
            InitializeComponent( );
        }

        private void Admin_frm_FormClosing (object sender, FormClosingEventArgs e)
        {
            Application.Exit( );
        }
    }
}
