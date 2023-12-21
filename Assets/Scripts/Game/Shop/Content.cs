namespace Game.Shop
{
    public class Content
    {
        public readonly string Name;
        public readonly ContentType ContentType;
        public readonly ContentSubType ContentSubType;
        public readonly int Price;
        public bool Activated;
        public bool Unlocked;

        public Content(string name, ContentType contentType, int price)
        {
            Name = name;
            ContentType = contentType;
            ContentSubType = ContentSubType.Null;
            Price = price;
            Unlocked = false;
            Activated = false;
        }
        
        public Content(string name, ContentType contentType, ContentSubType contentSubType, int price)
        {
            Name = name;
            ContentType = contentType;
            ContentSubType = contentSubType;
            Price = price;
            Unlocked = false;
            Activated = false;
        }
        
        public Content(string name, ContentType contentType, ContentSubType contentSubType)
        {
            Name = name;
            ContentType = contentType;
            ContentSubType = contentSubType;
            Price = 0;
            Unlocked = true;
            Activated = false;
        }
        
        public Content(string name, ContentType contentType)
        {
            Name = name;
            ContentType = contentType;
            ContentSubType = ContentSubType.Null;
            Price = 0;
            Unlocked = true;
            Activated = false;
        }
    }
}