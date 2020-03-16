using UnityEngine;

//This script handles the destruction of the fireball
public class FireballDetonate : MonoBehaviour
{
    //This variable is the maximum lifespan of a fireball
    private float lifespan = 2f;
    private void OnCollisionEnter()
    {
        //Upon colliding with anything the fireball will detonate
        Destroy(gameObject);
    }

    void Start()
    {
        //If the fireball is still around <lifespan> seconds after being launched, it will be detonate on its own
        transform.Rotate(90, 0, 0);
        Destroy(gameObject, lifespan);

    }
}