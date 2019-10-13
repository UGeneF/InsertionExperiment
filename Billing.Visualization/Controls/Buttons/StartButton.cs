using System.Drawing;
using System.Windows.Forms;

namespace Billing.Visualization.Controls.Buttons
{
    public class StartButton:Button
    {
        private const string LetsGo = "Let`s go!";
        private const string Wait = "Wait...";

        public StartButton()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Location = new Point {X = 1000, Y = 500};
            Size = new Size(150, 80);
            Text = LetsGo;
            BackColor=Color.Yellow;
            FlatAppearance.BorderColor=Color.Yellow;
            FlatStyle = FlatStyle.Flat;
            Font=new Font(FontFamily.GenericSansSerif, 20);
        }

        public void BlockButton()
        {
            Enabled = false;
            Text = Wait;
        }

        public void UnblockButton()
        {
            Enabled = true;
            Text = LetsGo;
        }
    }
}