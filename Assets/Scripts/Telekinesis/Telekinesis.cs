using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Telekinesis : MonoBehaviour
{
    public Camera cam;
    [SerializeField]
    float interactDist;

    public Transform holdPos;

    [SerializeField]
    float attactspeed;

    [SerializeField]
    float minThrowForce;

    [SerializeField]
    float maxThrowForce;

    float throwForce;

    public GameObject objectIHave;
    private Rigidbody objectRB;

    private Vector3 rotateVector = Vector3.one;

    public bool hasObject = false;

    public bool shouldCharge = false;

    //public PlayerController Player; 

    private void Start()
    {
        throwForce = minThrowForce;
    }


    private void Update()
    {

        if (hasObject)
        {
            if (checkDist() > -.1f)
            {
                moveObjectToPos();
            }

            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit,Mathf.Infinity);

            if (hit.collider.gameObject == objectIHave)
            {
                Debug.Log("Dropped because box was stood on");
                dropObject();
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
    public void dropObject()
    {
        objectRB.constraints = RigidbodyConstraints.None;
        objectRB.useGravity = true;
        objectIHave.transform.parent = null;
        objectIHave = null;
        hasObject = false;
    }

    // Forces the held object forward
    // How far is based on throw force
    public void shootObject()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        objectRB.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        throwForce = minThrowForce;
        dropObject();
    }

    // When called do a ray trace froward and if hits sets object as held object.
    public void doRay()
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
                objectRB.constraints = RigidbodyConstraints.FreezeRotation;
                objectRB.useGravity = false;

                hasObject = true;
            }
        }
    }

    // when called increse the throw force.
    public void updateForce()
    {
        if (shouldCharge)
        {
            throwForce += 1;
            Debug.Log(throwForce);
            Invoke("updateForce", .1f);
        }

    }
}
