using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    // Animator
    public Animator animator;
    // Target to check for
    public Transform target;
    // To access another Game Object
    public GameObject player1;
    public GameObject player2;

    // Fire bullet variables
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Lives
    public Transform[] enemyLives;
    int lifeIndex = 0;

    // Ammo rate
    float rate = .5f;
    float timeToShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Determine target - switch between players
        if (player1.activeSelf == true)
        {
            target = player1.transform;
        }
        else
        {
            target = player2.transform;
        }
        // If player is close
        if (Vector2.Distance(transform.position, target.position) < 20)
        {
            // Activate animation
            animator.SetBool("inCombat", true);
            if (!(animator.GetBool("isDefeat")))
            {
                goAttack();
            }
        }
        // Otherwise
        else
        {
            // Continue default
            animator.SetBool("inCombat", false);
        }
    }

    void goAttack()
    {
        // START code from stackoverflow
        // https://stackoverflow.com/questions/43955288/unity2d-enemy-shooting
        timeToShoot -= Time.deltaTime;
        if (timeToShoot <= 0)
        {
            timeToShoot = 1/rate;
            // END code from stackoverflow
            // https://stackoverflow.com/questions/43955288/unity2d-enemy-shooting

            // Next line referenced from CouchFerret
            // https://www.youtube.com/watch?v=_QVAC69su3Q
            // Modified by Milan
            float x = target.position.x - transform.position.x;
            float y = target.position.y - transform.position.y;
            Vector3 fireDirection = new Vector3(x, y, 0.0f);

            // Instantiate bullet and set it to an object
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // Access bullet and get rigidbody component
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            //Ignore collision between bullet and boss
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            // Next line from Couch Ferret
            // https://www.youtube.com/watch?v=_QVAC69su3Q
            rb.velocity = fireDirection * 1.0f;
            rb.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg);
        }
    }

    // When collided with another object
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
            // Show visual loss of life for boss
            enemyLives[lifeIndex].gameObject.SetActive(false);
            // If first heart is inactive
            if (enemyLives[0].gameObject.activeSelf == false)
            {
                // Play defeat animation
                animator.SetBool("isDefeat", true);
                // Ignore collision between player and boss
                Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }
}
