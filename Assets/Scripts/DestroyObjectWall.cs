using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectWall : MonoBehaviour
{
    public PlayerController controller;

    void OnTriggerEnter(Collider block)
    {
        if (block.gameObject == controller.telekinesis.objectIHave)
            controller.telekinesis.dropObject();
        if (block.gameObject.tag == "Block")
            Destroy(block.gameObject);
    }
}
