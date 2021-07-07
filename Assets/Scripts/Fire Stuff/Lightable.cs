using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightable : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with an object tagged with "Fire", the object this script is attached to will be set on fire
        if (collision.gameObject.tag == "Fire")
        {
            gameObject.GetComponent<Light>().enabled = true;
        }
    }
}
