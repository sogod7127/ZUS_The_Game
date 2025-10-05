using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Gracz
    public float smoothSpeed = 0.125f;  // Szybkoœæ ruchu kamery
    public Vector3 offset = new Vector3(0, 0, -10); // Sta³e przesuniêcie w Z

    void LateUpdate()
    {
        if (target == null) return;

        // Pozycja docelowa kamery w XY + sta³e Z
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z
        );

        // P³ynne przejœcie kamery
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
