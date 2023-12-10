using Game.playerInformation;
using Unity.Netcode;
using UnityEngine;

namespace Game.towers
{
    public class Tower : NetworkBehaviour
    {
        [SerializeField]
        protected int cost;
        [SerializeField]
        protected int size;
        [SerializeField]
        private SoundManager.Sound buildingSound;
        protected BuildController owner;

        protected void Start()
        {
            SoundManager.PlaySound(buildingSound);
        }

        public Vector2 CenterOnGrid(Vector2 point)
        {
            if (size % 2 == 1) point -= Vector2.one / 2;

            point.x = Mathf.Round(point.x);
            point.y = Mathf.Round(point.y);

            if (size % 2 == 1) point += Vector2.one / 2;
            return point;
        }

        public int GetCost()
        {
            return cost;
        }

        public int GetSize()
        {
            return size;
        }

        public void SetOwner(BuildController owner)
        {
            this.owner = owner;
        }
    }
}

