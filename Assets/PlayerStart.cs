using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    // Reference to the Player. Drag a Player Prefab into this field in the Inspector.
    public GameObject player;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // Instantiate at position of the game object and zero rotation.
        Instantiate(player, this.gameObject.transform.position, Quaternion.identity);
        MouseLook temp = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
        temp.spawned();
    }
}
