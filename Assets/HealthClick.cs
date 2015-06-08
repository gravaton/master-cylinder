using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class HealthClick : MonoBehaviour {

	public int health;
	public Text healthText;
	public float fireRate = 4.0f;
	public Image prefabBar;
	public GameObject prefabBullet;

	// Private things
	private GameObject target;
	private float fireCooldown;

	private Image healthBar;
	// Use this for initialization
	void Start () {
		healthText.text = "Health: " + health.ToString ();
		healthBar = Instantiate (prefabBar);
		healthBar.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		healthBar.GetComponent<Image>().fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] surroundingPlayers = Physics.OverlapSphere (this.transform.position, 10).Where (c => c.CompareTag ("Player") && (c.gameObject != this.gameObject)).ToArray<Collider>();
		if (surroundingPlayers.Length > 0) {
			NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
			if( agent != null) { 
				if (agent.hasPath) { 
					agent.Stop ();
					agent.ResetPath ();
				}
			}
			target = surroundingPlayers[0].gameObject;
			if(fireCooldown < 0) {
				this.ShootTarget();
				fireCooldown = fireRate;
			} else {
				fireCooldown = fireCooldown - Time.deltaTime;
			}
		}

	}

	void LateUpdate() {
		Vector3 pos = this.transform.position;
		Vector3 screenpos = Camera.main.WorldToScreenPoint (pos);
		screenpos.y = screenpos.y + 50;
		healthBar.transform.position = screenpos;
	}

	void OnMouseDown() {
		Debug.Log ("OnMouseDown hit");
		//transform.Translate ((Vector3.right * 3) * Time.deltaTime);
		Vector3 bulletspawn = (target.transform.position - this.transform.position).normalized;
		healthText.text = "I am at: " + this.transform.position + " and spawning bullet with direction " + bulletspawn.ToString () + " and abs " + (this.transform.position + bulletspawn).ToString ();
		GameObject bullet = (GameObject)Instantiate (prefabBullet, (this.transform.position + bulletspawn), Quaternion.identity);
		bullet.GetComponent<Bullet> ().target = target;
	}

	public void TakeDamage(int hp) {
		this.health = this.health - hp;
		if (this.health < 0) {
			this.health = 0;
		}
		//healthText.text = "Position: " + this.transform.position.ToString ();
		healthBar.GetComponent<Image> ().fillAmount = (this.health / 120.0f);
	}

	private void ShootTarget() {
		if (this.target != null) {
			Vector3 bulletspawn = (this.target.transform.position - this.transform.position).normalized;
			GameObject bullet = (GameObject)Instantiate (prefabBullet, (this.transform.position + bulletspawn), Quaternion.identity);
			bullet.GetComponent<Bullet> ().target = target;
		}
	}
}
