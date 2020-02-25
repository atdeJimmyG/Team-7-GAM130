using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script handles the freezing of certain freezable objects
public class Freezable : MonoBehaviour
{
    public Material NewMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with an object tagged with "Fire", the object this script is attached to will be destroyed
        if (collision.gameObject.tag == "Ice")
        {
            gameObject.GetComponent<MeshRenderer>().material = NewMaterial;
            gameObject.tag = "Block";
        }
    }
}
