using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	
	
	public float moveSpeed = 3f;
	public float jumpPower = 3f;
	//
	//public float jumpGauge = 0;
	bool isJump = false;
	bool isOnGround = true;

    private Animator anim;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSprite;

	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}

	private void move()
	{
        /*
		Vector3 moveVelocity = Vector3.zero;
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            playerSprite.flipX = true;
            anim.SetBool("IsMoving", true);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            playerSprite.flipX = false;
            anim.SetBool("IsMoving", true);
        }
        //no movinf
        else
            anim.SetBool("IsMoving", false);

		transform.position += moveVelocity * moveSpeed * Time.deltaTime;
        */
        Vector3 moveVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        if(moveVelocity.x == 0)
            anim.SetBool("IsMoving", false);
        else if(moveVelocity.x > 0)
        {
            playerSprite.flipX = false;
            anim.SetBool("IsMoving", true);
        }
        else
        {
            playerSprite.flipX = true;
            anim.SetBool("IsMoving", true);
        }
            
        transform.position += moveVelocity * moveSpeed * Time.deltaTime;


    }

	void jump()
	{
		if (!isJump)
			return;

		playerRigidbody.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2(0, jumpPower);
		playerRigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
        

		isJump = false;
		isOnGround = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//그라운드 외에 다른 장애물에 대한 판정 추가 필요
		if (collision.tag == "ground")
		{
			isOnGround = true;
		}

		//적 밟아 죽이는 판정 추가
		if(collision.tag == "enemy")
		{
			float killVelocity = jumpPower / 2;

			//jump more
			playerRigidbody.velocity = Vector2.zero;
			playerRigidbody.AddForce(new Vector2(0,killVelocity), ForceMode2D.Impulse);


			/* TODO: jumpgauge implement
			 
			if (Input.GetKey(KeyCode.Space))
			{
				jump
				Debug.Log();

			}*/


			//jump timing!
			//enemy dead situation
		}
	}

	private void FixedUpdate()
	{
		move();
		jump();

	}

	


	void Update () {



		if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
		{
			isJump = true;

		}

        if (playerRigidbody.velocity.y == 0)
            anim.SetBool("IsGrounded", true);
        else
            anim.SetBool("IsGrounded", false);

        anim.SetFloat("YVeloCity", playerRigidbody.velocity.y);
        Debug.Log(playerRigidbody.velocity.y);
    }
}
