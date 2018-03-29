using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom
{
    public partial class Form1 : Form
    {
        public const float timeDelta = 0.8f;

        private World _world;
        private System.Timers.Timer _timer;


        public Form1()
        {
            InitializeComponent();
            _world = new World(_dbPanel1.Width, _dbPanel1.Height);
            _timer = new System.Timers.Timer();
            _timer.Elapsed += timerTick;
            _timer.Interval = 20;
            _timer.Enabled = true;

        }

        private void timerTick(object sender, ElapsedEventArgs e)
        {
            _world.Update(timeDelta);
            _dbPanel1.Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void _dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            _world.Render(e.Graphics);
        }

        private void _dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            //World.MouseX = e.X;
            //World.MouseY = e.Y;
            var target = _world.graph.FindNearestNode(new Vector2D(e.X, e.Y));
            _world.StartPathFollowing(target.Index);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 107:
                    // NumPlus
                    World.Intensity += 10;
                    break;
                case 109:
                    // NumMin
                    World.Intensity -= 10;
                    break;
                case 71:
                    World.ShouldRenderGraph = !World.ShouldRenderGraph;
                    break;
            }
            Console.WriteLine(@"Intensity: " + World.Intensity);
        }
    }
}
