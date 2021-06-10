using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float damage = 10f;
    public float radius = 1f;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

        if (hits.Length > 0)
        {
            // If -> Printing out hit events (for debugging purposes)
            print("We touched: " + hits[0].gameObject.tag);

            // If -> Apply damage upon hit
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);

            // If -> Deactive the bullet
            gameObject.SetActive(false);
        }
    }
}
