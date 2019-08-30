using System.Numerics;

namespace BuildCalypse.CodeGen
{
    public class Structure
    {
        public string MobName { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public Vector3 RedstoneOffset;
        public Vector3 Offset;
        public string Name { get; set; }
        public int Bp { get; set; }
        public string SpawnEggId { get; set; }

        // シリアライズ用
        public float RedstoneX { set { RedstoneOffset.X = value; } }
        public float RedstoneY { set { RedstoneOffset.Y = value; } }
        public float RedstoneZ { set { RedstoneOffset.Z = value; } }
        public float OffsetX { set { Offset.X = value; } }
        public float OffsetY { set { Offset.Y = value; } }
        public float OffsetZ { set { Offset.Z = value; } }
    }
}