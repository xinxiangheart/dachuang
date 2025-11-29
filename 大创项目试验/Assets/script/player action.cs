using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float playermovespeed;
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
}
