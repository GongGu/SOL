using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : Boss
{
    public Part_Boss_1 partPrefab;

    public List<Part_Boss_1> parts = new List<Part_Boss_1>();

    public float partRootDirection;
    public float partRotateSpeed = 30f;

    public bool enableSetupPartTransform = true;

    private void Awake()
    {
        for (int i = 0; i < 4; ++i)
            parts.Add(Instantiate(partPrefab));


    }

    private void FixedUpdate()
    {
        SetupPartTransform();

    }

    public void SetupPartTransform()
    {
        if (enableSetupPartTransform == false)
            return;

        partRootDirection += partRotateSpeed * Time.fixedDeltaTime;

        for (int i = 0; i < 4; ++i)
        {
            float distance = 1.5f;

            float dir = partRootDirection + i * 90f;

            Vector2 deltaPos = MyMath.DirectionToVector2(dir) * distance;

            dir += 90f;

            parts[i].transform.position = transform.position + (Vector3)deltaPos;
            Vector3 partAngle = parts[i].transform.eulerAngles;
            partAngle.z = dir;
            parts[i].transform.eulerAngles = partAngle;
        }
    }

    private void OnDestroy()
    {
            for (int i = 0; i < 4; ++i)
                Destroy(parts[i].gameObject);   
    }
}
