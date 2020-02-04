using UnityEngine;

//This script handles the destruction of certain flammable objects
public class Burnable : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with an object tagged with "Fire", the object this script is attached to will be destroyed
        if (collision.gameObject.tag == "Fire")
        {
            Destroy(gameObject);
        }
    }
}
