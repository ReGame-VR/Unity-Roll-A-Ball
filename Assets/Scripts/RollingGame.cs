using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingGame : MonoBehaviour {

    [Tooltip("The number of items to pick up to win game.")]
    [SerializeField]
    private int numPickups = 5;

    private int numPickupsCollected = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (numPickupsCollected >= numPickups)
        {
            //GameWon();
        }
	}

    public void PickupCollected()
    {
        numPickupsCollected++;
    }
}
