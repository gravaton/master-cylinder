using UnityEngine;
using System.Collections;

public class MapLogic : MonoBehaviour {

	public GameObject moveobj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject == this.gameObject) {
					NavMeshAgent agent = moveobj.GetComponent<NavMeshAgent> ();
					agent.SetDestination (hit.point);
				}
			}
		}
	}
}
