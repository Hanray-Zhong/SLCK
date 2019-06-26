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
	public float SearchRadius;
    public float AttackRadius;
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
    [Header("Sound")]
    public AudioSource Sound;

	private LayerMask playerLayer;
    private LayerMask enemyLayer;
    private LayerMask mapLayer;
	public GameObject target;
    // private Vector3 TargetLastPos;

    private float AttackCD;
    private float _attackCD;
    private bool haveTarget = false;
    // 判定是否被击中去找玩家
    public bool beHit = false;

	private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
		NavAgent = GetComponent<PolyNavAgent>();
		playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        mapLayer = LayerMask.NameToLayer("Map");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
        Sound = gameObject.GetComponent<AudioSource>();
	}

	private void FixedUpdate() {
		CheckStateChange();
		RunFSM();
        CheckGun();
        Attack();
	}


	/**************** Simple FSM ****************/
	void CheckStateChange() {
		Collider2D col = Physics2D.OverlapCircle((Vector2)transform.position, SearchRadius, 1 << playerLayer);
		if (CurrentState == States.PatrolState && col != null) {
            Vector2 SelfToTarget_Dir = (col.transform.position - transform.position).normalized; 
            float SelfToTarget_Dis = Vector2.Distance(col.transform.position, transform.position);
		    if (Physics2D.Raycast(transform.position, SelfToTarget_Dir, SelfToTarget_Dis, 1 << mapLayer)) 
                return;
			GetComponent<MoveBetween>().StopAllCoroutines();
			target = col.gameObject;
            // TargetLastPos = target.transform.position;
            SearchEnemyCircle.SetActive(false);
			ChangeState(States.ChaseState);
		}
        if (beHit) {
			GetComponent<MoveBetween>().StopAllCoroutines();
            target = Player;
            // TargetLastPos = target.transform.position;
            SearchEnemyCircle.SetActive(false);
			ChangeState(States.ChaseState);
            beHit = false;
        }
		if (target == null && CurrentState == States.ChaseState) {
            haveTarget = false;
            SearchEnemyCircle.SetActive(true);
			ChangeState(States.PatrolState);
		}
	}
	void ChangeState(States nextState) {
		CurrentState = nextState;
	}
	void RunFSM() {
		if (CurrentState == States.PatrolState) {
			// 
		}
		if (CurrentState == States.ChaseState) {
            Vector2 SelfToTarget_Dir = (target.transform.position - transform.position).normalized; 
            float SelfToTarget_Dis = Vector2.Distance(target.transform.position, transform.position);
            if (!Physics2D.Raycast(transform.position, SelfToTarget_Dir, SelfToTarget_Dis, 1 << mapLayer)) {
                NavAgent.SetDestination(target.transform.position);
            }
			if (target != null && !haveTarget) {
				NavAgent.SetDestination(target.transform.position);
                haveTarget = true;
			}
			else {
				// Debug.Log("Error : The target is miss.");
			}
		}
	}
	/**************** Simple FSM ****************/

    void CheckGun() {
        switch (Used_Gun) {
            case (Gun)0: ShootNumPerTime = Rifle.ShootNumPerTime;
                    ShootForce = Rifle.ShootForce;
                    BulletDamage = Rifle.BulletDamage;
                    ShootCD = Rifle.ShootCD * 2;
                    break;
            case (Gun)1: ShootNumPerTime = Sniper_rifle.ShootNumPerTime;
                    ShootForce = Sniper_rifle.ShootForce;
                    BulletDamage = Sniper_rifle.BulletDamage;
                    ShootCD = Sniper_rifle.ShootCD;
                    break;
            case (Gun)2: ShootNumPerTime = Shotgun.ShootNumPerTime;
                    ShootForce = Shotgun.ShootForce;
                    BulletDamage = Shotgun.BulletDamage;
                    ShootCD = Shotgun.ShootCD * 2;
                    break;
            default: return;
        }
    }
    void Attack() {
        if (target != null) {
            Vector2 SelfToTarget_Dir = (target.transform.position - transform.position).normalized;
            float SelfToTarget_Dis = Vector2.Distance(target.transform.position, transform.position);
		    if (SelfToTarget_Dis > AttackRadius || Physics2D.Raycast(transform.position, SelfToTarget_Dir, SelfToTarget_Dis, 1 << mapLayer)) {
                NavAgent.rotateTransform = true;
                return;
            }
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
                    newBullet.GetComponent<BulletEnemy>().Damage = BulletDamage;
                    newBullet.GetComponent<Rigidbody2D>().AddForce(ShootPos[i].transform.up * ShootForce, ForceMode2D.Impulse);
                    if (Sound != null)
                        Sound.Play();
                }
                _attackCD = 0;
            }
    }

	void OnDrawGizmosSelected() {
    	Gizmos.color = new Color(1,0,0,1);
    	Gizmos.DrawWireSphere(transform.position, SearchRadius);
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
}
