using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int HP = 10;
    public float moveSpeed = .1f;
    public int moveCount = 4;
    public string direction;
    bool canRotate = true;

    //1 = Movement Phase, 2 = Attack Phase
    private int phaseNumb = 1;

    public Animator playerAnim;
    public SpriteRenderer playerRend;
    public Transform movePoint;
    public Canvas UI;
    public GameObject weaponPrefab;

    public LayerMask colliders;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        playerAnim = GetComponentInChildren<Animator>();
        playerRend = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Makes player sprite always move towards the target point, which is moved below vvv
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed);

        //These four ifs are for animation
        if (canRotate)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                playerAnim.Play("Side");
                playerRend.flipX = true;
                direction = "Right";
            }
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                playerAnim.Play("Side");
                playerRend.flipX = false;
                direction = "Left";
            }
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                playerAnim.Play("Backward");
                direction = "Up";
            }
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                playerAnim.Play("Forward");
                direction = "Down";
            }
        }

        //MOVEMENT PHASE
        if (phaseNumb == 1)
        {
            if (Vector3.Distance(transform.position, movePoint.position) <= .02f)
            {
                //For Horizontal Movement (A/D)
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && moveCount != 0)
                {
                    //This if is for checking the tile infront of the direction and moving there
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .1f, colliders))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        moveCount--;
                    }
                }
                //For Vertical Movement (W/S)
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 && moveCount != 0)
                {
                    //This if is for checking the tile infront of the direction and moving there
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .1f, colliders))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        moveCount--;
                    }
                }
            }

            if (moveCount == 0)
            {
                phaseNumb = 2;
            }
        }

        //ATTACK PHASE
        if (phaseNumb == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float rotation = 0;
                if (direction == "Up") rotation = -90;
                if (direction == "Down") rotation = 90;
                if (direction == "Left") rotation = 0;
                if (direction == "Right") rotation = 180;
                canRotate = false;
                Instantiate(weaponPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, rotation));
                //After you attack, its the enemy's turn. Put reference to enemy turn here.
                //For right now, we'll just make it our turn again.
                phaseNumb = 1;
                moveCount = 4;
                canRotate = true;
            }
        }
    }

    public void TakeDamage(int x)
    {
        HP -= x;
        UI.GetComponentInChildren<Health>().SetHealth(HP);
    }
}
