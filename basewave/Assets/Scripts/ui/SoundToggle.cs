using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundToggle : MonoBehaviour {
    public Sprite ToggleIcon;
    public Toggle toggle;
    public void ToggleState() {
        // ToggleIcon =  Resources.Load<Sprite>((toggle.isOn) ? "speaker-on" : "speaker-off");
        // toggle.GetComponent<Background>().image = ToggleIcon;
        // if (toggle.isOn) {
        //     Debug.Log("Toggle disabled!");
        // } else {
        //     Debug.Log("Toggle enabled!");
        // }
    }
}
