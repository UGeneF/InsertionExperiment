using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.CheckBoxes
{
    public class EfCheckBox:CheckBox
    {
        public EfCheckBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Entity Framework";
            Location = new Point {X = 1000, Y = 184};
            AutoSize = true;
            Font=new Font(FontFamily.GenericSansSerif, 12);
            Checked = true;

        }
    }
}