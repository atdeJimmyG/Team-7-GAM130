using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            Destroy(gameObject);
        }
    }
}
