using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public float health = 150f;
	public GameObject beam;
	public float projectileSpeed = 10f;
	public float shotsPerSecond = 0.7f;
	public int scoreValue = 150;
	public AudioClip sound;
	public AudioClip dead;
	public Sprite[] sprites;

	private ScoreKeeper scoreKeeper;
	private int timesHit;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		timesHit = 0;
	}

	void EnemyFire(){
		AudioSource.PlayClipAtPoint (sound, transform.position);
		GameObject missile = Instantiate (beam, new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z), Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -projectileSpeed);
	}

	void Update(){
		float prob = shotsPerSecond * Time.deltaTime;
		//Random.value ranges from 0f to 1f
		if (Random.value < prob) {
			EnemyFire ();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		Laser missile = col.gameObject.GetComponent<Laser> ();
		if (missile) {
			timesHit = 1;
			this.GetComponent<SpriteRenderer> ().sprite = sprites [timesHit];
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Destroy (gameObject);
				timesHit = 0;
				AudioSource.PlayClipAtPoint (dead, transform.position);
				scoreKeeper.Score (scoreValue);
			}
			//Debug.Log ("Hit by a projectile");
		}
	}
}
