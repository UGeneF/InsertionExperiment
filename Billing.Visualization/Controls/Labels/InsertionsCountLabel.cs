using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.Labels
{
    public class InsertionsCountLabel:Label
    {
        public InsertionsCountLabel()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Location = new Point {X = 1110, Y = 352};
            Text = "Insertions count";
            AutoSize = true;
            Font=new Font(FontFamily.GenericSansSerif, 12);
        }
    }
}