using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.CheckBoxes
{
    public class BinaryCopyCheckBox : CheckBox
    {
        public BinaryCopyCheckBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Binary COPY";
            Location = new Point {X = 1000, Y = 130};
            AutoSize = true;
            Font=new Font(FontFamily.GenericSansSerif, 12);
            Checked = true;
        }
    }
}