using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public SoundAudioClip[] soundAudioClips;
    public CustomTile[] tiles;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        public float repeatTime;
    }
    [System.Serializable]
    public class CustomTile
    {
        public string name;
        public TileBase tileBase;
        public TileType tileType;
        public bool canWalk;
        public bool canBuild;
        public Neighbours neighbours;

        [System.Serializable]
        public class Neighbours
        {
            public TileType up;
            public TileType down;
            public TileType left;
            public TileType right;

            public Neighbours(TileType defaultType)
            {
                up = defaultType;
                down = defaultType;
                left = defaultType;
                right = defaultType;
            }
        };
        public enum TileType
        {
            Grass,
            Path,
            Rock,
            Any,
            Town,
            Spawner
        }
    }
}
