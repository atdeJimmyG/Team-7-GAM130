using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
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



    private void Start()
    {
        throwForce = minThrowForce;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasObject)
        {
            doRay();
        }

        if (Input.GetMouseButtonDown(1) && hasObject)
        {
            throwForce += 10f;
            Debug.Log(throwForce);
        }

        if (Input.GetMouseButtonUp(1) && hasObject)
        {
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

    private void dropObject()
    {
        objectRB.constraints = RigidbodyConstraints.None;
        objectIHave.transform.parent = null;
        objectIHave = null;
        hasObject = false;
    }

    private void shootObject()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        objectRB.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        throwForce = minThrowForce;
        dropObject();
    }

    private void doRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDist))
        {
            if (hit.collider.CompareTag("Block"))
            {
                objectIHave = hit.collider.gameObject;
                objectIHave.transform.SetParent(holdPos);

                objectRB = objectIHave.GetComponent<Rigidbody>();
                objectRB.constraints = RigidbodyConstraints.FreezeAll;

                hasObject = true;
            }
        }
    }
}
