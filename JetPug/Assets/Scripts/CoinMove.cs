using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    GameObject Player;
    MouseController playerscript;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerscript = Player.GetComponent<MouseController>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (MouseController.isMagnetOn)
        {
            //print(Player.transform.position.x + "," + transform.position.x + ","+ (Player.transform.position.x - transform.position.x));
            if(Player.transform.position.x-transform.position.x <= 1.0f  && Player.transform.position.x-transform.position.x > -1.0f)
            transform.position = Vector2.MoveTowards(transform.position, playerscript.transform.position, 12 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
