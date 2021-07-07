using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public SceneFader fader;
    public string NextLevelName;
    public int NextLevelIndex;

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            fader.fadeTo(NextLevelName);
            PlayerPrefs.SetInt("levelReached", NextLevelIndex);
        }
    }
}
