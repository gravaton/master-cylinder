using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float timeToHit = 2f;
	public GameObject target = null;

	private float curTime;
	// Use this for initialization
	void Start () {
		curTime = timeToHit;
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null) {
			Vector3 vec = (target.transform.position - this.transform.position);
			transform.Translate (vec * (Time.deltaTime / curTime));
			curTime = curTime - Time.deltaTime;
		}
	}
	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			other.SendMessage ("TakeDamage", 35, SendMessageOptions.DontRequireReceiver);
			Destroy (this.gameObject);
		}
	}
}
