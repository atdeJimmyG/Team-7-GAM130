using UnityEngine;

public class levelSelector : MonoBehaviour
{
    public SceneFader fader;

    public void Select(string levelname)
    {
        fader.fadeTo(levelname);
    }
}
