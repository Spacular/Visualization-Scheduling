using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizationScheduling
{
    public partial class RR_Form : Form
    {
        public Visualization_Scheduling main = new Visualization_Scheduling();
        public RR_Form()
        {
            InitializeComponent();
        }

        public RR_Form(Visualization_Scheduling _form)
        {
            main = _form;

        }
    }
}
