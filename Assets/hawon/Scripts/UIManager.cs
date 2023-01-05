using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] Window; //window setting

    public void PotionTrue()
    {
        Window[0].SetActive(true);
    }

    public void Potion() //potion active true
    {
        Window[0].SetActive(true);
        for (int i = 0; i < Window.Length; i++)
        {
            if(i != 0)
            {
                Window[i].SetActive(false);
            }
        }
    }

    public void Weapon() //weapon active true
    {
        Window[1].SetActive(true);
        for (int i = 0; i < Window.Length; i++)
        {
            if (i != 1)
            {
                Window[i].SetActive(false);
            }
        }
    }

    public void Close() //potion/weapon active false
    {
        for (int i = 0; i < Window.Length; i++)
        {
            if (i != -1)
            {
                Window[i].SetActive(false);
            }
        }
    }
}
