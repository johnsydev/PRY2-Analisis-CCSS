using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRY2_Analisis_CCSS
{
    internal static class Program
    {
        [DllImport("kernel32.dll")] //PERMITE CONSOLA
        static extern bool AllocConsole(); //PERMITE CONSOLA
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            AllocConsole(); //PERMITE CONSOLA
            Principal p = new Principal(); //EDITAR CONSOLA EN PRINCIPAL
            p.Main();
        }
    }
}
