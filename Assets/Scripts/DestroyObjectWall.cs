using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectWall : MonoBehaviour
{
    private PlayerController controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider block)
    {
        if (block.gameObject == controller.telekinesis.objectIHave)
            controller.telekinesis.dropObject();
        if (block.gameObject.tag == "Block")
            Destroy(block.gameObject);
    }
}
