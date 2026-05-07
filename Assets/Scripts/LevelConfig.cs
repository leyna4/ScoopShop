using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScoopShop/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Level Bilgisi")]
    public string levelName;
    public int levelNumber;

    [Header("Boncuk Ayarlari")]
    public int activeBoncukTypes = 1; // Level 1: 1, Level 2: 2, Level 3: 3
    public int minBeadsPerScoop = 3;
    public int maxBeadsPerScoop = 7;

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