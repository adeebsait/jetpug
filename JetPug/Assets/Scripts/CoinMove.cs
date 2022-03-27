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
            if (Player.transform.position.x - transform.position.x <= 1.0f && Player.transform.position.x - transform.position.x > -1.0f)
                if (PlayerPrefs.GetInt("MagnetLevel") == 0)
                    transform.position = Vector2.MoveTowards(transform.position, playerscript.transform.position, 3 * Time.deltaTime);
                else if (PlayerPrefs.GetInt("MagnetLevel") == 1)
                    transform.position = Vector2.MoveTowards(transform.position, playerscript.transform.position, 6 * Time.deltaTime);
                else if (PlayerPrefs.GetInt("MagnetLevel") == 2)
                    transform.position = Vector2.MoveTowards(transform.position, playerscript.transform.position, 9 * Time.deltaTime);
                else if (PlayerPrefs.GetInt("MagnetLevel") == 3)
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
