using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour, IIntractable
{
    public float MaxRange { get { return maxRange;  } }

    private const float maxRange = 4f;

    [SerializeField] private GameObject spawneObject;
    [SerializeField] private Transform spawnPos;

    public Canvas shownText;

    private GameObject currentCube;

    public void OnStartHover()
    {
        shownText.enabled = true;
    }


    public void OnIntract()
    {
        if (currentCube == null)
        {
            currentCube = Instantiate(spawneObject, spawnPos.position, Quaternion.identity);
        }
        else if (currentCube != null)
        {
            Destroy(currentCube);
            currentCube = Instantiate(spawneObject, spawnPos.position, Quaternion.identity);
        }
    }

    public void OnEndHover()
    {
        shownText.enabled = false;
    }
}
