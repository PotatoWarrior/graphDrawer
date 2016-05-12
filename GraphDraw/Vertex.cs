namespace GraphDraw
{
    class Vertex
    {
        public int index;
        public float x;
        public float y;
        public static int VERTEX_D = 30;
        public Vertex(float x, float y, int index)
        {
            this.index = index;
            this.x = x;
            this.y = y;
        }
    }
}
