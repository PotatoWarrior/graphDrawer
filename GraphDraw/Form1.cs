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
        Graph graph;
        public Form1()
        {
            InitializeComponent();
            graph = new Graph(pictureBox1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            graph.setK(Decimal.ToInt32(numTime.Value));
            graph.addVertex(Decimal.ToInt32(numX.Value) - Vertex.VERTEX_D / 2, Decimal.ToInt32(numY.Value) - Vertex.VERTEX_D / 2, Decimal.ToInt32(numDiam.Value));
            graph.draw();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            graph.setK(Decimal.ToInt32(numTime.Value));
            graph.addVertex(e.X - Vertex.VERTEX_D / 2, e.Y - Vertex.VERTEX_D / 2, Decimal.ToInt32(numDiam.Value));
            graph.draw();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            graph.clear();
        }
    }
}
