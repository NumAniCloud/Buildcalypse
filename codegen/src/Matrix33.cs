namespace BuildCalypse.CodeGen.Numerics
{
    public struct Matrix33
    {
        private readonly int[,] Elements;

        public Matrix33(int a1, int a2, int a3, int b1, int b2, int b3, int c1, int c2, int c3)
        {
            Elements = new int[,]
            {
                {a1, a2, a3},
                {b1, b2, b3},
                {c1, c2, c3}
            };
        }

        public static Vector3 operator*(Matrix33 left, Vector3 right)
        {
            var e = left.Elements;
            return new Vector3(
                e[0,0] * right.X + e[0,1] * right.Y + e[0,2] * right.Z,
                e[1,0] * right.X + e[1,1] * right.Y + e[1,2] * right.Z,
                e[2,0] * right.X + e[2,1] * right.Y + e[2,2] * right.Z
            );
        }
    }
}