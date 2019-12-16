using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private int damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject hitParticle;
    [SerializeField]
    private GameObject bloodParticle;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        Destroy(this.gameObject, 10);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Health>().LoseHealth((float)damage);
        }

        if (col.gameObject.GetComponent<ParticleWeapon>() == null)
        {
            Destroy(Instantiate(hitParticle, transform.position, Quaternion.identity), 3);
            Destroy(this.gameObject);
        }
    }
}