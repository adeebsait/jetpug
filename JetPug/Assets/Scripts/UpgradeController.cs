using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeController : MonoBehaviour
{
    private int coins = 0;
    private int Scoins = 0;
    private int SpeedLevel = 0;
    private int MagnetLevel = 0;
    private int BubbleLevel = 0;

    private int SDSpend = 0;
    private int MSpend = 0;
    private int BSpend = 0;
    private int lvl1price = 50;
    private int lvl2price = 200;
    private int lvl3price = 1000;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI SDLevel;
    public TextMeshProUGUI MagLevel;
    public TextMeshProUGUI BubLevel;
    public TextMeshProUGUI SDPrice;
    public TextMeshProUGUI MagPrice;
    public TextMeshProUGUI BubPrice;
    // Start is called before the first frame update
    void Start()
    {
        coins = PlayerPrefs.GetInt("Coin");
        SpeedLevel = PlayerPrefs.GetInt("SpeedLevel");
        MagnetLevel = PlayerPrefs.GetInt("MagnetLevel");
        BubbleLevel = PlayerPrefs.GetInt("BubbleLevel");
        Scoins = coins;
        PlayerPrefs.SetInt("SpeedLevel", 0);
        PlayerPrefs.SetInt("BubbleLevel", 0);
        PlayerPrefs.SetInt("MagnetLevel", 0);
    }

    public void SLevelUpgrade()
    {
        if (SpeedLevel < 3 && Scoins >= SDSpend)
        {
            SpeedLevel++;
            PlayerPrefs.SetInt("SpeedLevel", this.SpeedLevel);
            
            Scoins = Scoins - SDSpend;
            PlayerPrefs.SetInt("Coin", this.Scoins);
        }
    }
    public void MLevelUpgrade()
    {
        if (MagnetLevel < 3 && Scoins >= MSpend)
        {
            MagnetLevel++;
            PlayerPrefs.SetInt("MagnetLevel", this.MagnetLevel);
            
            Scoins = Scoins - MSpend;
            PlayerPrefs.SetInt("Coin", this.Scoins);
        }
    }
    public void BLevelUpgrade()
    {
        if (BubbleLevel < 3 && Scoins >= BSpend)
        {
            BubbleLevel++;
            PlayerPrefs.SetInt("BubbleLevel", this.BubbleLevel);
            
            Scoins = Scoins - BSpend;
            PlayerPrefs.SetInt("Coin", this.Scoins);
        }
    }
    // Update is called once per frame
    void Update()
    {
        coins = PlayerPrefs.GetInt("Coin");
        CoinText.text = "Coins: " + this.coins.ToString();
        SDLevel.text = "Level " + this.SpeedLevel.ToString();
        MagLevel.text = "Level " + this.MagnetLevel.ToString();
        BubLevel.text = "Level " + this.BubbleLevel.ToString();
        if (this.SpeedLevel == 0)
        {
            SDPrice.text = "Upgrade: " + this.lvl1price.ToString();
            SDSpend = lvl1price;
        }
        if (this.SpeedLevel == 0)
        {
            MagPrice.text = "Upgrade: " + this.lvl1price.ToString();
            MSpend = lvl1price;
        }
        if (this.SpeedLevel == 0)
        {
            BubPrice.text = "Upgrade: " + this.lvl1price.ToString();
            BSpend = lvl1price;
        }

        if (this.SpeedLevel == 1)
        {
            SDPrice.text = "Upgrade: " + this.lvl2price.ToString();
            SDSpend = lvl2price;
        }
        if (this.MagnetLevel == 1)
        {
            MagPrice.text = "Upgrade: " + this.lvl2price.ToString();
            MSpend = lvl2price;
        }
        if (this.BubbleLevel == 1)
        {
            BubPrice.text = "Upgrade: " + this.lvl2price.ToString();
            BSpend = lvl2price;
        }

        if (this.SpeedLevel == 2)
        {
            SDPrice.text = "Upgrade: " + this.lvl3price.ToString();
            SDSpend = lvl3price;
        }
        if (this.MagnetLevel == 2)
        {
            MagPrice.text = "Upgrade: " + this.lvl3price.ToString();
            MSpend = lvl3price;
        }
        if (this.BubbleLevel == 2)
        {
            BubPrice.text = "Upgrade: " + this.lvl3price.ToString();
            BSpend = lvl3price;
        }

        if (this.SpeedLevel == 3)
        {
            SDPrice.text = "Upgrade: MAX";
        }
        if (this.MagnetLevel == 3)
        {
            MagPrice.text = "Upgrade: MAX";
        }
        if (this.BubbleLevel == 3)
        {
            BubPrice.text = "Upgrade: MAX";
        }
    }
}
