namespace BuildCalypse.CodeGen
{
    public struct Direction
    {
        public static readonly Direction South = new Direction(0);
        public static readonly Direction West = new Direction(90);
        public static readonly Direction North = new Direction(180);
        public static readonly Direction East = new Direction(270);

        public Direction(int degree)
        {
            Degree = degree;
        }

        public int Degree { get; }

        public string GetStructureBlockRotation()
        {
            return Degree switch
            {
                0 => "NONE",
                90 => "CLOCKWISE_90",
                180 => "CLOCKWISE_180",
                270 => "COUNTERCLOCKWISE_90",
                _ => "NONE",
            };
        }

        public string GetFacingString()
        {
            return Degree switch
            {
                0 => "south",
                90 => "west",
                180 => "north",
                270 => "east",
                _ => "up",
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Direction dir ? this == dir : false;
        }

        public override int GetHashCode() => Degree.GetHashCode() ^ 135;

        public static bool operator==(Direction direction1, Direction direction2)
        {
            return direction1.Degree == direction2.Degree;
        }
        
        public static bool operator!=(Direction direction1, Direction direction2)
        {
            return direction1.Degree != direction2.Degree;
        }
    }
}