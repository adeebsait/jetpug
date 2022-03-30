using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Sprite laserOnSprite;
    public Sprite laserOffSprite;

    public float interval = 0.5f;
    public float rotationSpeed = 50.0f;

    private SpriteRenderer spriteRndr;
    private BoxCollider2D boxCollider;

    private float timeSinceStart;
    private bool laserOn;
    int counter = 0;

    public void Start()
    {
        this.spriteRndr = this.GetComponent<SpriteRenderer>();
        spriteRndr.drawMode = SpriteDrawMode.Sliced;
        spriteRndr.size = new Vector2(1, 11.49f);


        this.boxCollider = this.GetComponent<BoxCollider2D>();
        this.boxCollider.enabled = false;
        this.timeSinceStart = 0;
        Invoke("LaserStartEffect", 0);
        Invoke("LaserStopEffect", 5);
    }
    
    void LaserStartEffect()
    {
        if (PlayerController.IsObstacleOn)
        {
            if (counter <= 3)
            {
                if (spriteRndr.sprite == laserOffSprite)
                {
                    counter++;
                    spriteRndr.sprite = null;
                    spriteRndr.drawMode = SpriteDrawMode.Sliced;
                    spriteRndr.size = new Vector2(1, 11.49f);
                }
                else
                {
                    spriteRndr.sprite = laserOffSprite;
                    spriteRndr.drawMode = SpriteDrawMode.Sliced;
                    spriteRndr.size = new Vector2(1, 11.49f);
                }

                Invoke("LaserStartEffect", 0.5f);

            }
            else
            {
                this.boxCollider.enabled = true;
                if (spriteRndr.sprite == laserOnSprite)
                {
                    spriteRndr.sprite = laserOffSprite;
                    spriteRndr.drawMode = SpriteDrawMode.Sliced;
                    spriteRndr.size = new Vector2(1, 11.49f);
                }
                else
                {
                    spriteRndr.sprite = laserOnSprite;
                    spriteRndr.drawMode = SpriteDrawMode.Sliced;
                    spriteRndr.size = new Vector2(1, 11.49f);
                }
                //Invoke("LaserStartEffect", 0.1f);
            }
        }
    }
    void LaserStopEffect()
    {
        
        spriteRndr.sprite = null;
        this.boxCollider.enabled = false;
        PlayerController.IsObstacleOn = false;
        //Invoke("MakeObstacleOn", 2f);
    }
    void MakeObstacleOn()
    {
        PlayerController.IsObstacleOn = false;
    }
    public void FixedUpdate()
    {
        //this.timeSinceStart += Time.fixedDeltaTime;

        //if (counter > 3)
        //{
        //    if (this.timeSinceStart > this.interval)
        //    {
        //        this.laserOn = !this.laserOn;
        //        this.timeSinceStart = 0;

        //        this.spriteRndr.sprite = this.laserOn ? this.laserOnSprite : this.laserOffSprite;
        //        this.boxCollider.enabled = this.laserOn;
        //    }
        //}

        //this.transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        //this.interval -= 0.001f;
    }
}
