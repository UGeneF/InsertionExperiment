using System.Windows.Forms;
using Billing.Visualization.Controls.Buttons;
using Billing.Visualization.Controls.Charts;
using Billing.Visualization.Controls.CheckBoxes;
using Billing.Visualization.Controls.Labels;
using Billing.Visualization.Controls.TextBoxes;

namespace Billing.Visualization.Controls
{
    public class Contols
    {
        public StartButton StartButton { get; }
        public StatisticsChart Chart { get; }
        public EfCheckBox EfCheckBox { get; }
        public BinaryCopyCheckBox BinaryCopyCheckBox{ get; }
        public CompositeTypesCheckBox CompositeTypesCheckBox{ get; }
        public StartBatchBox StartBatchBox{ get; }
        public BatchIncrementBox BatchIncrementBox{ get; }
        
        public InsertionsCountBox InsertionsCountBox{ get; }
        public IteranceBox IteranceBox{ get; }
        
        public StartBatchLabel StartBatchLabel{ get; }
        
        public BatchIncrementLabel BatchIncrementLabel{ get; }
        
        public InsertionsCountLabel InsertionsCountLabel{ get; }
        
        public IteranceLabel IteranceLabel{ get; }

        public Contols()
        {
            StartButton=new StartButton();
            Chart=new StatisticsChart();
            EfCheckBox=new EfCheckBox();
            CompositeTypesCheckBox=new CompositeTypesCheckBox();
            BinaryCopyCheckBox=new BinaryCopyCheckBox();
            StartBatchBox=new StartBatchBox();
            BatchIncrementBox=new BatchIncrementBox();
            InsertionsCountBox=new InsertionsCountBox();
            IteranceBox=new IteranceBox();
            StartBatchLabel=new StartBatchLabel();
            BatchIncrementLabel=new BatchIncrementLabel();
            InsertionsCountLabel=new InsertionsCountLabel();
            IteranceLabel=new IteranceLabel();
        }
        public Control[] GetControls()
        {
            return new Control[]
            {
                StartButton,
                Chart,
                EfCheckBox,
                BinaryCopyCheckBox,
                CompositeTypesCheckBox,
                StartBatchBox,
                BatchIncrementBox,
                InsertionsCountBox,
                IteranceBox,
                StartBatchLabel,
                BatchIncrementLabel,
                InsertionsCountLabel,
                IteranceLabel
            };
        }
    }
}