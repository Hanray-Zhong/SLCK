using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//example. moving between some points at random
[RequireComponent(typeof(PolyNavAgent))]
public class MoveBetween : MonoBehaviour{

	public List<Vector2> WPoints = new List<Vector2>();

	private PolyNavAgent _agent;
	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}

	void Start(){
		
		if (WPoints.Count != 0)
			agent.SetDestination(WPoints[ Random.Range(0, WPoints.Count) ]);
	}

	//Message from agent
	public void OnDestinationReached(){
		if (gameObject.GetComponent<EnemyAI>().target == null)
			agent.SetDestination(WPoints[Random.Range(0, WPoints.Count)]);
		else {
			agent.SetDestination(gameObject.GetComponent<EnemyAI>().target.transform.position);
		}
	}
	

	//Message from agent
	IEnumerator OnDestinationInvalid(){
		yield return new WaitForSeconds(2);
		agent.SetDestination(WPoints[Random.Range(0, WPoints.Count)]);
	}

	void OnDrawGizmosSelected(){
		for ( int i = 0; i < WPoints.Count; i++)
			Gizmos.DrawSphere(WPoints[i], 0.05f);			
	}
}
