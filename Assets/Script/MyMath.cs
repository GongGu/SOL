using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMath {


    public static float GetDirection(Vector3 from, Vector3 to)
    {
        Vector2 direction2d = ((Vector2)(to - from)).normalized;
        return Mathf.Atan2(direction2d.y, direction2d.x) * Mathf.Rad2Deg;
    }

    public static float GetDirection(Vector2 from, Vector2 to)
    {
        return GetDirection(new Vector3(from.x, from.y, 0f), new Vector3(to.x, to.y, 0f));
    }

    public static float GetDirection(Unit from, Unit to)
    {
        Vector2 fromPos = from.transform.position;
        Vector2 toPos = to.transform.position;

        return GetDirection(fromPos, toPos);
    }

    public static Vector2 DirectionToVector2(float direction)
    {
        float rad = direction * (Mathf.PI / 180f);

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static Vector2 GetWorldHalfSize()
    {
        float worldY = Camera.main.orthographicSize; // 카메라의 세로 반지름 두배
        float worldX = worldY * Camera.main.aspect; // 카메라 세로 비율로 가로 비율 추출

        return new Vector2(worldX, worldY);
    }
    
    public static Vector2 FixedTowords(Vector2 from, Vector2 to, float delta)
    {
        return from + (to - from).normalized * delta;
    }

    public static float FixedTowordsAngle(float from, float to, float deltaAngle)
    {
        if(from + 180f < to)
            to -= 360f;

        if(from - 180f > to)
            to += 360f;

        if(from < to)
            return from + deltaAngle;
        else if(from > to)
            return from - deltaAngle;
        else
            return from;
    }
}
    