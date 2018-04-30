using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Boss2_1 : PatternSequencer
{
    public Unit owner;

    public Bullet bulletPrefab;
    public float patternDuration;
    private float remainPatternDuration;

    public float maxMoveSpeed;
    public float accel;
    private float currentMoveSpeed;

    public float fireDelay;
    private float remainFireDelay;

    private bool isFireRight;

    protected override void Start()
    {
        owner = GetComponent<Unit>();

        if (owner == null)
            Destroy(this);
    }

    protected override IEnumerator PatternFlow()
    {
        isPatternRunning = true;

        remainPatternDuration = patternDuration;
        owner.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        float maxY = Camera.main.orthographicSize; // 카메라의 세로 반지름 두배

        while (remainPatternDuration >= 0f)
        {
            remainPatternDuration -= Time.fixedDeltaTime;

            float targetXPos = PlayerScript.player.transform.position.x;
            float currentXPos = owner.transform.position.x;
            currentMoveSpeed += accel * Mathf.Sign(targetXPos - currentXPos) * Time.fixedDeltaTime;

            if (currentMoveSpeed > maxMoveSpeed)
                currentMoveSpeed = maxMoveSpeed;
            else if (currentMoveSpeed < -maxMoveSpeed)
                currentMoveSpeed = -maxMoveSpeed;
                
            float deltaXPos = currentMoveSpeed * Time.fixedDeltaTime;

            owner.transform.position = new Vector3(currentXPos + deltaXPos, maxY - 2f);


            if (remainFireDelay >= 0f)
            {
                remainFireDelay -= Time.fixedDeltaTime;
            }
            else
            {
                remainFireDelay = fireDelay;
                Fire();
            }

            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(10f);
        
        isPatternRunning = false;
        

    }


    void Fire()
    {
        
    
        PatternCircle circle = new PatternCircle();
        circle.owner = owner;
        circle.bulletPrefab = bulletPrefab;
        circle.position = owner.transform.position;
        circle.count = 6;
        circle.delay = 0.05f;
        circle.bulletSpeed = 6f;
        circle.distance = 2f;

        if(isFireRight == true)
        {
            circle.angle = 180f;
            circle.direction = 270f;
        }
        else
        {
            circle.angle = -180f;
            circle.direction = 270f;
        }
        isFireRight = !isFireRight;
       

        StartCoroutine(circle.PatternFramework(null));
    }



}