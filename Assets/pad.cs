using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pad : MonoBehaviour
{
    public Animator Test;
    // Start is called before the first frame update
    
    void OnTriggerEnter(Collider col)
    {
        Test.SetBool("Lower", true);
    }

    void OnTriggerExit(Collider col)
    {
        Test.SetBool("Lower", false);
        Debug.Log("udfgkjdfigongfihj");
    }
}
