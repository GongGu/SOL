using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : LivingUnit
{
    public static PlayerScript player;

    private float currentPlayerSpeed;
    public float originPlayerSpped;

    public float immuneDuration;

    public float fireDelay;
    private float remainFireDelay;

    public float subFireDelay;
    private float remainSubFireDelay;

    public Bullet bulletPrefab;
    public AllySubBullet1 subBulletPrefab;

    public AllySubBullet2 subBullet2Prefab;

    private AllySubBullet1 spawnedSubBullet;

    private List<AllySubBullet2> spawnedSub2BulletList = new List<AllySubBullet2>();

    private Dictionary<KeyCode, bool> isPressDic = new Dictionary<KeyCode, bool>(); // 1:1 키 대응

    public SubWeaponType currentWeapon;

    public bool isSturn;

    public enum SubWeaponType
    {
        SUB_1,
        SUB_2,
    }

    private bool hasSubBullet;

    private bool isImmune = false;
    public bool IsImmune
    {
        get
        {
            return isImmune;
        }
        set
        {
            isImmune = value;

            if (isImmune == true)
            {
                StartCoroutine(ReleaseImmune());

                GetComponent<SpriteRenderer>().color = new Color(
                    GetComponent<SpriteRenderer>().color.r,
                    GetComponent<SpriteRenderer>().color.g,
                    GetComponent<SpriteRenderer>().color.b,
                    0.6f);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(
                    GetComponent<SpriteRenderer>().color.r,
                    GetComponent<SpriteRenderer>().color.g,
                    GetComponent<SpriteRenderer>().color.b,
                    1f);
            }
        }
    }

    private void Awake()
    {
        ApplyGameSetting();

        player = this;
        currentPlayerSpeed = originPlayerSpped;

        isPressDic.Add(KeyCode.W, false);
        isPressDic.Add(KeyCode.A, false);
        isPressDic.Add(KeyCode.S, false);
        isPressDic.Add(KeyCode.D, false);

        isPressDic.Add(KeyCode.Mouse0, false);
        isPressDic.Add(KeyCode.Mouse1, false);
    }

    private void Update()
    {
        isPressDic[KeyCode.W] = Input.GetKey(KeyCode.W);
        isPressDic[KeyCode.A] = Input.GetKey(KeyCode.A);
        isPressDic[KeyCode.S] = Input.GetKey(KeyCode.S);
        isPressDic[KeyCode.D] = Input.GetKey(KeyCode.D);

        isPressDic[KeyCode.Mouse0] = Input.GetKey(KeyCode.Mouse0);
        isPressDic[KeyCode.Mouse1] = Input.GetKey(KeyCode.Mouse1);
    }

    void FixedUpdate()
    {
        if (isSturn == true)
            return;

        PlayerMove();

        PlayerRotate();

        BulletFire();

        Boom();
    }

    void PlayerMove() // 플레이어 움직이는 함수
    {
        if (isPressDic[KeyCode.W] == true)
        {
            transform.position = transform.position + (new Vector3(0, 1, 0) * currentPlayerSpeed * Time.fixedDeltaTime);
        }
        if (isPressDic[KeyCode.S] == true)
        {
            transform.position = transform.position + (new Vector3(0, -1, 0) * currentPlayerSpeed * Time.fixedDeltaTime);
        }
        if (isPressDic[KeyCode.A] == true)
        {
            transform.position = transform.position + (new Vector3(-1, 0, 0) * currentPlayerSpeed * Time.fixedDeltaTime);
        }
        if (isPressDic[KeyCode.D] == true)
        {
            transform.position = transform.position + (new Vector3(1, 0, 0) * currentPlayerSpeed * Time.fixedDeltaTime);
        }
    }

    void PlayerRotate() // 플레이어 각도 회전함수
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dir = MyMath.GetDirection(mouseWorldPosition, (Vector3)this.transform.position);
        this.transform.eulerAngles = new Vector3(0, 0, dir + 90f); //오일러 앵글 쿼터니언과 다른 각도 
    }

    void BulletFire()
    {
        if (isPressDic[KeyCode.Mouse0] == true) // 마우스 0 은 좌클릭
        {
            if (remainFireDelay < 0f)
            {
                Bullet bullet = Instantiate<Bullet>(bulletPrefab); //
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 씬의 좌표랑 마우스 좌표를 통일해주고 Vector3에 저장

                bullet.direction = MyMath.GetDirection(this.transform.position, mouseWorldPosition);
                bullet.transform.position = this.transform.position; // 총알 위치를 캐릭터 위치랑 같게 캐릭터에서 총알이 나가니깐
                bullet.transform.eulerAngles = this.transform.eulerAngles; //오일러앵글 이용해서 각도 마우스 방향으로 각도 조정

                remainFireDelay = fireDelay;
            }
            else
            {
                remainFireDelay -= Time.fixedDeltaTime;
            }
        }


        switch(currentWeapon)
        {
            case SubWeaponType.SUB_1:
                {
                    // 스킬 1 차징총알
                    if (isPressDic[KeyCode.Mouse1] == true && hasSubBullet == false)
                    {
                        spawnedSubBullet = Instantiate(subBulletPrefab);
                        spawnedSubBullet.owner = this; // AllySubBullet1 의 주인은 이놈이다
                        spawnedSubBullet.transform.position = this.transform.position;

                        hasSubBullet = true;
                    }
                    if (isPressDic[KeyCode.Mouse1] == true && hasSubBullet == true)
                    {
                        currentPlayerSpeed = originPlayerSpped * 0.5f;
                    }
                    else
                    {
                        currentPlayerSpeed = originPlayerSpped;
                    }

                    if (isPressDic[KeyCode.Mouse1] == false && hasSubBullet == true)
                    {
                        spawnedSubBullet.isCharge = false;

                        hasSubBullet = false;
                    }
                }
                break;
            case SubWeaponType.SUB_2:
                {
                    if (isPressDic[KeyCode.Mouse1] == true && hasSubBullet == false)
                    {
                        spawnedSub2BulletList.Clear();

                        hasSubBullet = true;
                    }
                        // 스킬 1 차징총알
                    if (isPressDic[KeyCode.Mouse1] == true)
                    {
                        if (remainSubFireDelay < 0f)
                        {
                            remainSubFireDelay = subFireDelay;

                            AllySubBullet2 bullet = Instantiate(subBullet2Prefab);

                            spawnedSub2BulletList.Add(bullet);

                            bullet.transform.position = transform.position;
                        }
                        else
                        {
                            remainSubFireDelay -= Time.fixedDeltaTime;
                        }
                    }
                    if(isPressDic[KeyCode.Mouse1] == false && hasSubBullet == true)
                    {
                        hasSubBullet = false;

                        for(int i = 0; i < spawnedSub2BulletList.Count;++i)
                        {
                            spawnedSub2BulletList[i].isCharge = false;

                            spawnedSub2BulletList[i].direction = transform.eulerAngles.z + 90f + Random.Range(-30f, 30f);

                            spawnedSub2BulletList[i].bulletSpeed = Random.Range(9f, 11f);
                        }
                    }
                    break;
                }
        }

    }

    void Boom()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].CurrentHP = 0;
            }
        }
    }

    private IEnumerator ReleaseImmune()
    {
        yield return new WaitForSeconds(immuneDuration);

        IsImmune = false;
    }

    public void BeHit(Unit attackUnit)
    {
        float hitDirection = MyMath.GetDirection(attackUnit, this);

        StartCoroutine(SturnProcess(hitDirection));
    }

    IEnumerator SturnProcess(float hitDirection)
    {
        isSturn = true;

        float maxDuration = 0.3f;
        float duration = maxDuration;

        float maxSpd = 10f;
        float spd = maxSpd;

        while (duration > 0f)
        {
            duration -= Time.fixedDeltaTime;

            spd -= maxSpd * Time.fixedDeltaTime / maxDuration;

            Vector2 deltaPos = MyMath.DirectionToVector2(hitDirection) * spd * Time.fixedDeltaTime;

            transform.position = transform.position + (Vector3)deltaPos;

            yield return new WaitForFixedUpdate();
        }

        isSturn = false;
    }


    private void ApplyGameSetting()
    {
        switch (GameSetting.gameSetting.subWeapon)
        {
            case GameSetting.SubWeapon.A:
                currentWeapon = SubWeaponType.SUB_1;
                break;

            case GameSetting.SubWeapon.B:
                currentWeapon = SubWeaponType.SUB_2;
                break;
        }


        switch (GameSetting.gameSetting.defenceModule)
        {
            case GameSetting.DefenceModule.A:
                GetComponent<Shield>().enabled = true;
                FindObjectOfType<Part_DefencePanel>().gameObject.SetActive(false);
                break;

            case GameSetting.DefenceModule.B:
                GetComponent<Shield>().enabled = false;
                FindObjectOfType<Part_DefencePanel>().gameObject.SetActive(true);
                break;
        }

        switch (GameSetting.gameSetting.playerPlane)
        {
            case GameSetting.PlayerPlane.A:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Player1");
                break;
            case GameSetting.PlayerPlane.B:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Player2_0");
                gameObject.AddComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprite/Anim/Player2_0") as RuntimeAnimatorController;
                break;
        }
    }

}
