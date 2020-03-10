using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSprayActive : MonoBehaviour
{
    public static GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(90, 0, 0);
        transform.Translate(0, 0.5f, 0.4f);
        player = GameObject.FindGameObjectWithTag("MainCamera");
        transform.SetParent(player.transform);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Destroy(gameObject);
        }
    }
}
