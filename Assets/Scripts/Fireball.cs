using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private Transform position;

    [SerializeField]
    private GameObject fireball;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float coolDownTime = 0.2f;

    private float nextFireTime = 0;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))

            {
                FireFire();
                nextFireTime = Time.time + coolDownTime;
            }
        }
    }

    private void FireFire()
    {
        GameObject firedBall = Instantiate(fireball, position.position, position.rotation);
        firedBall.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
