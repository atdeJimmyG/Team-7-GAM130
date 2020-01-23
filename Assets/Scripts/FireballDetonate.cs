using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDetonate : MonoBehaviour
{
    private void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
