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
            _contentsStorage.Add(new Content("ThinCanon", ContentType.Tower, ContentSubType.Turret, 0));
            _contentsStorage.Add(new Content("BigCanon", ContentType.Tower, ContentSubType.Turret, 0));
            _contentsStorage.Add(new Content("SphereBase", ContentType.Tower, ContentSubType.Turret,0));
            _contentsStorage.Add(new Content("TriangleBase", ContentType.Tower, ContentSubType.Turret,0));
            _contentsStorage.Add(new Content("YellowCanon", ContentType.Tower, ContentSubType.Turret));
            _contentsStorage.Add(new Content("GreenCanon", ContentType.Tower, ContentSubType.Turret));
            _contentsStorage.Add(new Content("BlueCanon", ContentType.Tower, ContentSubType.Turret));
            _contentsStorage.Add(new Content("RedCanon", ContentType.Tower, ContentSubType.Turret));
            _contentsStorage.Add(new Content("BlackBase", ContentType.Tower, ContentSubType.Turret));
            _contentsStorage.Add(new Content("WhiteBase", ContentType.Tower, ContentSubType.Turret));
        }

        public static bool BuyContent(ContentType contentType, string name)
        {
            var contentIndex = GetContentIndexByType(contentType, name);
            
            if (!_contentsStorage[contentIndex].Unlocked && _contentsStorage[contentIndex].Price <= _gems)
            {
                _contentsStorage[contentIndex].Unlocked = true;
                _gems -=_contentsStorage[contentIndex].Price;
                return true;
            }

            return false;
        }
        
        public static bool BuyContent(ContentType contentType, ContentSubType contentSubType, string name)
        {
            var contentIndex = GetContentIndexByTypeAndSubtype(contentType, contentSubType, name);
            
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
            var contentIndex = GetContentIndexByType(contentType, name);
            Debug.Log(contentIndex);
            if (_contentsStorage[contentIndex].Unlocked && !_contentsStorage[contentIndex].Activated)
            {
                DisableContents(contentType);
                _contentsStorage[contentIndex].Activated = true;
                return true;
            }

            return false;
        }
        
        public static bool ActivateContent(ContentType contentType, ContentSubType contentSubType, string name)
        {
            var contentIndex = GetContentIndexByTypeAndSubtype(contentType, contentSubType, name);
            Debug.Log(contentIndex);
            if (_contentsStorage[contentIndex].Unlocked && !_contentsStorage[contentIndex].Activated)
            {
                DisableContents(contentType);
                _contentsStorage[contentIndex].Activated = true;
                Debug.Log(_contentsStorage[contentIndex].Name);
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
        
        private static void DisableContents(ContentType contentType, ContentSubType contentSubType)
        {
            _contentsStorage
                .Where(content => content.ContentType == contentType)
                .Where(content => content.ContentSubType == contentSubType)
                .ToList()
                .ForEach(content => content.Activated = false);
        }

        private static int GetContentIndexByType(ContentType contentType, string name)
        {
            Debug.Log(name);
            Debug.Log(contentType);
            Debug.Log(_contentsStorage
                .Where(content => content.ContentType == contentType)
                .ToList().ToString());
            Debug.Log(name);
            return _contentsStorage
                .Where(content => content.ContentType == contentType && content.ContentSubType == ContentSubType.Turret)
                .ToList()
                .FindIndex(content => string.Equals(content.Name,name));
        }
        
        private static int GetContentIndexByTypeAndSubtype(ContentType contentType, ContentSubType contentSubType, string name)
        {
            return _contentsStorage
                .Where(content => content.ContentType == contentType)
                .Where(content => content.ContentSubType == contentSubType)
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