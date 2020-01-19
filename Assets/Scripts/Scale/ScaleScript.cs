using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    public GameObject Cube;

    public float scaleValue = 0.00f;
    int counter = 0;


    void Update()
    {
        scaleValue += Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(0) && counter <=1) { 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Debug.Log(hit.transform.name);
                Cube.transform.localScale += new Vector3(scaleValue, scaleValue, scaleValue);
                //counter += 1;
            }

            if (hit.transform.name == "Cube"){
                
            }
        }
        
    }
}
