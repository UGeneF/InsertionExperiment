using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.CheckBoxes
{
    public class CompositeTypesCheckBox:CheckBox
    {
        public CompositeTypesCheckBox()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Text = "Composite types";
            Location = new Point {X = 1000, Y = 157};
            AutoSize = true;
            Font=new Font(FontFamily.GenericSansSerif, 12);
            Checked = true;

        }
    }
}