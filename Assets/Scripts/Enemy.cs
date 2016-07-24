using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject enemy;
	public float width = 18;
	public float height = 8;
	public float speed = 15.0f;
	public float spawnDelay = 0.5f;

	private bool movingRight = false;
	private float xmin;
	private float xmax;
	private float leftPadding = 5f;
	private float rightPadding = 9f;

	// Use this for initialization
	void Start () {
		float cameraDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBound = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, cameraDistance));
		Vector3 rightBound = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0f, cameraDistance));
		xmin = leftBound.x + leftPadding;
		xmax = rightBound.x - rightPadding;

	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += new Vector3(0.25f * speed * Time.deltaTime, 0f, 0f);
			if (transform.position.x >= xmax) {
				movingRight = false;
			}
		}

		else {
			transform.position -= new Vector3(0.25f * speed * Time.deltaTime, 0f, 0f);
			if (transform.position.x <= xmin) {
				movingRight = true;
			}
		}

		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}

	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	void Spawn(){
		foreach (Transform child in transform) {
			GameObject e = Instantiate (enemy, child.transform.position, Quaternion.identity) as GameObject;
			e.transform.parent = child;
		}
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject e = Instantiate (enemy, freePosition.position, Quaternion.identity) as GameObject;
			e.transform.parent = freePosition;
		}
		//recursion
		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
}
