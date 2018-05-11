using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Tooltip("The script that handles the game logic")]
    [SerializeField]
    private RollingGame gameScript;

    // Where the ball spawns
    private Vector3 startingPosition;

    // The rigidbody for this player
    private Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        startingPosition = transform.position;
        GetComponent<ParticleSpawner>().SpawnResetParticles(transform.position);
    }

    void Update()
    {
        // If the ball fell off the grid, respawn it
        if (Vector3.Distance(transform.position, startingPosition) > 20)
        {
            gameScript.BallOutOfBounds();
            transform.position = startingPosition;
            rigidBody.velocity = new Vector3(0, 0, 0);
            GetComponent<ParticleSpawner>().SpawnResetParticles(transform.position);
        }
        
        // If the ball is going too fast, slow it down!
        if (rigidBody.velocity.magnitude > 20f)
        {
            rigidBody.velocity = rigidBody.velocity * 0.5f;
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
        else if (c.gameObject.tag == "Bad Pickup")
        {
            gameScript.BadPickupCollected();
            GetComponent<ParticleSpawner>().SpawnBadParticles(transform.position);
            Destroy(c.gameObject);
        }
    }

    // Freeze the ball from moving
    public void FreezePlayer()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePosition;
    }

    // Unfreeze the ball
    public void UnfreezePlayer()
    {
        rigidBody.constraints = RigidbodyConstraints.None;
    }
}
