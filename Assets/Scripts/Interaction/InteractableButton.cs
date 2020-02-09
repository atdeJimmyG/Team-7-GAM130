using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour, IIntractable
{
    public float MaxRange { get { return maxRange;  } }

    private const float maxRange = 2f;

    public void OnStartHover()
    {
        Debug.Log("Has overlapped");
    }


    public void OnIntract()
    {
        Debug.Log("Do Something, Like I Don't Know Spawn A Cube. It's Really Up To You Now");
    }

    public void OnEndHover()
    {
        Debug.Log("Overlapped has ended");
    }
}
