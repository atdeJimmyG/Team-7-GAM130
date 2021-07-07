using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialMenu : MonoBehaviour
{
    public GameObject Player;
    public List<MenuButton> Buttons = new List<MenuButton>();
    public Vector2 MousePos;
    private Vector2 FromVector2 = new Vector2(.5f, 1f);
    private Vector2 CenterCircle = new Vector2(.5f, .5f);
    private Vector2 ToVector;

    public int MenuItems;
    public int CurrentMenuItem;
    private int OldMenuItem;
    public AnimationCurve curve;
    public bool open = false;
    private List<string> Spells = new List<string>();
    private Telekinesis Telekinesis;
    private Fireball Fire;
    public string CurrentSpell;

    private void Start()
    {
        Telekinesis = Player.GetComponent<Telekinesis>();
        Fire = Player.GetComponent<Fireball>();
    }

    public void Open()
    {
        open = true;
        MenuItems = Buttons.Count;      
        foreach(MenuButton button in Buttons)
        {  
            StartCoroutine(fadeIn(button.SenceImage, button.NormalColor));
            Spells.Add(button.Spell);
        }
        CurrentMenuItem = 0;
        OldMenuItem = 0;     
    }

    public void Close()
    {
        open = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            GetCurrentMenuItem();
            CurrentSpell = Spells[CurrentMenuItem];
            //if (Input.GetMouseButtonDown(0))
            //{
            //    ButtonAction(CurrentSpell);
            //}
        }       
    }

    public void GetCurrentMenuItem()
    {
        //MousePos.x = MousePos.x - (Screen.width / 2f);
        //MousePos.y = MousePos.y - (Screen.height / 2f);
        //MousePos.Normalize();

        if(MousePos != Vector2.zero)
        {
            float angle = Mathf.Atan2(MousePos.y, -MousePos.x) / Mathf.PI;
            angle *= 180;
            //angle -= 90f;
            if(angle < 0)
            {
                angle += 360;
            }

            CurrentMenuItem = (int)(angle / (360 / MenuItems));

            if (CurrentMenuItem != OldMenuItem)
            {
                Buttons[OldMenuItem].SenceImage.color = Buttons[OldMenuItem].NormalColor;
                OldMenuItem = CurrentMenuItem;
                Buttons[CurrentMenuItem].SenceImage.color = Buttons[CurrentMenuItem].HighlightedColor;
            }
        }
    }

    public void ButtonAction(string CurrentSpell)
    {
        Buttons[CurrentMenuItem].SenceImage.color = Buttons[CurrentMenuItem].PressedColor;
        //Debug.Log(CurrentMenuItem);
        //Debug.Log(CurrentSpell);
        if (CurrentMenuItem == 0 && CurrentSpell == "Telekinesis" && Player.GetComponent<Telekinesis>() != null)
        {
            if (Telekinesis.enabled)
            {
                Debug.Log("Current Spell Is Telekinesis");
            }
            else
            {
                Debug.Log("Telekinesis is now you current spell");
                Fire.enabled = false;
                Telekinesis.enabled = true;
            }
        }
        if((CurrentMenuItem == 1 && CurrentSpell == "Fireball" && Player.GetComponent<Telekinesis>() == null))
        {
            Debug.Log("You Don't Have Telekinesis");
        }
        if (CurrentMenuItem == 1 && CurrentSpell == "Fireball" && Player.GetComponent<Fireball>() != null)
        {
            if (Fire.enabled)
            {
                Debug.Log("Current Spell Is fire");
            }
            else
            {
                Debug.Log("Fire is now you current spell");
                Telekinesis.enabled = false;
                Fire.enabled = true;
            }
        }
        if (CurrentMenuItem == 1 && CurrentSpell == "Fireball" && Player.GetComponent<Fireball>() == null)
        {
            Debug.Log("You Don't Have Fire");
        }
    }

    IEnumerator fadeIn(Image img, Color NormalColor)
    {
        float t = 0f;

        while (t < .1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            NormalColor.a = a;
            img.color = NormalColor;
            yield return 0;
        }
    }
}

[System.Serializable]
public class MenuButton
{
    public string Name;
    public Image SenceImage;
    public Color NormalColor = Color.white;
    public Color HighlightedColor = Color.gray;
    public Color PressedColor = Color.gray;
    public string Spell;
}


//MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
//ToVector = new Vector2(MousePos.x / Screen.width, MousePos.y / Screen.height);
//float angle = (Mathf.Atan2(FromVector2.y - CenterCircle.y, FromVector2.x - CenterCircle.x) - Mathf.Atan2(ToVector.y - CenterCircle.y, ToVector.x - CenterCircle.x)) * Mathf.Rad2Deg;

//        if (angle< 0)
//            angle += 360;

//        CurrentMenuItem = (int) (angle / (360 / MenuItems));

//        if (CurrentMenuItem != OldMenuItem)
//        {
//            Buttons[OldMenuItem].SenceImage.color = Buttons[OldMenuItem].NormalColor;
//            OldMenuItem = CurrentMenuItem;
//            Buttons[CurrentMenuItem].SenceImage.color = Buttons[CurrentMenuItem].HighlightedColor;
//        }