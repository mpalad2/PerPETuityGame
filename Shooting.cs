/*
 * Milan Palad and Ugonma Nnakwe
 * Fundamentals of Game Design Final Project
 * Due Date: 12/10/20
 * This is the file for shooting ammo.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // START code from Brackeys
    // https://www.youtube.com/watch?v=LNLVOjbrQj4

    // Fire bullet variables
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        } 
    }

    void Shoot()
    {
        // Next line referenced from CouchFerret
        // https://www.youtube.com/watch?v=_QVAC69su3Q
        // Modified by Milan
        Vector3 fireDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        // Instantiate bullet and set it to an object
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Access bullet and get rigidbody component
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //Ignore collision between bullet and player
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // Next line from Couch Ferret
        // https://www.youtube.com/watch?v=_QVAC69su3Q
        rb.velocity = fireDirection * 3.0f;
        rb.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg);
    }

    // END code from Brackeys
    // https://www.youtube.com/watch?v=LNLVOjbrQj4
}
