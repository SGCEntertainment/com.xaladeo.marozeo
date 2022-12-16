using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private bool isMute;

    void Start()
    {
        isMute = PlayerPrefs.GetInt("MuteSound") == 1;
        AudioListener.pause = isMute;
    }

    public void MuteButton()
    {
        isMute = !isMute;
        AudioListener.pause = isMute;
        PlayerPrefs.SetInt("MuteSound", isMute ? 1 : 0);
    }
}
