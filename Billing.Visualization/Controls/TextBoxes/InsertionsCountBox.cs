using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.TextBoxes
{
    public class InsertionsCountBox:TextBox
    {
        public InsertionsCountBox()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Location = new Point {X = 1000, Y = 350};
            Text = "15";
        }
        
        public int GetValue()
        {
            return int.Parse(Text);
        }
    }
}