using System;
using System.Net.WebSockets;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    public InputManager inputManager;
    public ParticleSystem particles;
    public Boolean fire;

    void Start()
    {
      particles.Stop();
    }

    void Update()
    {
        var target = GetTarget();
        transform.LookAt(target);

        if (fire && particles.isStopped) {
            Debug.Log("fire");
            particles.Play();
        } else if (!fire && particles.isPlaying) {
            Debug.Log("stop");
            particles.Stop();
        }

        Debug.DrawLine(transform.position, target, Color.red);
        Debug.DrawLine(target, target+new Vector3(0, 25, 0), Color.blue);
    }

    private Vector3 GetTarget() 
    {
        return inputManager.pointerWorld;
    }
}
