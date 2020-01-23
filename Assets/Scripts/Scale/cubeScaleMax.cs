using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScaleMax : MonoBehaviour
{
    private Vector3 maxLocalScale;
    float maxlocalScaleMagnitude;

    void Start() {

        maxLocalScale = new Vector3(2f, 2f, 2f);
        maxlocalScaleMagnitude = maxLocalScale.magnitude;
    }


    void FixedUpdate() {

        float actualLocalScaleMagnitude = transform.localScale.magnitude;
         if (Input.GetMouseButton(0) && (Input.GetAxis("Mouse ScrollWheel")) != 0f && (actualLocalScaleMagnitude > maxlocalScaleMagnitude)) {
            transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
        }
    }
}
