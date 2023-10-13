using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speedBullet = 40.0f;
    public ParticleSystem deathOfEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>(); //Velocity represents the object's speed and direction of movement in 3D space
        rb.velocity = transform.forward * speedBullet; //"Make the GameObject move in its forward direction  at a speed specified by the speedBullet variable
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * Time.deltaTime * speedBullet);
    }
    private void OnCollisionEnter(Collision collision)
    { 
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // destroy enemy
            Destroy(gameObject); // destroy gameobject
            Instantiate(deathOfEnemy, transform.position, deathOfEnemy.transform.rotation);
        }
    }

}

