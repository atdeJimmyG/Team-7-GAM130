using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScaleMax : MonoBehaviour
{
    void FixedUpdate() {

        float actualLocalScaleMagnitude = transform.localScale.magnitude;
        if (transform.localScale.x > 2) {
            transform.localScale = new Vector3(2f, 2f, 2f);
        }

    }
}
