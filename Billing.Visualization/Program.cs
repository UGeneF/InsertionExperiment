using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Billing.Database.EntityFramework;
using Billing.Models;
using Billing.Service.Experiment;
using Billing.Visualization.Controls;
using Npgsql;
using NpgsqlTypes;
using PostgresCopy;

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

            var get = new CallGenerator();
            var calls = get.GetCalls(10);
            
            
            CopyTypeMapper
                .MapType(typeof(Call))
                .MapProperty(nameof(Call.Duration), NpgsqlDbType.Integer)
                .MapProperty(nameof(Call.EndTime), NpgsqlDbType.Timestamp);

            var context = new BillingContext();
            await context.BulkCopyAsync(calls).ConfigureAwait(false);


            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Visualization();
            mainForm.Controls.AddRange(new Engine().GetLinkedControls());
            Application.Run(mainForm);
        }
    }
}