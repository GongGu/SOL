using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part_DefencePanel : Part
{
    public Unit owner;
    public float distance;
    public float rotateSpeed;
    public float direction;

    private void Start()
    {
        owner = PlayerScript.player; // 주인은 바로 플레이어!
    }

    private void FixedUpdate()
    {
        // owner 주변 떠다니는 코드
        direction += rotateSpeed * Time.fixedDeltaTime; // 매 프레임마다 회전속도를 디렉션에 더해줌
        Vector2 deltaPos = MyMath.DirectionToVector2(direction) * distance;
        this.transform.position = owner.transform.position + (Vector3)deltaPos;
    }
}
