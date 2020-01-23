using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour {
    public GameObject Cube;

    public float scaleValue = 0.00f;


    void Update() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) { // forward
            scaleValue = 0.01f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) { // backwards
            scaleValue = -0.01f;
        }
        scaleValue += Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(0) && (Input.GetAxis("Mouse ScrollWheel")) != 0f) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Debug.Log(hit.transform.name);
                Cube.transform.localScale += new Vector3(scaleValue, scaleValue, scaleValue);
            }
        }
    }
}