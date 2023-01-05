using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // move speed
    private bool inputF;

    public GameObject[] Interaction; // gameobject setting
    public UIManager UIManager;
    public Rigidbody2D rb;
    public UI_Inventory UI_Inventory;

    Vector2 movement;

    private Inventory inventory;

    private void Start()
    {
        inventory = new Inventory();
        UI_Inventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(-20, 20), new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, -20), new Item { itemType = Item.ItemType.Coin , amount = 1 });
    }

    private void Update()
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
