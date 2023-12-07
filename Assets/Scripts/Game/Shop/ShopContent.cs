using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Shop
{
    public static class ShopContent
    {
        private static int _gems;
        private static bool _initialization = false;
        private static List<Content> _contentsStorage;

        public static void Initializing()
        {
            if (!_initialization)
            {
                _initialization = true;
                _gems = 0;
                _contentsStorage = new List<Content>();
                InitializeContentStorage();
            }
        }
        public static void AddToGems(int gemsToAdd)
        {
            _gems += gemsToAdd;
        }

        public static int GetGems()
        {
            return _gems;
        }

        private static void InitializeContentStorage()
        {
            _contentsStorage.Add(new Content("Wood", ContentType.Board, 0));
            _contentsStorage.Add(new Content("Digital", ContentType.Board, 0));
        }

        public static bool BuyContent(ContentType contentType, string name)
        {
            var contentIndex = GetContentIndex(contentType, name);
            
            if (!_contentsStorage[contentIndex].Unlocked && _contentsStorage[contentIndex].Price <= _gems)
            {
                _contentsStorage[contentIndex].Unlocked = true;
                _gems -=_contentsStorage[contentIndex].Price;
                return true;
            }

            return false;
        }

        public static bool ActivateContent(ContentType contentType, string name)
        {
            var contentIndex = GetContentIndex(contentType, name);
            if (_contentsStorage[contentIndex].Unlocked && !_contentsStorage[contentIndex].Activated)
            {
                DisableContents(contentType);
                _contentsStorage[contentIndex].Activated = true;
                return true;
            }

            return false;
        }

        private static void DisableContents(ContentType contentType)
        {
            _contentsStorage
                .Where(content => content.ContentType == contentType)
                .ToList()
                .ForEach(content => content.Activated = false);
        }

        private static int GetContentIndex(ContentType contentType, string name)
        {
            return _contentsStorage
                .Where(content => content.ContentType == contentType)
                .ToList()
                .FindIndex(content => content.Name == name);
        }

        public static List<Content> GetContentsByType(ContentType contentType)
        {
            return _contentsStorage
                .Where(content => content.ContentType == contentType)
                .ToList();
        }
    }
}