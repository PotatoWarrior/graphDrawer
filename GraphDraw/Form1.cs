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
        int N = 9;
        int[,] matrix = 
            {
                {0, 0, 0, 0, 0, 0, 0, 1, 1}, 
                {0, 0, 0, 0, 0, 1, 0, 0, 1}, 
                {0, 0, 0, 0, 0, 0, 1, 0, 1}, 
                {0, 0, 0, 0, 0, 0, 1, 1, 0}, 
                {0, 0, 0, 0, 0, 0, 1, 0, 0}, 
                {0, 1, 0, 0, 0, 0, 0, 0, 1}, 
                {0, 0, 1, 1, 1, 0, 0, 0, 0}, 
                {1, 0, 0, 1, 0, 0, 0, 0, 0}, 
                {1, 1, 1, 0, 0, 1, 0, 0, 0},
            };
        Graph g;
        public Form1()
        {
            InitializeComponent();
            g = new Graph(matrix, N, pictureBox1);
            g.draw();
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            g = new Graph(matrix, N, pictureBox1);
            g.draw();
        }
        
        

    }
}
