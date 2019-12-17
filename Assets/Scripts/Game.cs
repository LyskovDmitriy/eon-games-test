using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CameraController mainCamera = default;
    [SerializeField] private Player player = default;


    private void Start()
    {
        mainCamera.StartFollowing(player.transform);
    }
}
