using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : Boss
{
    public Part_Boss_1 partPrefab;

    public List<Part_Boss_1> parts = new List<Part_Boss_1>();

    public float partRootDirection;

    public float partRotateSpeed;
    public float partReverseRotateSpeed;
    private float targetPartRotateSpeed;
    private float currentPartRotateSpeed;

    public float partDistance = 1.5f;

    public bool enableSetupPartTransform = true;
    public bool enableReversePattern = false;

    protected override void Awake()
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

        if(enableReversePattern == true)
            targetPartRotateSpeed = partReverseRotateSpeed;
        else
            targetPartRotateSpeed = partRotateSpeed;

        currentPartRotateSpeed = Mathf.Lerp(currentPartRotateSpeed, targetPartRotateSpeed, Time.fixedDeltaTime);

        partRootDirection += currentPartRotateSpeed * Time.fixedDeltaTime;

        for (int i = 0; i < 4; ++i)
        {
            float dir = partRootDirection + i * 90f;

            Vector2 deltaPos = MyMath.DirectionToVector2(dir) * partDistance;

            dir -= 90f;

            parts[i].transform.position = transform.position + (Vector3)deltaPos;

            Vector3 partAngle = parts[i].transform.eulerAngles;
            partAngle.z = dir;
            parts[i].transform.eulerAngles = partAngle;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < 4; ++i)
        {
            if (parts[i] != null)
                Destroy(parts[i].gameObject);
        }
    }
}
