using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D playerRb;
    private GameManager gameManager;
    private LifeManager lifeManager;
    private bool isGround = false;
    private bool isJump = false;
    private bool isHead = false;
    private float jumpPos;
    private float jumpTime,dashTime;

    public float speed;
    public GroundCheck groundChecker;
    public GroundCheck headChecker;
    public float gravity;
    public float jumpSpeed;
    public float jumpLimitHeight;
    public float jumpLimitTime;
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;
    public float beforeInput;
    public GameObject mask;
    public bool canShoot;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGround = groundChecker.IsGround();
        isHead = headChecker.IsGround();

        float horizontalInput = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float verticalInput = Input.GetAxis("Vertical");
        float ySpeed = -gravity;
        //横移動
        if (horizontalInput > 0 && transform.position.x < 8)
        {
            transform.localScale = new Vector2(0.8f, 0.8f);
            playerAnim.SetBool("run", true);
            xSpeed = horizontalInput * speed;
            dashTime += Time.deltaTime;
        } else if (horizontalInput < 0 && transform.position.x > -8)
        {
            transform.localScale = new Vector2(-0.8f, 0.8f);
            playerAnim.SetBool("run", true);
            xSpeed = horizontalInput * speed;
            dashTime += Time.deltaTime;
        }else
        {
            playerAnim.SetBool("run", false);
            xSpeed = 0.0f;
        }
        //ジャンプ
        if (isGround)
        {
            jumpPos = transform.position.y;
            jumpTime = 0.0f;
            if(verticalInput > 0 && jumpTime < jumpLimitTime)
            {
                ySpeed = verticalInput * jumpSpeed;
                isJump = true;
            }else
            {
                isJump = false;
            }
        }else if (isJump)
        {
            if(verticalInput > 0 && jumpPos + jumpLimitHeight > transform.position.y && jumpTime < jumpLimitTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                
            }
            else
            {
                isJump = false;
            }
        }

        if(horizontalInput > 0 && beforeInput < 0)
        {
            dashTime = 0.0f;
        }else if(horizontalInput < 0 && beforeInput > 0)
        {
            dashTime = 0.0f;
        }
        beforeInput = horizontalInput;

        xSpeed *= dashCurve.Evaluate(dashTime);
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        playerAnim.SetBool("jump", isJump); 
        playerAnim.SetBool("Ground", isGround);
        playerRb.velocity = new Vector2(xSpeed, ySpeed);

        if (!gameManager.isPlaying)
        {
            playerAnim.SetBool("lose", true);
        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canShoot)
        {
            ShootMask();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            gameManager.score++;
        }else if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            lifeManager.LifeDamage();
        }
    }

    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        canShoot = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PowerUp")
        {
            canShoot = true;
            Destroy(collision.gameObject);
            StartCoroutine(PowerUpCountDown());
        }
    }
    public void ShootMask()
    {
        Instantiate(mask, transform.position, mask.transform.rotation);
    }
}
