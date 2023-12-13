using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float linearDrag;
    public LayerMask pointerMask;
    public new Camera camera;
    public InputManager inputManager;
    public Vector3 velocity = new();
    public Minigun[] miniguns;
    private Vector3 pointer = new();
    private Vector2 pInput = new();


    void FixedUpdate()
    {
        pointer = inputManager.pointerWorld;
        pInput = GetInput();

        if (Input.GetMouseButton(0)) {
            foreach (var mg in miniguns) {
                mg.fire = true;
            }
        } else {
            foreach (var mg in miniguns) {
                mg.fire = false;
            }
        }

        ApplyRotation();
        ApplyLinearVelocity();
        ApplyLinearDrag();

        // DEBUG
        Debug.DrawLine(transform.position, transform.position+(transform.forward*3), Color.green);
    }

    private Vector2 GetInput() {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void ApplyLinearVelocity() {
        var s = speed;
        if (pInput.y < 0) {
            s *= 0.5f;
        }
        velocity.x += pInput.y*Time.deltaTime*s*transform.forward.x;
        velocity.z += pInput.y*Time.deltaTime*s*transform.forward.z;

        transform.position += velocity;
    }

    private void ApplyLinearDrag() {
        velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime*linearDrag);
    }

    private void ApplyRotation() {
        var pointer = inputManager.pointerWorld;
        var sp = Camera.main.WorldToViewportPoint(transform.position);
        var mp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        var angle = Mathf.Atan2(mp.y - sp.y, mp.x - sp.x) * Mathf.Rad2Deg;
        var accelRotation = pInput.y*20;
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            Quaternion.Euler(new Vector3(accelRotation, -angle+90, 0)), 
            Time.deltaTime*rotationSpeed
        );
    }
}