using UnityEngine;

//This script handles the launching of fireballs
public class Fireball : MonoBehaviour
{
    //Allows the start position of fireballs to be selected via the Unity Editor
    [SerializeField]
    private Transform position;

    //This allows the fireball prefab to be used as a variable in the script after attachment via the Unity Editor
    [SerializeField]
    private GameObject fireball;

    //"Speed" is how fast the fireball will travel
    [SerializeField]
    private float speed = 5f;

    //"coolDownTime" is the minimum time between the firing of each fireball
    [SerializeField]
    private float coolDownTime = 1f;

    //"nextFireTime" is the time since the script began running when the next fireball can be launched
    private float nextFireTime = 0;

    void Update()
    {
        //The script contantly keeps track of when fireballs are launched, keeping a minimum amount of time between each
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))

            {
                FireFire();
                nextFireTime = Time.time + coolDownTime;
            }
        }
    }

    //"FireFire" is the part that actually creates and propels the fireball
    private void FireFire()
    {
        GameObject firedBall = Instantiate(fireball, position.position, position.rotation);
        firedBall.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
    }

}