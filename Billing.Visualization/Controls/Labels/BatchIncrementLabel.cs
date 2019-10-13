using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.Labels
{
    public class BatchIncrementLabel:Label
    {
        public BatchIncrementLabel()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Location = new Point {X = 1110, Y = 302};
            Text = "Batch increment";
            FlatStyle = FlatStyle.Flat;
            AutoSize = true;
            Font=new Font(FontFamily.GenericSansSerif, 12);
        }
    }
}