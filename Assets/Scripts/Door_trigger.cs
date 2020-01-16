using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_trigger : MonoBehaviour
{
    public GameObject door;

    public Transform startPos;
    public Transform endPos;
    public float speed = 1f;

    float time = .5f;

    bool isOpened = false;

    void OnTriggerEnter(Collider col)
    {
        if (!isOpened)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            Debug.Log(i);
            door.transform.position = Vector3.Lerp(door.transform.position, endPos.position, i);
            isOpened = true;
            yield return new WaitForEndOfFrame();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (isOpened)
        {
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator CloseDoor()
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            door.transform.position = Vector3.Lerp(door.transform.position, startPos.position, i);
            isOpened = false;
            yield return new WaitForEndOfFrame();
        }
    }
}