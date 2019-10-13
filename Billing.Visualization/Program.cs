using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Billing.Models;
using Billing.Visualization.Controls;
using Npgsql;

namespace Billing.Visualization
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NpgsqlConnection.GlobalTypeMapper.MapComposite<Call>("call_ct");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<CallType>("call_type_enum");
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Visualization();
            mainForm.Controls.AddRange(new Engine().GetLinkedControls());
            Application.Run(mainForm);
        }
    }
}