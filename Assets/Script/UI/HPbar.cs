using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour {

    public GameObject hpiconPrefab;

    Stack<GameObject> hpIconStack = new Stack<GameObject>();

    private void Update()
    {
        int hp = PlayerScript.player.CurrentHP;

        while(hpIconStack.Count != hp)
        {
            if (hpIconStack.Count > hp)
            {
                Destroy(hpIconStack.Pop());
            }
            else if(hpIconStack.Count < hp)
            {
                GameObject instance = Instantiate(hpiconPrefab);
                hpIconStack.Push(instance);

                Vector2 targetPos = transform.position;


                float maxY = Camera.main.orthographicSize; // 카메라의 세로 반지름 두배
                float maxX = maxY * Camera.main.aspect; // 카메라 세로 비율로 가로 비율 추출

                targetPos.x = -maxX + 0.5f + 1f * (hpIconStack.Count - 1);

                instance.transform.position = targetPos;

                instance.transform.SetParent(transform);

                instance.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }


}
