using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    [SerializeField] AudioClip[] bounceSounds;
	// Use this for initialization
	void Start () {
        // we automatically add an audiosource, if one has not been manually added.
        // (if you want to control the rolloff or other audio settings, add an audiosource manually)
        gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        //If this ball is not colliding with a player then play the bounce noise.
        if (!(col.gameObject.tag.Equals("Player")))
        {
            GetComponent<AudioSource>().clip = bounceSounds[Random.Range(0, bounceSounds.Length)];
            GetComponent<AudioSource>().Play();
        }
    }
}
