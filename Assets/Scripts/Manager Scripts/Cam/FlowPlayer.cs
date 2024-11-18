using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlayer : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    private GameObject currentlyplayer;
    private PlayerSelection index;
    private CinemachineVirtualCamera camera;
    private Vector3 offset = new Vector3(24.32f, 5.65f, 68.5f);
    void Start()
    {
       
    }
    private void LateUpdate()
    {
        transform.position = player1.transform.position + offset;
        transform.position = player2.transform.position + offset;
    }

    private void UpdateCurrentlyPlayer()
    {
        // Giả định rằng 'players' trong PlayerSelection là chỉ số 0 hoặc 1 để chọn nhân vật
        
    }
}
