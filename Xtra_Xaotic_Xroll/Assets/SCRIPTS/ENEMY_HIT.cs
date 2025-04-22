using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ENEMY_HIT : MonoBehaviour
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
        // El enemigo reacciona con el collider de la base aliada.
        if (collision.collider.tag == "YOURBASE")
        {
            if(damageDone == false)
            {
                StartCoroutine(DamageCD(collision.gameObject));
            }
            //aqui si deja apretao se ejecuta 1000 veces por segundo, añade algun tipo de cooldown crack
        }
    }

    IEnumerator DamageCD (GameObject _base)
    {
        damageDone = true;
        yield return new WaitForSeconds(atackSpeed);
        _base.gameObject.GetComponent<ALLY_SPAWNER>().GetDamage();
        Debug.LogWarning(_base.gameObject.name);
        damageDone = false;
        print("BONK");
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // Si estamos dentro de un enemigo, hacemos clic y llamamos al conteo de vida del enemigo
        if (other.tag == "ALLY")
        {
            other.gameObject.GetComponent<MINION_CONTROLLER>().GetDamage();
        }

    }
}