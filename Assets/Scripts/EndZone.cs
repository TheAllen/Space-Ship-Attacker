using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour {
	//private Laser laser;

	// Use this for initialization
	void Start () {
		//laser = GameObject.FindObjectOfType<Laser> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collision){
		//Destroy (laser);
		Destroy(collision.gameObject);
	}
}
