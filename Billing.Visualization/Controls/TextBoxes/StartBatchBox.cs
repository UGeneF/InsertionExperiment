using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.TextBoxes
{
    public class StartBatchBox:TextBox
    {
        public StartBatchBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Location = new Point {X = 1000, Y = 250};
            Text = "1000";
        }
        
        public int GetValue()
        {
            return int.Parse(Text);
        }
    }
}