namespace Game.Shop
{
    public static class ShopContent
    {
        private static int _gems;
        private static bool _initialization = false;

        public static void Initializing()
        {
            if (!_initialization)
            {
                _initialization = true;
                _gems = 0;
            }
        }
        public static void AddToGems(int gemsToAdd)
        {
            _gems += gemsToAdd;
        }

        public static int getGems()
        {
            return _gems;
        }
    }
}