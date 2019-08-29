using System.Collections.Generic;

namespace BuildCalypse.CodeGen
{
    public class ShopBookContentsFactory
    {
        private static readonly int PlayerNum = 8;

        public IEnumerable<ShopContent> GetContents(Structure[] structures)
        {
            foreach (var item in structures)
            {
                for (int i = 0; i < PlayerNum; i++)
                {
                    yield return new ShopContent()
                    {
                        PlayerId = i + 1,
                        Id = item.Id,
                        Name = item.Name,
                        Bp = item.Bp,
                        SpawnEggId = item.SpawnEggId,
                    };
                }
            }
        }
    }
}