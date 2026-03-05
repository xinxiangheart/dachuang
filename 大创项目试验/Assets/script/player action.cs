using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("移动相关")]
    public float playermovespeed;
    public float playerjumpspeed;
    [Header("额外跳跃次数")]
    public float playerjumpcount;
    [Header("跳跃倍率")]
    public float playjumpmultiplier;
    [Header("判断相关")]
    public bool isGround;
    public bool isCrouch;
    public bool presssCrouch;
    [Header("其他组件")]
    public Transform foot;
    public LayerMask Ground;
    public Rigidbody2D playerRB;
    public Animator playAnim;
    [Header("碰撞体相关")]
    public CapsuleCollider2D playerColl;
    public Vector2 playerOffsetVector;
    public Vector2 playerSizeVector;
    void Start()
    {
        playerColl= GetComponent<CapsuleCollider2D>();
        playerRB= GetComponent<Rigidbody2D>();
        playAnim= GetComponent<Animator>();
        playerOffsetVector = new Vector2(playerColl.offset.x, playerColl.offset.y);
        playerSizeVector = new Vector2(playerColl.size.x, playerColl.size.y);
    }

    void Update()
    {
        playerMove();
        playerJump();
        isGroundcheck();
        UpdateCheck();
        playCrouch();
    }
     void isGroundcheck()
    {
      isGround = Physics2D.OverlapCircle(foot.position, 0.1f, Ground);
    }
    void UpdateCheck()
    {
        if (Input.GetButton("Crouch")) {
            presssCrouch = true;
        }
        else
        {
            presssCrouch= false; 
        }
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
            playerjumpcount--;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerjumpspeed);
            playAnim.SetBool("jump", true);
            playAnim.SetBool("jumpbegin", true);
        }
       
        if (isGround)
        {
            playerjumpcount = 1;
            //二段跳就把playjumpcount设定为1
            playAnim.SetBool("jump", false);
            playAnim.SetBool("jumpdoublejump", false);
        }
        if (!isGround)
        {
            playAnim.SetBool("jump", true);
            playAnim.SetBool("jumpbegin", false);
        }
        if (Input.GetButtonDown("Jump") && playerjumpcount > 0&&!isGround) {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerjumpspeed);
            playerjumpcount--;
            playAnim.SetBool("jumpdoublejump", true);
        }

    }
    void playCrouch()
    {
        if (presssCrouch && isGround)
        {
            isCrouch = true;
            playerColl.size = new Vector2(playerSizeVector.x, playerSizeVector.y * 0.5f);
            playerColl.offset = new Vector2(playerOffsetVector.x, playerOffsetVector.y * 0.5f);
            playermovespeed = 4;
        }
        else
        {
            isCrouch = false;
            playerColl.size = new Vector2(playerSizeVector.x, playerSizeVector.y);
            playerColl.offset = new Vector2(playerOffsetVector.x, playerOffsetVector.y);
            playermovespeed = 8;
        }
    }
}
