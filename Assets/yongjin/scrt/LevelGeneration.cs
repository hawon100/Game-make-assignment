using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    public Transform[] startingPositions;

    [Header("index 0 = LR, index 1 = LRB, index 2 = LRT, index 3 = LRBT")] //안 눌러도 보임
    //[Tooltip("index 0 = 좌 우 구멍, index 1 = 좌 우 아래 구멍")] 눌렀을때 보임
    public GameObject[] rooms;

    public float moveAmount;

    private int direction;
    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    private int downConuter;

    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration;

    public LayerMask room;
    
    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false) {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2)
        { // 오른쪽으로 갈까?
            if (transform.position.x < maxX)
            {
                downConuter = 0;

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                } else if (direction == 4)
                {
                    direction = 5;
                }
            } else
            {
                direction = 5;
            }
            
        } else if (direction == 3 || direction == 4) 
        { // 왼쪽으로 갈까?
            if (transform.position.x > minX)
            {
                downConuter = 0;

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            } else
            {
                direction = 5;
            }
            
        } else if (direction == 5)
        { // 아래로 갈까?

            downConuter++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downConuter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    } else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            } else
            { // 멈춰!
                stopGeneration = true;
            }
            
        }

    }

}
