using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeController : MonoBehaviour
{
    private int coins = 0;
    public TextMeshProUGUI CoinText;
    // Start is called before the first frame update
    void Start()
    {
        coins = PlayerPrefs.GetInt("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = "Coins: "+this.coins.ToString();
    }
}
