using UnityEngine;
using System.Collections;

public class Walls : MonoBehaviour {

	public AudioClip collapseSound;
	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Projectile"){
			source.PlayOneShot (collapseSound, 1f);
			float soundLength = 1f;
			Destroy (gameObject, soundLength);
		}
	}
}
