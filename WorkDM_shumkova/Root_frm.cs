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
    public partial class Root_frm :Form
    {
        public Root_frm ()
        {
            InitializeComponent( );
        }

        private void Root_frm_FormClosing (object sender, FormClosingEventArgs e)
        {
            Application.Exit( );
        }
    }
}
