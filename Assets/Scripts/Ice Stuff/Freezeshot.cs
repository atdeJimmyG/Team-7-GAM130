using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//This script handles the launching of freezeshots
public class Freezeshot : MonoBehaviour
{
    //Allows the start position of freezeshots to be selected via the Unity Editor
    [SerializeField]
    private Transform position;

    //This allows the freezeshot prefab to be used as a variable in the script after attachment via the Unity Editor
    [SerializeField]
    private GameObject freezeshot;

    //"Speed" is how fast the freezeshot will travel
    [SerializeField]
    private float speed = 5f;

    //"coolDownTime" is the minimum time between the firing of each freezeshot
    [SerializeField]
    private float coolDownTime = 1f;

    //"nextFireTime" is the time since the script began running when the next freezeshot can be launched
    private float nextFireTime = 0;

    // pause menu ref used to then check if the player is in the UI
    public PlayerController player;

    bool testing = true;

    void Update()
    {
        
        bool inMenu = player.inUI;
        //The script contantly keeps track of when freezeshots are launched, keeping a minimum amount of time between each
        if (Time.time > nextFireTime)
        {
            // Checks what state UImode is. This then either enables the spell or disables it based on its results.               
            if (!inMenu)
            {
                testing = true;
            }
   
        }
        
    }

    public void FireTest()
    {
        if (testing)
        {
            FireIce();
            nextFireTime = Time.time + coolDownTime;
            testing = false;
        }
    }

    //"FireFire" is the part that actually creates and propels the freezeshot
    private void FireIce()
    {
        Debug.Log("YEEEEEEEEEEEEEEET");
        GameObject firedShot = Instantiate(freezeshot, position.position, position.rotation);
        firedShot.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
    }

}