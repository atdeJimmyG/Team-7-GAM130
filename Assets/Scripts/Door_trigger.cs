using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_trigger : MonoBehaviour
{
    public Animator doorAnimationController;
    public float animSpeed = 1f;
    public bool oneUse = false;

    // You should only need to change this if you have a differnt parameter name in your animatior
    public string stateParameterName = "OpenDoor";
    public string speedParameterName = "AnimSpeed";

    bool isOpened = false;

    // On starts sets the animation play rate to desired speed
    private void Start()
    {
        doorAnimationController.SetFloat(speedParameterName, animSpeed);
    }


    // When something enters the collider plays the open animation
    void OnTriggerEnter(Collider col)
    {
        if (!isOpened)
        {
            isOpened = true;
            doorAnimationController.SetBool(stateParameterName, true);
        }
    }


    // When something leaves the collider plays the close animation
    void OnTriggerExit(Collider col)
    {
        if (isOpened && !oneUse)
        {
            isOpened = false;
            doorAnimationController.SetBool(stateParameterName, false);
        }
    }
}