using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class P : MonoBehaviour
{
    public float moveSpeed = 5f; // move speed
    public bool inputF;

    public GameObject[] Interaction; // gameobject setting
    public UIManager UIManager;
    public Rigidbody2D rb;

    Vector2 movement;

    void Update()
    {
        Move();
        Storewindow();
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
                UIManager.PotionTrue(); //potionwinow true
            }
        }
        else if(inputF == false)
        {
            Interaction[0].SetActive(false);
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
