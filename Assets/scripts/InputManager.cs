using UnityEngine;

public class InputManager : MonoBehaviour 
{
    public Camera cam;
    public LayerMask pointerMask;
    public Vector3 pointerWorld = new();

    void Update() {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, pointerMask)) {
            pointerWorld = hit.point;
        }
    }
}