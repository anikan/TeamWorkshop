using UnityEngine;
using System.Collections;

public class PrintColl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        print(other.collider.name);
    }

    void OnTriggerEnter(Collider other)
    {
        GetComponent<Ball>();
        print(other.name);
    }
}
