using System.Collections.Generic;

namespace BuildCalypse.CodeGen
{
    public class ShopBookContentsFactory
    {
        public IEnumerable<ShopContent> GetContents(Structure[] structures, int playerId)
        {
            foreach (var item in structures)
            {
                yield return new ShopContent()
                {
                    PlayerId = playerId,
                    Id = item.Id,
                    Name = item.Name,
                    Bp = item.Bp,
                    SpawnEggId = item.SpawnEggId,
                    Index = item.Index,
                };
            }
        }
    }
}