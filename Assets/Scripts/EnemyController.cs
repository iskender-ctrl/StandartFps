using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;
    public int speed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed* Time.deltaTime);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
