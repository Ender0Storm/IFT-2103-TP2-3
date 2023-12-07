namespace Game.Shop
{
    public class Content
    {
        public readonly string Name;
        public readonly ContentType ContentType;
        public readonly int Price;
        public bool Activated;
        public bool Unlocked;

        public Content(string name, ContentType contentType, int price)
        {
            Name = name;
            ContentType = contentType;
            Price = price;
            Unlocked = false;
            Activated = false;
        }
    }
}