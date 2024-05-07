using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallCamera : MonoBehaviour
{

    public GameObject tPlayer;
    public Transform tFollowTarget;
    [SerializeField] private CinemachineFreeLook vcam;
    [SerializeField] private TurnManager turnManager;
    

    void Update()
    {
        tPlayer = turnManager.GetCurrentPlayer();
        if (tPlayer != null)
        {
            tFollowTarget = tPlayer.transform;
            vcam.LookAt = tFollowTarget;
            vcam.Follow = tFollowTarget;
        }
        
    }
}
