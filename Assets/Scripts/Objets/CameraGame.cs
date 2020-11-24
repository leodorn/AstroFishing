using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGame : MonoBehaviour
{
    public void AttachCameraAndSetPosition(Transform transformToAttach)
    {
        transform.SetParent(transformToAttach);
        transform.position = new Vector3(transformToAttach.position.x,
            transformToAttach.position.y, transform.position.z);
    }

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -1f);
    }
}
