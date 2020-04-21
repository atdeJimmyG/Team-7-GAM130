
using UnityEngine;

public class BGSoundScript : MonoBehaviour {

    private static BGSoundScript instance = null;
    public static BGSoundScript Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}