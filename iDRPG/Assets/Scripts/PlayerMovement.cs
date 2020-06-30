using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = .1f;
    public int moveCount = 4;
    private bool attackPhaseCaller = false;
    public string direction;

    public Animator playerAnim;
    public SpriteRenderer playerRend;
    public Transform movePoint;

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

        if (Vector3.Distance(transform.position, movePoint.position) <= .02f)
        {
            //These four ifs are for animation
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

        if (moveCount == 0 && attackPhaseCaller == false)
        {
            AttackPhase();
            attackPhaseCaller = true;
        }
    }

    void AttackPhase()
    {
        if (Input.GetAxisRaw("AttackSubmit") == 1)
        {
            //Attack command here as well as animation
            //After you attack, its the enemy's turn. Put reference to enemy turn here.
            //For right now, we'll just make it our turn again.
            attackPhaseCaller = false;
            moveCount = 4;
        }
    }
}
