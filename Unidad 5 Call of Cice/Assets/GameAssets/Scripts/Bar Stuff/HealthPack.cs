using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
	[SerializeField]
	private float ammount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.gameObject.GetComponent<Health>().hasMaxHealth())
        {
            other.gameObject.GetComponent<Health>().GainHealth(ammount);
            Destroy(this.gameObject);
        }
    }
}
