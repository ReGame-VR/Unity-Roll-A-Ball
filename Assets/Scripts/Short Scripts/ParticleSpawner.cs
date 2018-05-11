using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class that spawns particle effects for the ball game
public class ParticleSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject starParticles;

    [SerializeField]
    private GameObject resetParticles;

    [SerializeField]
    private GameObject badParticles;

    // Spawn star effect at given position
    public void SpawnStarParticles(Vector3 position)
    {
        Instantiate(starParticles, position, Quaternion.identity);
    }
    
    // Spawn reset effect at given position
    public void SpawnResetParticles(Vector3 position)
    {
        Instantiate(resetParticles, position, Quaternion.identity);
    }

    // Spawn reset effect at given position
    public void SpawnBadParticles(Vector3 position)
    {
        Instantiate(badParticles, position, Quaternion.identity);
    }
}
