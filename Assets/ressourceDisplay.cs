using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceDisplay : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _ressourceText;
    [SerializeField]
    private BuildController _buildController;

    // Update is called once per frame
    void Update()
    {
        _ressourceText.text = "Money: " + _buildController.GetCurrency().ToString();
    }
}
