namespace BuildCalypse.CodeGen
{
    public class ShopBook
    {
        public ShopBook(ShopContent[] contents)
        {
            Contents = contents;
        }

        public ShopContent[] Contents { get; }

        public string GetGiveCommand()
        {
            return "";
        }
    }
}