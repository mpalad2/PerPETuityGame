/*
 * Milan Palad and Ugonma Nnakwe
 * Fundamentals of Game Design Final Project
 * Due Date: 12/10/20
 * This is the file for screens activated by keys and trigger events.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenKeyControls : MonoBehaviour
{
    // Screens
    public Transform openScreen;
    public Transform instructions;
    public Transform instructionsKey;
    public Transform back;
    public Transform[] interactKeys;
    public Transform credits;
    public Transform tbc;

    // Start is called before the first frame update
    void Start()
    {
        // Stop time
        Time.timeScale = 0;

        // Set to default false (hidden screens)
        tbc.gameObject.SetActive(false);
        interactKeys[0].gameObject.SetActive(false);
        interactKeys[1].gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        instructionsKey.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // When game starts and user presses enter
        if (Input.GetKeyDown("return"))
        {
            // Resume time
            Time.timeScale = 1;

            // Hide screens to play
            openScreen.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
            instructionsKey.gameObject.SetActive(true);
        }
        // When (i) instructions menu is clicked
        if (Input.GetKeyDown("i"))
        {
            // Show instructions
            instructions.gameObject.SetActive(true);
            // Stop time
            Time.timeScale = 0;
        }
        // If (c) credits screen is clicked
        if (Input.GetKeyDown("c"))
        {
            // Show credits
            credits.gameObject.SetActive(true);
            // Stop time
            Time.timeScale = 0;
        }
        // If (l) exit screen is clicked
        if (Input.GetKeyDown("l"))
        {
            // Close all screens
            closeScreens();
        }
        
    }

    // General method to close all screens
    void closeScreens()
    {
        // Hide screens
        instructions.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        // Resume time
        Time.timeScale = 1;
    }
}
