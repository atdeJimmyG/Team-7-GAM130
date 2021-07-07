using UnityEngine;

public class Meltable : MonoBehaviour
{
    public Material NewMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with an object tagged with "Fire", the object this script is attached to will be destroyed
        if (collision.gameObject.tag == "Fire")
        {
            gameObject.GetComponent<MeshRenderer>().material = NewMaterial;
            gameObject.tag = "Freezable";
        }
    }
}