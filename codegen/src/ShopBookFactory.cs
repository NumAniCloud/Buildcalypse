using System.Collections.Generic;

namespace BuildCalypse.CodeGen
{
    public class ShopBookFactory
    {
        public ShopBook GetShopBook(ShopContent[] contents, int playerId, ShopTriggerObjectives triggers)
        {
            return new ShopBook(contents, playerId, triggers);
        }
    }
}