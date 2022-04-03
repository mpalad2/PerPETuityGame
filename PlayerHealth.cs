/*
 * Milan Palad and Ugonma Nnakwe
 * Fundamentals of Game Design Final Project
 * Due Date: 12/10/20
 * This is the file for the player's health system.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Lives
    public Transform[] lives;
    // Lose screen
    public Transform loseScreen;
    // Background
    public Transform back;
    // Start is called before the first frame update
    void Start()
    {
        // Set to default
        loseScreen.gameObject.SetActive(false);
        lives[3].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If all player lives are gone
        if (lives[0].gameObject.activeSelf == false)
        {
            // Show lose screen
            loseScreen.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
        }
    }
}
