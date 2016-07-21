using UnityEngine;
using System.Collections;

public class MetalWalls : MonoBehaviour {

	public AudioClip metalSound;
	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Projectile"){
			source.PlayOneShot (metalSound, 1f);
		}
	}
}
