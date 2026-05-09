using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScoopShop/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Level Bilgisi")]
    public string levelName;
    public int levelNumber;
    [Header("Urun Degerleri")]
    public int hairClipValue = 5;
    public int wetWipeValue = 8;
    public int nailPolishValue = 10;
    [Header("Boncuk Ayarlari")]
    public int activeBoncukTypes = 1; // Level 1: 1, Level 2: 2, Level 3: 3
    [Header("Siparis Zorlugu")]
    public int minBeadsPerType = 1;
    public int maxBeadsPerType = 5;
    [Header("Ekonomi")]
    public int coinTarget = 50;      // hedef coin
    public int dailyCost = 0;        // Level 1'de gider yok
    public int dailyCostIncrease = 0; // her gün artan gider

    [Header("Zaman")]
    public bool hasTimer = false;
    public float timerSeconds = 60f;

    [Header("Upgrade")]
    public bool canUpgradeScoop = false;
    public bool hasNewJar = false;
}