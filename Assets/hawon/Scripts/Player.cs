using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : MonoBehaviour
{
    [Header("Coin Score")]
    public Text CoinText; //coin
    [Header("On/Off")]
    public GameObject[] Interaction; // gameobject setting
    [Header("  ")]
    [Header("UIManager")]
    public UIManager UIManager;
    [Header("  ")]
    [Header("Rigidbody")]
    public Rigidbody2D rb;
    [Header("  ")]
    [Header("Move")]
    public float moveSpeed = 20f; // move speed
    private bool inputF;
    bool CoinTF = false;
    int coinnumber = 0;
    Vector2 movement;
    [Header("Tools")]
    public GameObject[] Tools;

    private void Start()
    {
        CoinText.text = coinnumber.ToString();
    }

    private void Update()
    {
        Move();
        Storewindow();
        WeaponTrue();
    }

    private void WeaponTrue()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Tools[0].gameObject.SetActive(true);
        }
    }

    private void CoinTake(int index)
    {
        coinnumber += index;

        CoinText.text = coinnumber.ToString();
    }

    private void Move() //move
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // left right
        movement.y = Input.GetAxisRaw("Vertical"); // forward back

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    } 

    private void Storewindow()
    {
        if(inputF == true)
        {
            Interaction[0].SetActive(true); //merchantInter
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interaction[0].SetActive(false); //merchant
                Interaction[1].SetActive(true); //storewindow
                Interaction[2].SetActive(false); //player
                Interaction[3].SetActive(false); //Coin Text
                UIManager.PotionTrue(); //potionwinow true
            }
        }
        else if(inputF == false)
        {
            Interaction[0].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin") //coin 
        {
            Destroy(collision.gameObject);
            CoinTake(1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "merchant")
        {
            inputF = true;
        }
        else if(collision.gameObject.tag != "merchant")
        {
            inputF = false; //merchantInter
        }
    }

    public void Close()
    {
        Interaction[0].SetActive(true);
        Interaction[1].SetActive(false);
        Interaction[2].SetActive(true);
    }
}
