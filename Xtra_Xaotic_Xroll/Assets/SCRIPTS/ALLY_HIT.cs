using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ALLY_HIT : MonoBehaviour
{
    bool damageDone = false;
    public float atackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // El aliado reacciona con el collider de la base enemiga.
        if (collision.collider.tag == "ENEMYBASE")
        {
            if (damageDone == false)
            {
                StartCoroutine(DamageCD(collision.gameObject));
            }
            
        }
    }

    IEnumerator DamageCD(GameObject _base)
    {
        damageDone = true;
        yield return new WaitForSeconds(atackSpeed);
        _base.gameObject.GetComponent<ENEMY_SPAWNER>().GetDamage();
        Debug.LogWarning(_base.gameObject.name);
        damageDone = false;
        print("BONK");

    }
}
