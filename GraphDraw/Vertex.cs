using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDraw
{
    class Vertex
    {
        public int index;
        public int x;
        public int y;
        public int diam;
        public static int VERTEX_D = 30;
        public Vertex(int x, int y, int index, int diam)
        {
            this.index = index;
            this.x = x;
            this.y = y;
            this.diam = diam;
        }
    }
}
