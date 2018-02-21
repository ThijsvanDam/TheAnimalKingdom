using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheAnimalKingdom
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            World _mainWorld = new World();
            while (true)
            {
                _mainWorld.ReadInputs();
                _mainWorld.Process();
                _mainWorld.Render();
            }

        }
    }
}
