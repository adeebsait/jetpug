using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : MonoBehaviour
{
    private SpriteRenderer spriteRndr;
    private BoxCollider2D boxCollider;
    public GameObject missileWarning;
    bool missilewarning = false;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRndr = this.GetComponent<SpriteRenderer>();
        this.spriteRndr.enabled = false;

        this.boxCollider = this.GetComponent<BoxCollider2D>();
        this.boxCollider.enabled = false;
        Invoke("MissileStartEffect", 0);
        Invoke("Warningstop", 2);
        Invoke("MissileStopEffect", 5);
        missileWarning.gameObject.SetActive(true);

    }
    void MissileStartEffect()
    {
        if (MouseController.IsObstacleOn)
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
        //MouseController.IsObstacleOn = false;
        missilewarning = false;
        
    }
    void MakeObstacleOn()
    {
        MouseController.IsObstacleOn = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //this.transform.Translate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (missileWarning)
            transform.Translate(-Vector2.right * 8.0f*Time.deltaTime);
    }
}