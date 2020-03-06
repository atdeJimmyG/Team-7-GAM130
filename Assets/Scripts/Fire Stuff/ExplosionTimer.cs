using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTimer : MonoBehaviour
{
    //This variable is the maximum lifespan of an explosion
    private float lifespan = 0.01f;

    void Start()
    {
        //The explosion will disapear <lifespan> seconds after intantiation
        Destroy(gameObject, lifespan);
    }
}
