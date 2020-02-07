using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Telekinesis : MonoBehaviour
{
    public Camera cam;
    public float interactDist;

    public Transform holdPos;
    public float attactspeed;

    public float minThrowForce;
    public float maxThrowForce;
    private float throwForce;

    private GameObject objectIHave;
    private Rigidbody objectRB;

    private Vector3 rotateVector = Vector3.one;

    private bool hasObject = false;

    private bool shouldCharge = false;

    public FirstPersonController Player; 

    private void Start()
    {
        throwForce = minThrowForce;
    }


    private void Update()
    {
        bool InMenu = Player.UImode;
        
        if (!InMenu)
        {
            if (Input.GetMouseButtonDown(0) && !hasObject)
            {
                doRay();
            }

            if (Input.GetMouseButtonDown(1) && hasObject)
            {
                shouldCharge = true;
                updateForce();
            }

            if (Input.GetMouseButtonUp(1) && hasObject)
            {
                shouldCharge = false;
                shootObject();
            }

            if (Input.GetKeyDown(KeyCode.E) && hasObject)
            {
                dropObject();
            }

            if (hasObject)
            {
                if (checkDist() > -1f)
                {
                    moveObjectToPos();
                }
            }
        }        
    }

    //---------Fuction

    public float checkDist()
    {
        float dist = Vector3.Distance(objectIHave.transform.position, holdPos.transform.position);
        return dist;
    }

    private void moveObjectToPos()
    {
        objectIHave.transform.position = Vector3.Lerp(objectIHave.transform.position, holdPos.position, attactspeed * Time.deltaTime);
    }

    // Drops the held object
    private void dropObject()
    {
        objectRB.constraints = RigidbodyConstraints.None;
        objectRB.useGravity = true;
        objectIHave.transform.parent = null;
        objectIHave = null;
        hasObject = false;
    }

    // Forces the held object forward
    // How far is based on throw force
    private void shootObject()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        objectRB.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        throwForce = minThrowForce;
        dropObject();
    }

    // When called do a ray trace froward and if hits sets object as held object.
    private void doRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDist))
        {
            if (hit.collider.CompareTag("Block"))
            {
                objectIHave = hit.collider.gameObject;
                //objectIHave.transform.SetParent(holdPos);

                objectRB = objectIHave.GetComponent<Rigidbody>();
                objectRB.constraints = RigidbodyConstraints.FreezeRotation;
                objectRB.useGravity = false;

                hasObject = true;
            }
        }
    }

    // when called increse the throw force.
    private void updateForce()
    {
        if (shouldCharge)
        {
            throwForce += 1;
            Debug.Log(throwForce);
            Invoke("updateForce", .1f);
        }

    }
}
