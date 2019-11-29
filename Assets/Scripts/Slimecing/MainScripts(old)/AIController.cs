using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

	public Transform target;
	public Transform target2;
	public float dist;
	NavMeshAgent enemyNav;

	private bool atTarget1 = false;
	void Start () {
		enemyNav = GetComponent<NavMeshAgent>();
	}

	void Update () {
		if(atTarget1){
			enemyNav.SetDestination(target2.position);
		}
		if(!atTarget1){
			enemyNav.SetDestination(target.position);
		}
		if(!atTarget1){
			dist = Vector3.Distance(target.position, transform.position);
		}if(atTarget1)
		{
			dist = Vector3.Distance(target2.position, transform.position);
		}
		if(dist < 1f) {
			atTarget1 = !atTarget1;
		}
	}
}
