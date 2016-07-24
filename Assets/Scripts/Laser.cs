using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public float damage = 100f;

	public float GetDamage(){
		return damage;
	}

	public void Hit(){
		Destroy (gameObject);
	}
}
