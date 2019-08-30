using System.Collections.Generic;
using System.Numerics;

namespace BuildCalypse.CodeGen
{
    public class PlacerCircuitFactory
    {
        public IEnumerable<string> GetCommandsToPlace(Vector3 origin)
        {
            string SetBlock(string itemName, Vector3 offset)
            {
                var pos = origin + offset;
                return $"setblock {itemName} {pos.X} {pos.Y} {pos.Z}";
            }

            yield return SetBlock("redstone_comparetor", new Vector3(0, 0, 1));
            yield return SetBlock("redstone", new Vector3(0, 0, 2));
            for (int i = 0; i < 8; i++)
            {
                yield return SetBlock("redstone", new Vector3(i, 0, 3));
                yield return SetBlock("glass", new Vector3(i, 1, 4));
                yield return SetBlock("redstone_repeater", new Vector3(i, 2, 4));
            }
        }
    }
}