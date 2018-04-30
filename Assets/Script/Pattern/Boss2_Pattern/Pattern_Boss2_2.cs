using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Boss2_2 : PatternSequencer
{
    public Unit owner;

    public float patternDuration;
    private float remainPatternDuration;

    public float activateDelay;
    private float remainActivateDelay;

    private int count;


    public LaserBodyBullet laserPrefab;
    public LaserBodyBullet laserRootPrefab;
    public LaserBodyBullet laserWaitPrefab;
    LaserGenerator laser = new LaserGenerator(); // 레이저

    public Boss2_illusion illusionPrefab;

    protected override void Start()
    {
        owner = GetComponent<Unit>();

        if (owner == null)
            Destroy(this);
    }

    protected override IEnumerator PatternFlow()
    {
        isPatternRunning = true;

        Boss2 boss = owner as Boss2; // onwer를 보스2로

        remainPatternDuration = patternDuration;


        Boss2_illusion illusion;

        Vector2 randomPos = new Vector2();
        Vector2 prevPos = new Vector2();
        Vector2 prevPrevPos = new Vector2();

        float worldHeight = Camera.main.orthographicSize * 2f; // 카메라의 세로 반지름 두배
        float worldWidth = worldHeight * Camera.main.aspect; // 카메라 세로 비율로 가로 비율 추출

        while (remainPatternDuration >= 0f)
        {
            remainPatternDuration -= Time.fixedDeltaTime;

            if(remainActivateDelay > 0)
            {
                remainActivateDelay -= Time.fixedDeltaTime;
            }
            else
            {
                boss.pattern2On = true;

                remainActivateDelay = activateDelay;

                int limit = 100;
                do
                {
                    float randomX = Random.Range(-0.45f, 0.45f); // 범위 설정
                    float randomY = Random.Range(-0.45f, 0.45f);

                    float spawnPositionX = randomX * worldWidth; // 두개 범위로 지정해서 윗부분 출현 좌표x
                    float spawnPositionY = randomY * worldHeight; //y 

                    randomPos = new Vector2(spawnPositionX, spawnPositionY);

                    --limit;

                } while ((Vector2.Distance(randomPos, PlayerScript.player.transform.position) < 5f ||
                    Vector2.Distance(prevPos, randomPos) < 5f ||
                    Vector2.Distance(prevPrevPos, randomPos) < 5f) &&
                    limit > 0);

                prevPrevPos = prevPos;
                prevPos = randomPos;


                if (count % 3 == 0)
                {
                    owner.transform.position = randomPos;
                    owner.transform.eulerAngles = new Vector3(0f, 0f, MyMath.GetDirection(owner, PlayerScript.player) - 90f);

                    laser.position = this.transform.position;
                    laser.direction = this.transform.eulerAngles.z + 90f;
                    laser.count = 50;
                    laser.laserPrefab = laserPrefab;
                    laser.laserRootPrefab = laserRootPrefab;
                    laser.laserWaitPrefab = laserWaitPrefab;
                    laser.scale = 2f;
                    laser.distance = -1.5f;
                    laser.preDelay = 1f;

                    StartCoroutine(laser.GenerateLaser());
                    StartCoroutine(AlpahBlend(laser.preDelay));
                }
                else
                {
                    illusion = Instantiate<Boss2_illusion>(illusionPrefab);

                    illusion.transform.position = randomPos;
                    illusion.transform.eulerAngles = new Vector3(0f, 0f, MyMath.GetDirection(illusion, PlayerScript.player) - 90f);
                }

                ++count;
            }

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(2f);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        boss.pattern2On = false;

        yield return new WaitForSeconds(9f);

        isPatternRunning = false;

    }

    IEnumerator AlpahBlend(float duration)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float remainDuration = duration;
        float alpha = 0f;

        while (remainDuration > 0f)
        {
            remainDuration -= Time.fixedDeltaTime;

            alpha += Time.fixedDeltaTime / duration;

            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            yield return new WaitForFixedUpdate();
        }

        remainDuration = duration;

        while (remainDuration > 0f)
        {
            remainDuration -= Time.fixedDeltaTime;

            alpha -= Time.fixedDeltaTime / duration;

            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            yield return new WaitForFixedUpdate();
        }

        if(isPatternRunning == false)
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}