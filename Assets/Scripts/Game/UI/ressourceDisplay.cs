using Game.playerInformation;
using UnityEngine;

namespace Game.ui
{
    public class RessourceDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text _ressourceText;
        [SerializeField]
        private BuildController _buildController;

        // Update is called once per frame
        void Update()
        {
            _ressourceText.text = $"Coins: {_buildController.GetCurrency()}";
        }
    }
}
