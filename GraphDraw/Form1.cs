using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphDraw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        protected override void OnLoad(EventArgs e)
        {
            int[,] matrix = 
            {
                {0, 1, 0, 0, 1, 1, 1, 1}, 
                {1, 0, 0, 1, 0, 0, 0, 0}, 
                {0, 0, 0, 0, 0, 1, 0, 0}, 
                {0, 1, 0, 0, 1, 0, 0, 1}, 
                {1, 0, 0, 1, 0, 0, 0, 1}, 
                {1, 0, 1, 0, 0, 0, 1, 0}, 
                {1, 0, 0, 0, 0, 1, 0, 0},
                {1, 0, 0, 0, 0, 1, 0, 0},
            };
            int N = 8;
            Graph g = new Graph(matrix, N, pictureBox1);
            g.draw();
            //123
        }

    }
}
