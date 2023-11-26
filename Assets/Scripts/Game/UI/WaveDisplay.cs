using UnityEngine;

namespace Game.ui
{
    public class WaveDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text _waveText;
        [SerializeField]
        private WaveManager _waveManager;

        // Update is called once per frame
        void Update()
        {
            _waveText.text = $"Wave: {_waveManager.GetWave()}";
        }
    }
}

