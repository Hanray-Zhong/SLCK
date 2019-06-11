using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controller")]
    public float Speed;
    public GameInput gameInput;
    [Header("Weapon")]
    public GameObject Bullet;
    public GameObject ShootPos;
    public float ShootForce;
    public float ShootCD;
    [Header("Knife")]
    public bool UseKnife;
    public GameObject Knife;
    public float KnifeCD;
    private Transform _transf;
    private Rigidbody2D _rigidbody;
    private float AttackCD;
    private float _attackCD;
    void Start()
    {
        _transf = gameObject.GetComponent<Transform>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        AttackCD = ShootCD;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
        Shoot();
        ChangeWeapon();
    }

    void Move() {
        if (gameInput == null)
            return;
        Vector2 moveDir = gameInput.GetMoveDir();
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(moveDir * Speed * Time.deltaTime);
    }
    void Rotation() {
        if (gameInput == null)
            return;
        _transf.up = gameInput.GetForward();
    }
    void Shoot() {
        if (gameInput == null) 
            return;
        if (UseKnife == false) {
            AttackCD = ShootCD;
            if (_attackCD < AttackCD)
                _attackCD++;
            if (gameInput.GetInputInteraction_1() > 0 && _attackCD >= AttackCD) {
                GameObject newBullet = Instantiate(Bullet, ShootPos.transform.position, ShootPos.transform.rotation);
                newBullet.GetComponent<Rigidbody2D>().AddForce(ShootPos.transform.up * ShootForce, ForceMode2D.Impulse);
                _attackCD = 0;
            }
        }
        else {
            if (gameInput.GetInputInteraction_1() > 0)
                Knife.GetComponent<Animator>().SetBool("Attack", true);
        }
    }
    void ChangeWeapon() {
        if (Input.GetKeyUp(KeyCode.E)) {
            if (UseKnife == true) {
                UseKnife = false;
                Knife.SetActive(false);
            }
            else {
                UseKnife = true;
                Knife.SetActive(true);
            }
        }
    }
}
