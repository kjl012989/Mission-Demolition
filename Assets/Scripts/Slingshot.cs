using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	static public Slingshot S;
	public GameObject prefabProjectile;
	public float velocityMult = 4f;
	public bool __________;

	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;

	public AudioClip ropeSound;	//Create slot for rope sound in script
	public AudioClip MissleSound;
	private AudioSource source;	//Create slot for audio in audiosource in component

	void Awake(){
		S = this;
		Transform launcPointTrans = transform.Find ("LaunchPoint");
		launchPos = launcPointTrans.position;

		source = GetComponent<AudioSource> ();	//Have source get AudioSource component
	}

	void OnMouseEnter(){
		print ("Slingshot:OnMouseEnter()");
		launchPoint.SetActive (true);
	}

	void OnMouseExit(){
		print ("Slingshot:OnMouseExit()");
		launchPoint.SetActive (false);
	}

	void OnMouseDown(){
		aimingMode = true;
		projectile = Instantiate (prefabProjectile) as GameObject;
		projectile.transform.position = launchPos;
		projectile.GetComponent<Rigidbody>().isKinematic = true;

		source.PlayOneShot (ropeSound, 1f);	//1st arg: the source, 2nd arg: volume
	}

	void Update(){
		if (!aimingMode)return;
		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
		Vector3 mouseDelta = mousePos3D - launchPos;
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;
		if(mouseDelta.magnitude > maxMagnitude){
			mouseDelta.Normalize();
			mouseDelta *= maxMagnitude;
		}
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if(Input.GetMouseButtonUp(0)){
			aimingMode = false;
			projectile.GetComponent<Rigidbody>().isKinematic = false;
			projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
			FollowCam.S.poi = projectile;
			projectile = null;
			MissionDemolition.ShotFired();

			source.PlayOneShot (MissleSound, 1f);
		}
	}
}
