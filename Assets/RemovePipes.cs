using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePipes : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Destroy(gameObject);
        }
    }
}
