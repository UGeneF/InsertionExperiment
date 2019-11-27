using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Billing.Database.EntityFramework;
using Billing.Models;
using Copy;
using Npgsql;

namespace Billing.Visualization
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            NpgsqlConnection.GlobalTypeMapper.MapComposite<Call>("call_ct");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<CallType>("call_type_enum");

            var context = new BillingContext();
            await context.BulkCopyAsync(new[] {new Call()});
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Visualization();
            mainForm.Controls.AddRange(new Engine().GetLinkedControls());
            Application.Run(mainForm);
        }
    }
}