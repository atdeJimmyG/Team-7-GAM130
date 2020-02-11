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
        fader.fadeTo(NextLevelName);
        PlayerPrefs.SetInt("levelReached", NextLevelIndex);
    }
}
