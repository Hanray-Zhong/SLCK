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
    public Gun Used_Gun;
    public GameObject[] ShootPos;
    public int ShootNumPerTime;
    public float ShootForce;
    public float BulletDamage;
    public float ShootCD;
    [Header("Different Gun")]
    public int Shotgun_Number;
    public int Sniper_rifle_Number;
    public GameObject Sniper_rifle_Line;
    [Header("Gun Sound")]
    public AudioSource[] Sounds;
    private AudioSource currentSound;
    [Header("Knife")]
    public bool UseKnife;
    public GameObject Knife;
    public float KnifeCD;
    [Header("Score")]
    public int Score;
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
    void FixedUpdate()
    {
        Move();
        Rotation();
        CheackGun();
        Shoot();
        ChangeWeapon();
        ChangeGun();
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
    void CheackGun() {
        switch (Used_Gun) {
            case (Gun)0: ShootNumPerTime = Rifle.ShootNumPerTime;
                    ShootForce = Rifle.ShootForce;
                    BulletDamage = Rifle.BulletDamage;
                    ShootCD = Rifle.ShootCD;
                    Sniper_rifle_Line.SetActive(false);
                    currentSound = Sounds[0];
                    break;
            case (Gun)1: ShootNumPerTime = Sniper_rifle.ShootNumPerTime;
                    ShootForce = Sniper_rifle.ShootForce;
                    BulletDamage = Sniper_rifle.BulletDamage;
                    ShootCD = Sniper_rifle.ShootCD;
                    Sniper_rifle_Line.SetActive(true);
                    currentSound = Sounds[1];
                    break;
            case (Gun)2: ShootNumPerTime = Shotgun.ShootNumPerTime;
                    ShootForce = Shotgun.ShootForce;
                    BulletDamage = Shotgun.BulletDamage;
                    ShootCD = Shotgun.ShootCD;
                    Sniper_rifle_Line.SetActive(false);
                    currentSound = Sounds[2];
                    break;
            default: return;
        }
    }
    void Shoot() {
        if (gameInput == null) 
            return;
        if (UseKnife == false) {
            AttackCD = ShootCD;
            if (_attackCD < AttackCD)
                _attackCD++;
            if (gameInput.GetInputInteraction_1() > 0 && _attackCD >= AttackCD) {
                if (Used_Gun == Gun.Shotgun) {
                    if (Shotgun_Number <= 0) {
                        Sounds[3].Play();
                        _attackCD = 0;
                        return;
                    }
                    Shotgun_Number -= 3;
                }
                if (Used_Gun == Gun.Sniper_rifle) {
                    if (Sniper_rifle_Number <= 0) {
                        Sounds[3].Play();
                        _attackCD = 0;
                        return;
                    }
                    Sniper_rifle_Number -= 1;
                }
                for (int i = 0; i < ShootNumPerTime; i++) {   
                    GameObject newBullet = Instantiate(Bullet, ShootPos[i].transform.position, ShootPos[i].transform.rotation);
                    newBullet.GetComponent<Bullet>().Damage = BulletDamage;
                    newBullet.GetComponent<Rigidbody2D>().AddForce(ShootPos[i].transform.up * ShootForce, ForceMode2D.Impulse);
                    if (currentSound != null)
                        currentSound.Play();
                }
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
    void ChangeGun() {
        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            Used_Gun = Gun.Rifle;
            Sounds[4].Play();
            if (UseKnife == true) {
                UseKnife = false;
                Knife.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2)) {
            Used_Gun = Gun.Shotgun;
            Sounds[4].Play();
            if (UseKnife == true) {
                UseKnife = false;
                Knife.SetActive(false);
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha3)) {
            Used_Gun = Gun.Sniper_rifle;
            Sounds[4].Play();
            if (UseKnife == true) {
                UseKnife = false;
                Knife.SetActive(false);
            }
        }
    }
}
