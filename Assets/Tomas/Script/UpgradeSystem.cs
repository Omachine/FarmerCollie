using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject sheepSpawner;
    public GameObject scoreManager;
    public GameObject upgradeMenuUI;
    public GameObject pauseMenuUI;
    public Button playerSpeedUpgradeButton; // Reference to the player speed upgrade button
    public Button numberOfSheepUpgradeButton; // Reference to the number of sheep upgrade button
    public Button speedOfSpawnSheepUpgradeButton; // Reference to the speed of spawn sheep upgrade button
    public int playerSpeedUpgradeCost = 10;
    public int numberOfSheepUpgradeCost = 10;
    public int speedOfSpawnSheepUpgradeCost = 10;
    public int costIncrement = 5;
    private Color limitReachedColor = new Color(102 / 255f, 99 / 255f, 0); // Color when the upgrade limit is reached (666300 in hexadecimal)
    private Color initialColor; // Initial color of the buttons

    void Start()
    {
        // Store the initial color of the buttons
        initialColor = playerSpeedUpgradeButton.image.color;
    }

    void Update()
    {
        // Change the color of the buttons if the upgrades have reached their limits
        playerSpeedUpgradeButton.image.color = player.GetComponent<NavMeshAgent>().speed >= 10 ? limitReachedColor : initialColor;
        numberOfSheepUpgradeButton.image.color = sheepSpawner.GetComponent<SheepSpawner>().npcLimit >= 15 ? limitReachedColor : initialColor;
        speedOfSpawnSheepUpgradeButton.image.color = sheepSpawner.GetComponent<SheepSpawner>().spawnRate <= 1 ? limitReachedColor : initialColor;
    }

    public void UpgradePlayerSpeed()
    {
        if (ScoreManager.score >= playerSpeedUpgradeCost && player.GetComponent<NavMeshAgent>().speed < 10)
        {
            player.GetComponent<NavMeshAgent>().speed += 1;
            ScoreManager.score -= playerSpeedUpgradeCost;
            playerSpeedUpgradeCost += costIncrement;
        }
    }

    public void UpgradeNumberOfSheep()
    {
        if (ScoreManager.score >= numberOfSheepUpgradeCost && sheepSpawner.GetComponent<SheepSpawner>().npcLimit < 15)
        {
            sheepSpawner.GetComponent<SheepSpawner>().npcLimit += 1;
            ScoreManager.score -= numberOfSheepUpgradeCost;
            numberOfSheepUpgradeCost += costIncrement;
        }
    }

    public void UpgradeSpeedOfSpawnSheep()
    {
        if (ScoreManager.score >= speedOfSpawnSheepUpgradeCost && sheepSpawner.GetComponent<SheepSpawner>().spawnRate > 1)
        {
            sheepSpawner.GetComponent<SheepSpawner>().spawnRate -= 1;
            ScoreManager.score -= speedOfSpawnSheepUpgradeCost;
            speedOfSpawnSheepUpgradeCost += costIncrement;
        }
    }

    public void Back()
    {
        upgradeMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}

