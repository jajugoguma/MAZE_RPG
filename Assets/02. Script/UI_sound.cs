using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_sound : MonoBehaviour
{
    public UISlider soundSlider;

    public void OnAction()
    {
        float soundval = soundSlider.value;
        SoundManager.Instance.audioComponent.volume = soundval;
    }

}
