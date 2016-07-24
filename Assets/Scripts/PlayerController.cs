using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float health = 500f;
	public GameObject laser;
	public float firingRate = 0.2f;
	public AudioClip sound;

	//used to set the speed
	float speed = 15.0f;
	float xmin;
	float xmax;
	float ymin;
	float ymax;

	//padding
	float padding = .5f;

	void Die(){
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		man.LoadLevel ("Win Screen");
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftX = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightX = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		Vector3 UpY = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 DownY = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, distance));
		xmin = leftX.x + padding;
		xmax = rightX.x - padding;
		ymin = UpY.y + padding;
		ymax = DownY.y - padding;
	}

	void Fire(){
		GameObject beam = Instantiate (laser, new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, speed, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.A)) {
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		}else if (Input.GetKey (KeyCode.D)) {
			this.transform.position += new Vector3 (0.75f * speed * Time.deltaTime, 0f, 0f);
		} else if (Input.GetKey (KeyCode.W)) {
			this.transform.position += new Vector3 (0f, 0.75f * speed * Time.deltaTime, 0f);
		} else if (Input.GetKey (KeyCode.S)) {
			this.transform.position += new Vector3 (0f, -0.75f * speed * Time.deltaTime, 0f);
		} if (Input.GetKeyDown (KeyCode.J)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
			AudioSource.PlayClipAtPoint (sound, transform.position);
		} if (Input.GetKeyUp (KeyCode.J)) {
			CancelInvoke ("Fire");
		} 

		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp (transform.position.y, ymin, ymax);
		this.transform.position = new Vector3 (newX, newY, transform.position.z);

	}

	void OnTriggerEnter2D(Collider2D col){
		Laser missile = col.gameObject.GetComponent<Laser> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Die ();
			}
			//Debug.Log ("Hit by a projectile");
		}
	}

}
