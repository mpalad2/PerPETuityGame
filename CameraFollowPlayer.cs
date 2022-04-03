/*
 * Milan Palad and Ugonma Nnakwe
 * Fundamentals of Game Design Final Project
 * Due Date: 12/10/20
 * This is the file for the camera following the player.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // BEGIN code from jchaps1
    // https://www.instructables.com/How-to-make-a-simple-game-in-Unity-3D/

    // To access another Game Object
    public GameObject player1;
    public GameObject player2;
    GameObject player;

    // Vector3
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Change target according to active character
        if (player1.activeSelf == true)
        {
            player = player1;
        }
        else
        {
            player = player2;
        }
        //(x,y,z) position of the camera
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.activeSelf == true)
        {
            player = player1;
        }
        else
        {
            player = player2;
        }
    }

    // Update to define the camera's position as the player's position plus some offset
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    // END  code from jchaps1
    // https://www.instructables.com/How-to-make-a-simple-game-in-Unity-3D/
}
