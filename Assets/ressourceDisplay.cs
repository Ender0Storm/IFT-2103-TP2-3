using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ressourceDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ressourceText;
    [SerializeField]
    private BuildController buildController;

    // Update is called once per frame
    void Update()
    {
        ressourceText.text = "Money: " + buildController.GetCurrency().ToString();
    }
}
