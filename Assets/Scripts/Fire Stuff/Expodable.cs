using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the exploding of barrels
public class Expodable : MonoBehaviour
{
    //Allows the start position of explosions to be selected via the Unity Editor
    [SerializeField]
    private Transform position;

    //This allows the explosion prefab to be used as a variable in the script after attachment via the Unity Editor
    [SerializeField]
    private GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with an object tagged with "Fire", the object this script is attached to will be destroyed
        if (collision.gameObject.tag == "Fire")
        {
            Destroy(gameObject);
            Instantiate(explosion, position.position, position.rotation);
        }
    }
}