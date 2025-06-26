using UnityEngine;

[CreateAssetMenu(fileName = "ClickerConfig", menuName = "Configs/ClickerConfig")]
public class ClickerConfig : ScriptableObject
{
    public int MaxEnergy = 1000;
    public int EnergyPerTick = 10;
    public float EnergyTickSeconds = 10f;
    public float AutoCollectSeconds = 3f;
    public int ClickCost = 1;
    public int AutoCollectCost = 1;
    public int CurrencyPerClick = 1;
    
    [Header("Visual")]
    public float ButtonMaxAngle = 18f;
    public float ButtonPressDuration = 0.13f;
    public float ButtonReturnDuration = 0.25f;
} 