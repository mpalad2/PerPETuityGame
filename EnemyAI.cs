/*
 * Milan Palad and Ugonma Nnakwe
 * Fundamentals of Game Design Final Project
 * Due Date: 12/10/20
 * This is the file for the basic enemy AI (aka the little gators).
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // BEGIN code from unity documentation
    // Code altered for 2D movement
    // https://docs.unity3d.com/2019.3/Documentation/Manual/nav-AgentPatrol.html

    //declare variables
    public Transform[] goals;
    private Vector2 position;
    public float speed;
    private int index = 0;
    Rigidbody rb;

    // target and lives
    public Transform target;
    public float distance;
    public Transform[] enemyLives;
    int lifeIndex = 0;

    // To access another Game Object
    public GameObject player1;
    public GameObject player2;

    // Variable from CouchFerret makes Games
    // https://www.youtube.com/watch?v=rycsXRO6rpI
    // Call for animation
    public Animator animator;

    // Start is called before the first frame update
    // Move to a new position at the start
    void Start()
    {
        // Boolean from Hundred Fires Games
        // https://www.youtube.com/watch?v=dy8hkDmygRI
        animator.SetBool("isMoving", true);

        // Floats from CouchFerret makes Games
        // https://www.youtube.com/watch?v=rycsXRO6rpI
        animator.SetFloat("Horizontal", (goals[index].position.x - transform.position.x));
        animator.SetFloat("Vertical", (goals[index].position.y - transform.position.y));

        position = gameObject.transform.position;
        NextMove();
    }

    // Update is called once per frame
    void Update()
    {
        // Change target according to active character
        if (player1.activeSelf == true)
        {
            target = player1.transform;
        }
        else
        {
            target = player2.transform;
        }
        if (Vector2.Distance(transform.position, target.position) < distance)
        {
            // Boolean from Hundred Fires Games
            // https://www.youtube.com/watch?v=dy8hkDmygRI
            animator.SetBool("isMoving", true);

            // Floats from CouchFerret makes Games
            // https://www.youtube.com/watch?v=rycsXRO6rpI
            animator.SetFloat("Horizontal", (target.position.x - transform.position.x));
            animator.SetFloat("Vertical", (target.position.y - transform.position.y));

            // BEGIN code from unity documentation
            // https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html 
            float step = speed * Time.deltaTime;
            // Move towards goal at index
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
        else
        {
            // Boolean from Hundred Fires Games
            // https://www.youtube.com/watch?v=dy8hkDmygRI
            animator.SetBool("isMoving", true);

            // Floats from CouchFerret makes Games
            // https://www.youtube.com/watch?v=rycsXRO6rpI
            animator.SetFloat("Horizontal", (goals[index].position.x - transform.position.x));
            animator.SetFloat("Vertical", (goals[index].position.y - transform.position.y));

            // BEGIN code from unity documentation
            // https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html 
            float step = speed * Time.deltaTime;
            // Move towards goal at index
            transform.position = Vector2.MoveTowards(transform.position, goals[index].position, step);
            // END code from unity documentation
            // https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html 
            // When close to goal at index
            // Code altered by Milan to adjust for 2D as no nav agent is actually used
            if (Vector2.Distance(transform.position, goals[index].position) < 0.5)
            {
                // Call function for next move
                NextMove();
            }
        }
    }

    // Movement method to go to different positions
    void NextMove()
    {
        // If no goals, exit
        if (goals.Length == 0)
        {
            return;
        }

        // Boolean from Hundred Fires Games
        // https://www.youtube.com/watch?v=dy8hkDmygRI

        animator.SetBool("isMoving", true);

        // Floats from CouchFerret makes Games
        // https://www.youtube.com/watch?v=rycsXRO6rpI
        animator.SetFloat("Horizontal", (goals[index].position.x - transform.position.x));
        animator.SetFloat("Vertical", (goals[index].position.y - transform.position.y));

        // BEGIN code from unity documentation
        // https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html 
        float step = speed * Time.deltaTime;
        // Move towards goal at index
        transform.position = Vector2.MoveTowards(transform.position, goals[index].position, step);
        // Add to index to set up for next move
        index = Random.Range(0,5);
        // Precautionary if - if index is greater or equal to length (meaning the last move was at the last index)
        if (index >= goals.Length)
        {
            //reset index to zero
            index = 0;
        }
        // END code from unity documentation
        // https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html 
    }
    // END code from unity documentation
    // Code altered for 2D movement
    // https://docs.unity3d.com/2019.3/Documentation/Manual/nav-AgentPatrol.html

    void OnCollisionEnter2D(Collision2D other)
    {
        // If hit by ammo
        if (other.gameObject.tag == "Ammo")
        {
            // Set index for lives at last index
            lifeIndex = enemyLives.Length - 1;
            // Find current life the boss is at
            while (enemyLives[lifeIndex].gameObject.activeSelf == false)
            {
                // Subtract until the current life index is found
                lifeIndex--;
            }
            // Show visual loss of life for enemy
            enemyLives[lifeIndex].gameObject.SetActive(false);
            // If first heart is inactive
            if (enemyLives[0].gameObject.activeSelf == false)
            {
                // Destroy enemy from game
                Destroy(gameObject);
            }
        }
    }
}
