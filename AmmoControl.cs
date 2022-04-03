/*
 * Milan Palad and Ugonma Nnakwe
 * Fundamentals of Game Design Final Project
 * Due Date: 12/10/20
 * This is the file for deleting ammo after a set amount of time or by hitting something.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoControl : MonoBehaviour
{
    // START code referenced from unity docs
    // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html

    // Audio
    AudioSource[] shootSound;
    AudioSource player;
    AudioSource boss;

    // Start is called before the first frame update
    void Start()
    {
        // Play sound
        shootSound = GetComponents<AudioSource>();
        player = shootSound[0];
        boss = shootSound[1];
        // If player ammo
        if (gameObject.tag == "Ammo")
        {
            // Play ammo noise for player
            player.Play(0);
        }
        // Otherwise play boss ammo sound
        else
        {
            boss.Play(0);
        }
        // END code referenced from unity docs
        // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
        // Destroy ammo after 5 seconds
        Destroy(gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // START code from unity docs
    // https://docs.unity3d.com/ScriptReference/Collider2D.OnCollisionEnter2D.html
    // On colliding
    void OnCollisionEnter2D(Collision2D other)
    {
        // Destroy ammo
        Destroy (gameObject);

        // If other object is a blockade
        if (other.gameObject.tag == "Blockade")
        {
            // Destroy it too
            Destroy(other.gameObject);
        }
    }
    // END code from unity docs
    // https://docs.unity3d.com/ScriptReference/Collider2D.OnCollisionEnter2D.html
}
