using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporter : MonoBehaviour
{
    public CharacterController Player;
    public Transform Reciever;

    bool PlayerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsOverlapping) {
            Vector3 portalToPlayer = Player.transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if(dotProduct < 0f) {
                float rotationDiff = Quaternion.Angle(transform.rotation, Reciever.rotation);
                rotationDiff += 180;
                Player.transform.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                Player.enabled = false;
                Player.transform.position = Reciever.position + positionOffset;
                Player.enabled = true;

                PlayerIsOverlapping = false;

            }

        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            PlayerIsOverlapping = true;
        }
    }

    void onTriggerExit(Collider other) {
        if (other.tag == "Player") {
            PlayerIsOverlapping = false;
        }
    }
}
