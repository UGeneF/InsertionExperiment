using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.Labels
{
    public class StartBatchLabel:Label
    {
        public StartBatchLabel()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Location = new Point {X = 1110, Y = 252};
            Text = "Start Batch";
            AutoSize = true;
            Font=new Font(FontFamily.GenericSansSerif, 12);
        }
    }
}