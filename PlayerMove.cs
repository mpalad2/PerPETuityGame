/*
 * Milan Palad and Ugonma Nnakwe
 * Fundamentals of Game Design Final Project
 * Due Date: 12/10/20
 * This is the file for the player movement and interaction system.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // START code from CouchFerret makes Games
    // https://www.youtube.com/watch?v=rycsXRO6rpI
    
    // Call for animation
    public Animator animator;

    // Two variables from jchaps1
    // https://www.instructables.com/How-to-make-a-simple-game-in-Unity-3D/
    private int count;
    public Text countText;

    // Variables for info on screen text, easter egg objects, and player lives
    public Transform textBox;
    public Text popUpText;
    public Transform[] easterEggs;
    public Transform interactKey;

    public Transform[] lives;
    int index = 0;

    // Audio
    AudioSource interactSound;
    AudioSource owSound;
    AudioSource collectSound;
    AudioSource heartSound;

    // Start is called before the first frame update
    void Start()
    {
        // Get and set audio
        AudioSource[] allAudio = GetComponents<AudioSource>();
        interactSound = allAudio[0];
        owSound = allAudio[1];
        collectSound = allAudio[2];
        heartSound = allAudio[3];
        // Set default value
        textBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Move player
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
    }
    // END code from CouchFerret makes Games
    // https://www.youtube.com/watch?v=rycsXRO6rpI

    // BEGIN code from jchaps1
    // https://www.instructables.com/How-to-make-a-simple-game-in-Unity-3D/

    // Function for what happens when the Player collides with the Items
    void OnTriggerEnter2D(Collider2D other)
    {
        // Play sound
        collectSound.Play(0);

        // Checking if the player hits an item
        // Next line modified by Milan
        if (other.gameObject.tag == "Coin")
        {
            // Sets item object to inactive (disappears)
            other.gameObject.SetActive(false);
            // Adds to score count
            count = count + 1;
            // Calls function for count score
            CountText();
        }
        
        // Next conditionals added by Milan
        // Check if one of the easter eggs has been picked up
        if (other.gameObject.tag == "Tifa")
        {
            // Show text background (if not already triggered earlier)
            textBox.gameObject.SetActive(true);
            // Set item to inactive
            other.gameObject.SetActive(false);
            // If other easter egg has not been picked up yet
            if (easterEggs[1].gameObject.activeSelf == true)
            {
                // Display this text
                popUpText.text = "Picked up Tifa.";
            }
            // Otherwise
            else
            {
                // Display this other text
                popUpText.text = "Picked up Tifa second.";
            }
        }

        // Check if one of the easter eggs has been picked up
        if (other.gameObject.tag == "Aerith")
        {
            // Show text background (if not already triggered earlier)
            textBox.gameObject.SetActive(true);
            // Set item to inactive
            other.gameObject.SetActive(false);
            // If other easter egg has not been picked up yet
            if (easterEggs[0].gameObject.activeSelf == true)
            {
                // Display this text
                popUpText.text = "Picked up Aerith.";
            }
            // Otherwise
            else
            {
                // Display this other text
                popUpText.text = "Picked up Aerith second.";
            }
        }
    }


    // Function that updates the score on the GUI display
    void CountText()
    {
        textBox.gameObject.SetActive(true);
        popUpText.text = "Picked up Biscuit.";
        // Show current score
        countText.text = "Biscuits: " + count.ToString();
    }
    // END code from jchaps1
    // https://www.instructables.com/How-to-make-a-simple-game-in-Unity-3D/

    // START code referenced by unity docs
    // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
    // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionStay.html
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionExit.html

    // For when a collision first occurs
    void OnCollisionEnter2D(Collision2D other)
    {
        // If other object is an enemy
        if (other.gameObject.tag == "Enemy")
        {
            // Play sound
            owSound.Play(0);
            // Set index to last life
            index = lives.Length - 1;
            // Find the current life the player is on (three lives, two lives, etc.) through checking active states
            while (lives[index].gameObject.activeSelf == false)
            {
                // Subtract until you get to the index for the current life
                index--;
            }
            // Once found, use index to show the visual of the loss of a life
            lives[index].gameObject.SetActive(false);
        }
        // If other object is a sign
        if (other.gameObject.tag == "ReadMe1" || other.gameObject.tag == "ReadMe2" || other.gameObject.tag == "ReadMe3")
        {
            // Show interact key to indicate to users to press F for a text to pop up
            interactKey.gameObject.SetActive(true);
        }
        // If other object is the Merchant
        if (other.gameObject.tag == "Merchant" || other.gameObject.tag == "Machine")
        {
            // Show interact key to indicate to users to press F for a text to pop up
            interactKey.gameObject.SetActive(true);
        }
    }

    // During the time the collision is still occuring
    void OnCollisionStay2D(Collision2D other)
    {
        // If the other object is the first sign
        if (other.gameObject.tag == "ReadMe1")
        {
            // Continue showing the interact key
            interactKey.gameObject.SetActive(true);
            // If the user presses F during the collision
            if (Input.GetKeyDown("f"))
            {
                // Play sound
                interactSound.Play(0);
                // Show text background (if not already triggered earlier) 
                textBox.gameObject.SetActive(true);
                // Display text of information
                popUpText.text = "Welcome! Before you go, press i on your keyboard.";
            }
        }
        // If the other object is the second sign
        else if (other.gameObject.tag == "ReadMe2")
        {
            // Continue showing the interact key
            interactKey.gameObject.SetActive(true);
            // If the user presses F during the collision
            if (Input.GetKeyDown("f"))
            {
                // Play sound
                interactSound.Play(0);
                // Show text background (if not already triggered earlier)
                textBox.gameObject.SetActive(true);
                // Display text of information
                popUpText.text = "Shooting can be used for both blockades and enemies.";
            }
        }
        // If the other object is the third sign
        else if (other.gameObject.tag == "ReadMe3")
        {
            // Continue showing the interact key
            interactKey.gameObject.SetActive(true);
            if (Input.GetKeyDown("f"))
            {
                // Play sound
                interactSound.Play(0);
                // Show text background (if not already triggered earlier)
                textBox.gameObject.SetActive(true);
                // Display text of information
                popUpText.text = "The sign reads: \"Warning: Crocodiles Found in this Area\".";
            }
        }

        // If other object is the Merchant
        if (other.gameObject.tag == "Merchant")
        {
            // Show interact key to indicate to users to press F for a text to pop up
            interactKey.gameObject.SetActive(true);
            if (Input.GetKeyDown("f"))
            {
                if (lives[3].gameObject.activeSelf == false)
                {
                    // Play sound
                    heartSound.Play(0);
                    // Set index to first life
                    index = 0;
                    // Find the current life the player is on (three lives, two lives, etc.) through checking active states
                    while (lives[index].gameObject.activeSelf == true)
                    {
                        // Add until you get to the index for the current life
                        index++;
                    }
                    // Once found, use index to show the visual of the addition of a life
                    lives[index].gameObject.SetActive(true);
                    // Show text background (if not already triggered earlier)
                    textBox.gameObject.SetActive(true);
                    // Notify user of new heart/life
                    popUpText.text = "The Merchant Goose has granted you an extra life!";
                }
                else
                {
                    // Play sound
                    interactSound.Play(0);
                    // Show text background (if not already triggered earlier)
                    textBox.gameObject.SetActive(true);
                    // Notify user of new heart/life
                    popUpText.text = "Sorry, the Merchant Goose has no more to give.";
                }
            }
        }

        // If other object is the Time Machine
        if (other.gameObject.tag == "Machine")
        {
            // Show interact key to indicate to users to press F for a text to pop up
            interactKey.gameObject.SetActive(true);
            if (Input.GetKeyDown("f"))
            {
                // Play sound
                interactSound.Play(0);
                // Show text background (if not already triggered earlier)
                textBox.gameObject.SetActive(true);
                // Display text of information
                popUpText.text = "This looks familiar. Isn't this the scientist's current pet project?";
            }
        }
    }

    // When the collision is no longer occuring
    void OnCollisionExit2D(Collision2D other)
    {
        // Hide interaction key (visually indicates you are too far to interact with the object)
        interactKey.gameObject.SetActive(false);
    }

    // END code referenced by unity docs
    // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
    // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionStay.html
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionExit.html
}
