using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallCamera : MonoBehaviour
{

    public GameObject tPlayer;
    public Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;
    [SerializeField] TurnManager turnManager;
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        tPlayer = turnManager.GetCurrentPlayer();
        if (tPlayer == null)
        {
            if (tPlayer != null)
            {
                tFollowTarget = tPlayer.transform;
                vcam.LookAt = tFollowTarget;
                vcam.Follow = tFollowTarget;
            }
        }
    }
}
