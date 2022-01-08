using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Func<Vector3> GetCameraFollowPosFunc;

    [SerializeField]
    float cameraMoveSpeed = 2f;

    public void Setup(Func<Vector3> GetCameraFollowPosFunc){
        this.GetCameraFollowPosFunc = GetCameraFollowPosFunc;
    }

    

    void Update()
    {
        Vector3 cameraFollowPos = GetCameraFollowPosFunc();
        cameraFollowPos.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPos - transform.position).normalized;
        float dist = Vector3.Distance(cameraFollowPos, transform.position);

        if(dist > 0){
            Vector3 newCameraPosition = transform.position + cameraMoveDir * dist * cameraMoveSpeed * Time.deltaTime;
            float distAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPos);
            if(distAfterMoving > dist){
                newCameraPosition = cameraFollowPos;
            }

            transform.position = newCameraPosition;
        }
        
    }
}
