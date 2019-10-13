using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.Labels
{
    public class IteranceLabel:Label
    {
        public IteranceLabel()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Location = new Point {X = 1110, Y = 402};
            Text = "Iterance";
            AutoSize = true;
            Font=new Font(FontFamily.GenericSansSerif, 12);
        }
    }
}