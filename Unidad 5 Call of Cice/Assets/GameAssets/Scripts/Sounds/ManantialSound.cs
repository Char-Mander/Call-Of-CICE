using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManantialSound : MonoBehaviour
{

    [SerializeField]
    private float detDist;

    AudioSource aSource;
    float distToPlayer;
    GameObject player;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectPlayer())
        {
            if (!aSource.isPlaying)
            {
                aSource.Play();
            }
        }
        else
        {
            if (aSource.isPlaying)
            {
                aSource.Stop();
            }
        }
    }

    private bool DetectPlayer()
    {
        distToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        return distToPlayer <= detDist;
    }
   
}
