using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	static public bool goalMet = false;
	public AudioClip goalSound;
	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Projectile"){
			Goal.goalMet = true;
			Color c = GetComponent<Renderer>().material.color;
			c.a = 1;
			GetComponent<Renderer>().material.color = Color.red;

			source.PlayOneShot (goalSound, 1f);
		}
	}
}
