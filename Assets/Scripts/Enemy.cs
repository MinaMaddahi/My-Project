using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     public float speed = 2.0f;
    //  private Rigidbody enemyRb;
    //  private GameObject player;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
      //  enemyRb = GetComponent<Rigidbody>();
      //  player = GameObject.Find("Chick");
        
    }

    // Update is called once per frame
    void Update()
    {
      //  enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);
      if (target != null)
        {
            //Calculate the direction from the enemy to the target
            Vector3 direction = (transform.position - target.position).normalized;
            // Calculate the distance between the enemy and the target.
            float distance = Vector3.Distance(transform.position, target.position);
          //  Move the enemy towards the target
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
