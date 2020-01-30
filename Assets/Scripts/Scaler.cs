using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    GameObject scaler;
    // Start is called before the first frame update
    void Start() {
        scaler = GameObject.Find("Cube");
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update() {

        float scaleSize = 0.00f;
        scaleSize = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.name == "Cube") {
                    scaler.transform.localScale += new Vector3(scaleSize, scaleSize, scaleSize);
                }
            }
        }

    }
}
