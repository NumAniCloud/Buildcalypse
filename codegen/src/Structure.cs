using System.Numerics;

namespace BuildCalypse.CodeGen
{
    public class Structure
    {
        public string MobName { get; internal set; }
        public string Id { get; internal set; }
        public string FullName { get; internal set; }
        public Vector3 RedstoneOffset { get; internal set; }
        public Vector3 Offset { get; internal set; }
    }
}