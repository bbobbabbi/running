using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject currentPlayer;
    private float xRange = 5;
    private float zMiddleRange = 8.5f;
    private float addzRange = 7.5f;
    [SerializeField]private float moveSpeed;
    private bool onTriggerTrigger;
    Vector3 firstPlayerPosition;
    private void Awake()
    {
        firstPlayerPosition = currentPlayer.transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // z 값은 카메라와의 거리 설정
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var value = worldPosition - firstPlayerPosition;

            if (!currentPlayer)
                return;

            if (value != Vector3.zero)
            {
                // 이동 방향 계산
                Vector3 moveDirection = new Vector3(value.x, 0, 0).normalized;
                Vector3 currentPosition = currentPlayer.transform.position;
                // 플레이어 이동
                currentPlayer.transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);


                if (currentPlayer.transform.position.x < -xRange ||
                    currentPlayer.transform.position.x > xRange ||
                    currentPlayer.transform.position.z < -zMiddleRange + addzRange ||
                    currentPlayer.transform.position.z > zMiddleRange + addzRange)
                {
                    currentPlayer.transform.position = currentPosition;
                }
                // Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                // currentPlayer.transform.rotation = Quaternion.Slerp(currentPlayer.transform.rotation, targetRotation, Time.deltaTime * 10f // 회전 속도 조절

                if (moveDirection != Vector3.zero)
                {
                    // 1️⃣ 방향 벡터를 각도로 변환 (라디안 → 도)
                    float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

                    // 2️⃣ Y축으로만 회전하도록 Euler 각도 적용
                    Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

                    // 3️⃣ 부드럽게 회전 (Slerp로 자연스러운 회전)
                    currentPlayer.transform.rotation = Quaternion.Slerp(currentPlayer.transform.rotation, targetRotation, Time.deltaTime * 10f);
                }

            }
        }
    }


}
