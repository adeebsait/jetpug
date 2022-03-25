﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections.Generic;
using System;

public class MouseController : MonoBehaviour
{
    public float jetpackSpeed = 75f;
    public float forwardMovementSpeed = 10.0f;

    public ParticleSystem ps;
    public Animator explosion;
    public GameObject groundChecker;
    public LayerMask layerMask;
    public Texture2D coinTexture;

    public AudioClip coinsSound;
    public AudioClip laserSound;

    public AudioSource footstepsAudioSource;
    public AudioSource jetpackAudioSource;

    private Rigidbody2D rb;
    private Animator animator;

    private bool grounded;
    private bool dead;

    private int coins = 0;
    private int tcoins = 0;

    public Text CoinText;
    public Text DistText;
    public GameObject GameoverPanel;
    public Text GameoverCoin;
    public Text GameoverDist;

    Vector3 startPoint;
    public static bool isBubbleOn = false;
    public static bool isSpeedDashOn = false;
    public static bool isMagnetOn = false;
    public GameObject protectiveLayer;
    float powerupTime = 10.0f;
    long meters;

    public List<GameObject> Objects;
    GameObject Obstacle1;
    GameObject Obstacle2;
    GameObject Obstacle3;
    GameObject Obstacle4;
    public static bool IsObstacleOn = false;
    public Sprite Invisible1Sprite;
    public Sprite Invisible2Sprite;
    private SpriteRenderer spriteRndr;

    public float interval = 0.5f;
    private bool InvisibleOn;
    private float timeSinceStart;
    public static bool gameOver = false;
    public void Start()
    {
        gameOver = false;
        tcoins = PlayerPrefs.GetInt("Coin");
        coins = 0;
        this.spriteRndr = this.GetComponent<SpriteRenderer>();

        this.rb = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        startPoint = transform.position;
        //print(isMagnetOn + "=isMagnetOn");
        Invoke("AddObstacles", UnityEngine.Random.Range(12, 16));

        //Invoke("AddObstacles", UnityEngine.Random.Range(8, 12));

    }
    void AddObstacles()
    {
        if (!this.dead)
        {
            int randomObjIndex = UnityEngine.Random.Range(0,this.Objects.Count);
            //Obstacle = Instantiate(this.Objects[randomObjIndex]);
            Obstacle1 = Obstacle2 = Obstacle3 = Obstacle4 = null;
            //randomObjIndex = 3;
            switch (randomObjIndex)
            {
                case 0:
                    Obstacle1 = Instantiate(this.Objects[0]);
                    Obstacle1.transform.position = new Vector2(this.transform.position.x + 3.0f, -2.31f);
                    IsObstacleOn = true;
                    break;
                case 1:
                    Obstacle2 = Instantiate(this.Objects[1]);
                    Obstacle2.transform.position = new Vector2(this.transform.position.x + 3.0f, -2.3f);
                    IsObstacleOn = true;

                    break;
                case 2:
                    Obstacle3 = Instantiate(this.Objects[2]);
                    Obstacle3.transform.position = new Vector2(this.transform.position.x + 3.0f, 2.73f);
                    IsObstacleOn = true;

                    break;
                case 3:
                    Obstacle4 = Instantiate(this.Objects[3]);
                    Obstacle4.transform.position = new Vector2(this.transform.position.x + 8f, Obstacle4.transform.position.y);
                    IsObstacleOn = true;

                    break;

            }
            //Invoke("AddObstacles", UnityEngine.Random.Range(8, 12));

            Invoke("AddObstacles", UnityEngine.Random.Range(12, 16));
        }
    }
    public void FixedUpdate()
    {
        var jetpackActive = Input.GetButton("Jump") && !dead;

        //var jetpackActive = Input.GetButton("Fire1") && !dead;
        this.CheckIfOnGround();
        this.AdjustJetpack(jetpackActive);

        if (jetpackActive)
        {
                this.rb.AddForce(new Vector2(0, this.jetpackSpeed));
                //rb.AddForce(Vector2.up * 2 ,ForceMode2D.Impulse);
                //rb.velocity = new Vector2(rb.velocity.x, 5.0f);
                //this.rb.AddForce(,force)
        }
        if (!this.dead)
        {
            var velocity = this.rb.velocity;
            velocity.x = this.forwardMovementSpeed;
            this.rb.velocity = velocity;
        }
        if(Obstacle1!=null)
            Obstacle1.transform.position = new Vector2(this.transform.position.x +3.0f,-2.31f);
       else if (Obstacle2 != null)
            Obstacle2.transform.position = new Vector2(this.transform.position.x + 3.0f, -2.3f);
        else if (Obstacle3 != null)
            Obstacle3.transform.position = new Vector2(this.transform.position.x + 3.0f, 2.73f);
        else if (Obstacle4 != null && !Obstacle4.GetComponent<SpriteRenderer>().enabled)
            Obstacle4.transform.position = new Vector2(this.transform.position.x + 8f, Obstacle4.transform.position.y);

        this.forwardMovementSpeed += 0.001f;

        if (isSpeedDashOn )
        {
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 2.41f), 10 * Time.deltaTime);

            animator.enabled = false;
            this.timeSinceStart += Time.fixedDeltaTime;
            if (this.timeSinceStart > this.interval)
            {
                setProtectiveLayerPosition();
                this.InvisibleOn = !this.InvisibleOn;
                this.timeSinceStart = 0;

                this.spriteRndr.sprite = this.InvisibleOn ? this.Invisible1Sprite : this.Invisible2Sprite;
            }
            this.interval -= 0.001f;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Coin"))
        {
            this.tcoins++;
            this.coins++;
            PlayerPrefs.SetInt("Coin", this.tcoins);
            AudioSource.PlayClipAtPoint(this.coinsSound, this.transform.position);
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("Magnet"))
        {
            if (!this.dead)
            {
                isMagnetOn = true;
                collider.gameObject.SetActive(false);
                Invoke("MagnetDeActivate", powerupTime);
            }
        }
        else if (collider.CompareTag("Bubble"))
        {
            if (!this.dead)
            {
                collider.gameObject.SetActive(false);
                isBubbleOn = true;
                protectiveLayer.gameObject.SetActive(true);
                protectiveLayer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("all-blue/bubble_10");
                protectiveLayer.gameObject.transform.localPosition = new Vector3(0.08f, protectiveLayer.gameObject.transform.localPosition.y, protectiveLayer.transform.localPosition.z);
            }

        }
        else if (collider.CompareTag("speeddash"))
        {
            if (!this.dead)
            {
                isSpeedDashOn = true;
                this.forwardMovementSpeed = this.forwardMovementSpeed + (this.forwardMovementSpeed / 3);
                //this.transform.position = new Vector2(this.transform.position.x,2.41f);
                animator.enabled = false;
                this.spriteRndr.sprite = this.InvisibleOn ? this.Invisible1Sprite : this.Invisible2Sprite;
                //this.rb.gravityScale = 0;
                //this.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                collider.gameObject.SetActive(false);
                Invoke("JetpackSpeedSlow", powerupTime);
            }
           
        }
        else
        {
            if (collider.gameObject.name == "Missle(Clone)")
            {
                explosion.gameObject.SetActive(true);
                explosion.enabled = true;
                Animator   anim=explosion.GetComponent<Animator>();
                anim.Play("Explosion");
                explosion.gameObject.transform.position = collider.gameObject.transform.position;
                Destroy(collider.gameObject);
                    
                Invoke("ExplosionParticleStop", 1);
            }

            //print("ISbubbleON=" + isBubbleOn);
            if (!isBubbleOn && !isSpeedDashOn)
            {
                if (!this.dead)
                {
                    this.dead = true;
                    AudioSource.PlayClipAtPoint(this.laserSound, this.transform.position);
                    this.animator.SetBool("Dead", true);
                }
            }
            else if (isBubbleOn && !isSpeedDashOn)
            {
                                //print(protectiveLayer.GetComponent<SpriteRenderer>().sprite.name);
                if (protectiveLayer.GetComponent<SpriteRenderer>().sprite.name == "bubble_10")
                {
                    protectiveLayer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("all-blue/bubble_7");
                }
                else if (protectiveLayer.GetComponent<SpriteRenderer>().sprite.name == "bubble_7")
                {
                    protectiveLayer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("all-blue/bubble_4");

                }
                else if (protectiveLayer.GetComponent<SpriteRenderer>().sprite.name == "bubble_4")
                {
                    protectiveLayer.gameObject.SetActive(false);
                    isBubbleOn = false;
                }
            }
            else if (isSpeedDashOn && this.rb.gravityScale==0.0f)
            {
                //this.transform.position = new Vector2(this.transform.position.x, 2.41f);
            }
        }
    }
    private void ExplosionParticleStop()
    {
        explosion.gameObject.SetActive(false);

    }
    private void JetpackSpeedSlow()
    {
        //this.rb.gravityScale = 1;
        //this.rb.constraints = RigidbodyConstraints2D.None;
        //this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        protectiveLayer.gameObject.transform.localPosition = new Vector3(0.08f, protectiveLayer.gameObject.transform.localPosition.y, protectiveLayer.transform.localPosition.z);
        this.forwardMovementSpeed = 5.0f;
        animator.enabled = true;
        isSpeedDashOn = false;
        //Invoke("IsspeedOff", 2);
    }
    private void setProtectiveLayerPosition()
    {
        if (isBubbleOn)
            protectiveLayer.gameObject.transform.localPosition = new Vector3(1.31f, protectiveLayer.gameObject.transform.localPosition.y, protectiveLayer.transform.localPosition.z);
        else
            protectiveLayer.gameObject.transform.localPosition = new Vector3(0.08f, protectiveLayer.gameObject.transform.localPosition.y, protectiveLayer.transform.localPosition.z);
    }

    void IsspeedOff()
    {
        isSpeedDashOn = false;
    }
    public void BubbleDeactivate()
    {
        isBubbleOn = false;
        protectiveLayer.gameObject.SetActive(false);
    }
    private void MagnetDeActivate()
    {
        isMagnetOn = false;
        print(isMagnetOn + "=isMagnetOn");
    }
    private void Update()
    {
        CoinText.text = this.coins.ToString();
        meters = Convert.ToInt64(this.transform.position.x - startPoint.x);
        DistText.text = meters.ToString()+"m";
        DisplayRestartButton();
    }
    private void DisplayRestartButton()
    {
        if (this.dead && this.grounded)
        {
            isMagnetOn = false;
            isSpeedDashOn = false;
            isBubbleOn = false;
            IsObstacleOn = false;
            CancelInvoke();
            gameOver = true;
            explosion.gameObject.SetActive(false);
            GameoverPanel.SetActive(true);
            GameoverCoin.text = "Coin:" +this.coins.ToString();
            GameoverDist.text = "Distance:" +DistText.text;
        }
    }
    public void RestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
    private void AdjustJetpack(bool jetpackActive)
    {
        this.ps.enableEmission = !this.grounded;
        this.ps.emissionRate = jetpackActive ? 300 : 100;
        this.jetpackAudioSource.enabled = jetpackActive;
    }
    private void CheckIfOnGround()
    {
       var colliding = Physics2D.OverlapCircle(this.groundChecker.transform.position, 0.1f, this.layerMask);
       this.grounded = colliding == null ? false : true;
       this.animator.SetBool("Grounded", this.grounded);
       this.footstepsAudioSource.enabled = this.grounded;
    }
}