using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.TextBoxes
{
    public class BatchIncrementBox:TextBox
    {
        public BatchIncrementBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Location = new Point {X = 1000, Y = 300};
            Text = "1000";
        }

        public int GetValue()
        {
            return int.Parse(Text);
        }
    }
}