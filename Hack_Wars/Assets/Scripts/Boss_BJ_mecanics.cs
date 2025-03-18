using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_BJ_mecanics : MonoBehaviour
{
    public float speed = 2f;
    public float minY = -3f, maxY = 3f;
    public GameObject card;
    public float fireRate = 1.5f;
    public Transform spawnPoint;
    public int life = 10;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 1f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed * direction * Time.deltaTime, 0);

        if (transform.position.y >= maxY || transform.position.y <= minY)
        {
            direction *= -1;
        }

    }

    void Shoot()
    {
        Instantiate(card, spawnPoint.position, Quaternion.identity);
       

    }

    public void RecibirDaño(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
