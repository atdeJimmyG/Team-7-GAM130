using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScaleMin : MonoBehaviour
{
    private Vector3 minLocalScale;
    float minlocalScaleMagnitude;

    void Start() {

        minLocalScale = new Vector3(0.5f, 0.5f, 0.5f);
        minlocalScaleMagnitude = minLocalScale.magnitude;
    }


    void FixedUpdate() {

        float actualLocalScaleMagnitude = transform.localScale.magnitude;
         if (Input.GetMouseButton(0) && (Input.GetAxis("Mouse ScrollWheel")) != 0f && (actualLocalScaleMagnitude < minlocalScaleMagnitude)) {
            transform.localScale += new Vector3(0.5F, 0.5F, 0.5f);
        }
    }
}
