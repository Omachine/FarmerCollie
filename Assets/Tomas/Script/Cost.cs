using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cost : MonoBehaviour
{
    public Text playerSpeedUpgradeCostText;
    public Text numberOfSheepUpgradeCostText;
    public Text speedOfSpawnSheepUpgradeCostText;
    public UpgradeSystem upgradeSystem;

    // Update is called once per frame
    void Update()
    {
        playerSpeedUpgradeCostText.text = "Player Speed Upgrade Cost: " + upgradeSystem.playerSpeedUpgradeCost;
        numberOfSheepUpgradeCostText.text = "Number of Sheep Upgrade Cost: " + upgradeSystem.numberOfSheepUpgradeCost;
        speedOfSpawnSheepUpgradeCostText.text = "Speed of Spawn Sheep Upgrade Cost: " + upgradeSystem.speedOfSpawnSheepUpgradeCost;
    }
}
