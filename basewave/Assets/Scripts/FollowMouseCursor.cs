using UnityEngine;
using System;

public class FollowMouseCursor : MonoBehaviour
{

    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0.0f;
        transform.position = pos;
    }

}