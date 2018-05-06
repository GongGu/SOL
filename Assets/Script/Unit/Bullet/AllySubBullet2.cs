using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySubBullet2 : Bullet
{
    public Vector2 v2Speed;
    float accel;

    public bool isCharge = true;

    private void Start()
    {
        accel = Random.Range(70f, 75f);
    }

    new private void FixedUpdate()
    {
        if(isCharge == false)
        {
            transform.position = (Vector2)transform.position + MyMath.DirectionToVector2(direction) * Time.fixedDeltaTime * bulletSpeed;

            return;
        }

        float distance = Vector2.Distance(this.transform.position, PlayerScript.player.transform.position);

        direction = MyMath.GetDirection(this, PlayerScript.player);

        if (distance > 1.0f)
        {
            float rand = Random.Range(-30f, 30f);

            Vector2 energy = (MyMath.DirectionToVector2(direction + rand)) * accel * Time.fixedDeltaTime;

            v2Speed = v2Speed * 0.5f + energy * 4.5f;

            //float speed = Vector2.Distance(v2Speed, new Vector2());
            //v2Speed = v2Speed.normalized * Mathf.Max(speed, Random.Range(14f, 16f));
        }
        else if(distance < 0.5f)
        {
;//            v2Speed = v2Speed * 0.95f + energy * 1.05f;
        }
        else
        {
            Vector2 energy = MyMath.DirectionToVector2(direction) * accel * Time.fixedDeltaTime;

            v2Speed += energy;
        }

        transform.position = (Vector2)transform.position + Time.fixedDeltaTime * v2Speed;
    }
}
