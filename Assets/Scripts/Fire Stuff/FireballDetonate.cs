using UnityEngine;

//This script handles the destruction of the fireball
public class FireballDetonate : MonoBehaviour
{
    //This variable is the maximum lifespan of a fireball
    private float lifespan = 2f;
    private void OnCollisionEnter()
    {
        Debug.Log("KMS");
        //Upon colliding with anything the fireball will detonate
        Destroy(gameObject);
    }

    void Start()
    {
        //If the fireball is still around <lifespan> seconds after being launched, it will be detonate on its own
        Destroy(gameObject, lifespan);
    }
}