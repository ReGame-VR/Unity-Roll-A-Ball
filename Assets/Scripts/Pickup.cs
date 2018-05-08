using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public int rotateSpeed = 150;

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
	}
}
