using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOptions : MonoBehaviour
{

    public void OnToggleValueChanged()
    {
        SoundManager.PlaySound(SoundManager.Sound.CheckboxSound);
    }
}
