using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject livesContainer;

    private Image[] lifeImages;

    public void Start()
    {
        lifeImages = livesContainer.GetComponentsInChildren<Image>();
        player.DamageTaken += OnPlayerDamageTaken;
    }

    private void OnPlayerDamageTaken(int lives)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = i < lives;
        }
    }
}
