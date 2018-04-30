using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Unit
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }

    public Direction direction;

    private void Awake()
    {
        float maxY = Camera.main.orthographicSize; // 카메라의 세로 반지름 두배
        float maxX = maxY * Camera.main.aspect; // 카메라 세로 비율로 가로 비율 추출

        float thick = 0.5f;

        switch(direction)
        {
            case Direction.UP:
                transform.position = new Vector3(0f, maxY + thick);
                transform.localScale = new Vector3(maxX * 2f + 2, 1);
                break;
            case Direction.DOWN:
                transform.position = new Vector3(0f, -maxY - thick);
                transform.localScale = new Vector3(maxX * 2f + 2, 1);
                break;
            case Direction.RIGHT:
                transform.position = new Vector3(maxX + thick, 0f);
                transform.localScale = new Vector3(1, maxY * 2f + 2);
                break;
            case Direction.LEFT:
                transform.position = new Vector3(-maxX - thick, 0f);
                transform.localScale = new Vector3(1 ,maxY * 2f + 2);
                break;

        }
    }
}
