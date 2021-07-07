using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScaleMin : MonoBehaviour
{
    void FixedUpdate() {
        float actualLocalScaleMagnitude = transform.localScale.magnitude;
        if(transform.localScale.x < .5f) {
            transform.localScale = new Vector3(.5f, .5f, .5f);
        }
        
    }
}
