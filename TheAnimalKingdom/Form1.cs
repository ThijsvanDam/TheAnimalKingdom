using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom
{
    public partial class Form1 : Form
    {
        private World _world;
        
        public Form1()
        {
            InitializeComponent();
            
            _world = new World();
        }
        
        private void _dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            //ToDo: Implement
        }
        
        private void _dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            //ToDo: Implement
        }
    }
}
