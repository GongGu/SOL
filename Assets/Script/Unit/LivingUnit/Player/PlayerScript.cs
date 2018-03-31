using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : LivingUnit
{
    public static PlayerScript player;

    public float playerSpeed;

    public float immuneDuration;

    public float fireDelay;
    private float remainFireDelay;

    public Bullet bulletPrefab;

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
                    0.2f);
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
        player = this;
    }

    void FixedUpdate()
    {

        PlayerMove();

        PlayerRotate();

        BulletFire();

        Boom();
    }

    void PlayerMove() // 플레이어 움직이는 함수
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + (new Vector3(0, 1, 0) * playerSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + (new Vector3(0, -1, 0) * playerSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + (new Vector3(-1, 0, 0) * playerSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + (new Vector3(1, 0, 0) * playerSpeed * Time.fixedDeltaTime);
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
        if (Input.GetKey(KeyCode.Mouse0)) // 마우스 0 은 좌클릭
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
}
