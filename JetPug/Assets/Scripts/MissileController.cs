using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private SpriteRenderer spriteRndr;
    private BoxCollider2D boxCollider;
    public GameObject missileWarning;
    public GameObject player;
    bool missilewarning = false;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRndr = this.GetComponent<SpriteRenderer>();
        this.spriteRndr.enabled = false;
        this.boxCollider = this.GetComponent<BoxCollider2D>();
        this.boxCollider.enabled = false;
        player = GameObject.Find("Player");
        Invoke("MissileStartEffect", 0);
        Invoke("Warningstop", 2);
        Invoke("MissileStopEffect", 5);
        missileWarning.transform.position=new Vector2(this.transform.position.x,this.transform.position.y);
        missileWarning.gameObject.SetActive(true);
    }
    void MissileStartEffect()
    {
        if (PlayerController.IsObstacleOn)
        {
            if (missileWarning.GetComponent<SpriteRenderer>().enabled)
                missileWarning.GetComponent<SpriteRenderer>().enabled = false;
            else
                missileWarning.GetComponent<SpriteRenderer>().enabled = true;
            Invoke("MissileStartEffect", 0.1f);
        }
    }
    void Warningstop()
    {
        missilewarning = true;
        this.spriteRndr.enabled = true;
        this.boxCollider.enabled = true;
        transform.position = missileWarning.transform.position; 
        missileWarning.gameObject.SetActive(false);

    }
    void MissileStopEffect()
    {
        Invoke("MakeObstacleOn", 2f);
        missilewarning = false;
        
    }
    void MakeObstacleOn()
    {
        PlayerController.IsObstacleOn = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (missileWarning)
            transform.Translate(-Vector2.right * 8.0f*Time.deltaTime);
    }
}
