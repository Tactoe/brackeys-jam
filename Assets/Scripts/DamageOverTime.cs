using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField] private float damage, delay, lifeSpan;

    private bool canDealDamage;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter p = other.GetComponent<PlayerCharacter>();
        if (p != null)
            StartCoroutine(Damage(p));
        else
            Destroy(gameObject, 0.4f);
        Destroy(gameObject, lifeSpan);
    }

    private void OnTriggerExit(Collider other)
    {
        canDealDamage = false;
    }

    IEnumerator Damage(PlayerCharacter p)
    {
        canDealDamage = true;
        while (canDealDamage)
        {
            p.GetHit(damage);
            yield return new WaitForSeconds(delay);
        }
    }
}
