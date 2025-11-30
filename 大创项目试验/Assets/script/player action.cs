using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float playermovespeed;
    public float playerjumpspeed;
    public bool isGround;
    public Transform foot;
    public LayerMask Ground;
    public Rigidbody2D playerRB;
    public Collider2D playerColl;
    public Animator playAnim;
    void Start()
    {
        playerColl= GetComponent<Collider2D>();
        playerRB= GetComponent<Rigidbody2D>();
        playAnim= GetComponent<Animator>();
    }

    
    void Update()
    {
        playerMove();
        playerJump();
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, Ground);
    }
     
    void playerMove()
    {
        float horizontalNum  =  Input.GetAxis("Horizontal");
        float facenum = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(playermovespeed * horizontalNum,playerRB.velocity.y);
        playAnim.SetFloat("run",Mathf.Abs( playermovespeed * horizontalNum));
        if (facenum != 0)
        {
            transform.localScale = new Vector3(3*facenum, transform.localScale.y, transform.localScale.z);
        }
    }
    void playerJump()
    {
        if (Input.GetButtonDown("Jump")&&isGround)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerjumpspeed);
            playAnim.SetBool("jump", true);
            playAnim.SetBool("jumpbegin", true);
        }
        if (isGround)
        {
            playAnim.SetBool("jump", false);
        }
        if (!isGround)
        {
            playAnim.SetBool("jump", true);
            playAnim.SetBool("jumpbegin", false);
        }
    }
}
