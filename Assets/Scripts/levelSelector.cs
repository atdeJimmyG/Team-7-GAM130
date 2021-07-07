﻿using UnityEngine;
using UnityEngine.UI;

public class levelSelector : MonoBehaviour
{
    public SceneFader fader;

    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > levelReached - 1)                      
                levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelname)
    {
        fader.fadeTo(levelname);
    }
}
