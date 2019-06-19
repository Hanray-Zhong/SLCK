using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States {
	PatrolState,
	ChaseState,
}

public class EnemyAI : MonoBehaviour
{
[Header("Search State Paramaters")]
	public float searchRadius;
	[Header("State")]
	public States CurrentState = States.PatrolState;
	[Header("Others")]
	public GameObject Player;
	public PolyNavAgent NavAgent;
    public GameObject SearchEnemyCircle;
    [Header("Weapon")]
    public GameObject Bullet;
    public Gun Used_Gun;
    public GameObject[] ShootPos;
    public int ShootNumPerTime;
    public float ShootForce;
    public float BulletDamage;
    public float ShootCD;

	private LayerMask playerLayer;
    private LayerMask mapLayer;
	private GameObject target;

        private float AttackCD;
    private float _attackCD;

	private void Awake() {
		NavAgent = GetComponent<PolyNavAgent>();
		playerLayer = LayerMask.NameToLayer("Player");
        mapLayer = LayerMask.NameToLayer("Map");
	}

	private void Update() {
		CheckStateChange();
		RunFSM();
        CheckGun();
        Attack();
	}


	/**************** Simple FSM ****************/
	void CheckStateChange() {
		Collider2D col = Physics2D.OverlapCircle((Vector2)transform.position, searchRadius, 1 << playerLayer);
		if (CurrentState == States.PatrolState && col != null) {
            Vector2 SelfToTarget_Dir = (col.transform.position - transform.position).normalized; 
            float SelfToTarget_Dis = Vector2.Distance(col.transform.position, transform.position);
		    if (Physics2D.Raycast(transform.position, SelfToTarget_Dir, SelfToTarget_Dis, 1 << mapLayer)) 
                return;
            Debug.Log("玩家进入视线范围，发现玩家");
			GetComponent<MoveBetween>().StopAllCoroutines();
			target = col.gameObject;
            SearchEnemyCircle.SetActive(false);
			ChangeState(States.ChaseState);
		}
        if (GetComponent<PlayerUnit>().Health < 100) {
            Debug.Log("被击中，发现玩家");
			GetComponent<MoveBetween>().StopAllCoroutines();
			target = Player;
            SearchEnemyCircle.SetActive(false);
			ChangeState(States.ChaseState);
        }
		if (target == null && CurrentState == States.ChaseState) {
			Debug.Log("丢失玩家");
            SearchEnemyCircle.SetActive(true);
			ChangeState(States.PatrolState);
		}
	}
	void ChangeState(States nextState) {
		CurrentState = nextState;
	}
	void RunFSM() {
		if (CurrentState == States.PatrolState) {
			GetComponent<MoveBetween>().enabled = true;
		}
		if (CurrentState == States.ChaseState) {
			GetComponent<MoveBetween>().enabled = false;
			if (target != null) {
				NavAgent.SetDestination(target.transform.position);
			}
			else {
				Debug.Log("Error : The target is miss.");
			}
		}
	}
	/**************** Simple FSM ****************/

    void CheckGun() {
        switch (Used_Gun) {
            case (Gun)0: ShootNumPerTime = Rifle.ShootNumPerTime;
                    ShootForce = Rifle.ShootForce;
                    BulletDamage = Rifle.BulletDamage;
                    ShootCD = Rifle.ShootCD;
                    break;
            case (Gun)1: ShootNumPerTime = Sniper_rifle.ShootNumPerTime;
                    ShootForce = Sniper_rifle.ShootForce;
                    BulletDamage = Sniper_rifle.BulletDamage;
                    ShootCD = Sniper_rifle.ShootCD;
                    break;
            case (Gun)2: ShootNumPerTime = Shotgun.ShootNumPerTime;
                    ShootForce = Shotgun.ShootForce;
                    BulletDamage = Shotgun.BulletDamage;
                    ShootCD = Shotgun.ShootCD;
                    break;
            default: return;
        }
    }
    void Attack() {
        if (target != null) {
            Vector2 SelfToTarget_Dir = (target.transform.position - transform.position).normalized; 
            float SelfToTarget_Dis = Vector2.Distance(target.transform.position, transform.position);
		    if (Physics2D.Raycast(transform.position, SelfToTarget_Dir, SelfToTarget_Dis, 1 << mapLayer)) 
                return;
            NavAgent.rotateTransform = false;
            transform.up = SelfToTarget_Dir;
            Shoot();
        }
    }
     void Shoot() {
            AttackCD = ShootCD;
            if (_attackCD < AttackCD)
                _attackCD++;
            if (_attackCD >= AttackCD) {
                for (int i = 0; i < ShootNumPerTime; i++) {   
                    GameObject newBullet = Instantiate(Bullet, ShootPos[i].transform.position, ShootPos[i].transform.rotation);
                    newBullet.GetComponent<Bullet>().Damage = BulletDamage;
                    newBullet.GetComponent<Rigidbody2D>().AddForce(ShootPos[i].transform.up * ShootForce, ForceMode2D.Impulse);
                }
                _attackCD = 0;
            }
    }

	void OnDrawGizmos(){
    	Gizmos.color = new Color(1,0,0,1);
    	Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
