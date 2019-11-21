using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            var copy = new Copy();
            var calls = get.GetCalls(10);


            CopyConfig.ConnectionString = 
                "Server=localhost;Port=5432;Database=billing;User ID=postgres;Password=11111111";
            
            CopyTypeMapper
                .MapType(typeof(Call))
                .MapProperty(nameof(Call.Duration), NpgsqlDbType.Integer)
                .MapProperty(nameof(Call.EndTime), NpgsqlDbType.Timestamp);

            await copy.InsertAsync(calls).ConfigureAwait(false);


            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Visualization();
            mainForm.Controls.AddRange(new Engine().GetLinkedControls());
            Application.Run(mainForm);
        }
    }
}