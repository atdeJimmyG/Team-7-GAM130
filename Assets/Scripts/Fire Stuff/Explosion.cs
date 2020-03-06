using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Explosion : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    { 
        Debug.Log("sdgsadgdggdgd");
        //If an object tagged with "Destructable" enters the trigger, it will be destroyed
        if (collision.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
}
