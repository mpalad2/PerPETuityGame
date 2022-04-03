/*
 * Milan Palad and Ugonma Nnakwe
 * Fundamentals of Game Design Final Project
 * Due Date: 12/10/20
 * This is the file for switching characters.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacters : MonoBehaviour
{
    // Audio
    AudioSource switchSound;
    AudioSource ost1;
    AudioSource ost2;

    // Trigger panel
    public Transform tiles;
    public Transform tbc;
    public Transform tbcScreen;
    public Transform bg;

    // START code from Alexander Zotov
    // https://www.youtube.com/watch?v=_B1Lc6tUSx8

    // oldPlayer variable added by Milan
    public GameObject player1, player2, oldPlayer;
    int currentPlayer = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Set to default
        tbc.gameObject.SetActive(false);
        // Get audio
        AudioSource[] allAudio = GetComponents<AudioSource>();
        // Set audio to variables
        switchSound = allAudio[0];
        ost1 = allAudio[1];
        ost2 = allAudio[2];
        // Set to default
        oldPlayer.gameObject.SetActive(false);

        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // When t is clicked switch character
        if (Input.GetKeyDown("t"))
        {
            SwitchAvatar();
        }
        // Play music after clicking out of start screen
        if (Input.GetKeyDown("return"))
        {
            ost1.Play();
        }
        // When character is nearing the end room
        if ((Vector2.Distance(player1.transform.position, tiles.transform.position) < 0.25) || (Vector2.Distance(player2.transform.position, tiles.transform.position) < 0.25))
        {
            // Play second OST if entering
            if (ost1.isPlaying == true)
            {
                ost1.Stop();
                ost2.Play();
            }
            // Otherwise play first OST
            else
            {
                ost2.Stop();
                ost1.Play();
            }
        }
        // When exit is reached
        if ((Vector2.Distance(player1.transform.position, tbc.transform.position) < 0.25) || (Vector2.Distance(player2.transform.position, tbc.transform.position) < 0.25))
        {
            // Show "To be continued..." screen
            bg.gameObject.SetActive(true);
            tbcScreen.gameObject.SetActive(true);
            // Stop time
            Time.timeScale = 0;
        }
    }

    // Switch characters
    public void SwitchAvatar()
    {
        // Play sound
        switchSound.Play(0);
        
        switch (currentPlayer)
        {
            // If current player is bear
            case 1:
                // Switch to dog
                currentPlayer = 2;

                player1.gameObject.SetActive(false);
                player2.gameObject.SetActive(true);
                // Next line added by Milan
                // Set position to be the same as original player
                player2.transform.position = player1.transform.position;
                break;
            // If current player is dog
            case 2:
                // Switch to bear
                currentPlayer = 1;

                player1.gameObject.SetActive(true);
                player2.gameObject.SetActive(false);
                // Next line added by Milan
                // Set position to be the same as original player
                player1.transform.position = player2.transform.position;
                break;
        }
    }
    // END code from Alexander Zotov
    // https://www.youtube.com/watch?v=_B1Lc6tUSx8
}
