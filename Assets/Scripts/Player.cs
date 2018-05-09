using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Tooltip("The script that handles the game logic")]
    [SerializeField]
    private RollingGame gameScript;

    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
        GetComponent<ParticleSpawner>().SpawnResetParticles(transform.position);
    }

    void Update()
    {
        if (transform.position.y < -20)
        {
            transform.position = startingPosition;
            GetComponent<ParticleSpawner>().SpawnResetParticles(transform.position);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Pickup")
        {
            gameScript.PickupCollected();
            GetComponent<ParticleSpawner>().SpawnStarParticles(transform.position);
            Destroy(c.gameObject);
        }
    }
}
