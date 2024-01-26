using UnityEngine;

public class RotationController : MonoBehaviour
{
    private float rotationAnglez = 1f;
    private float rotationAngley = 1f;
    private float rotationSpeed = 1f;
    private float speed = 40.0f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Quaternion yRotation = CalculateRotationQuaternion(rotationAngley * input, Vector3.up);
        Quaternion zRotation = CalculateRotationQuaternion(rotationAnglez * -input, Vector3.forward);
        
        transform.rotation = (yRotation * zRotation) * transform.rotation * (ConjugateQuaternion(yRotation) * ConjugateQuaternion(zRotation));

        transform.position = transform.position + transform.forward * Time.deltaTime * speed;
    }

    Quaternion CalculateRotationQuaternion(float angle, Vector3 axis)
    {
        // angle en radians
        float angleInRadians = angle * Mathf.Deg2Rad;

        // quaternion de rotation
        Quaternion rotationQuaternion = new Quaternion(
            0, // ici ca vaut 0
            axis.y * Mathf.Sin(angleInRadians / 2), // ici ca vaut 0 pour l'axe z
            axis.z * Mathf.Sin(angleInRadians / 2), // ici ca vaut 0 pour l'axe y
            Mathf.Cos(angleInRadians / 2)
        );

        return rotationQuaternion;
    }
    Quaternion ConjugateQuaternion(Quaternion q)
    {
        return new Quaternion(-q.x, -q.y, -q.z, q.w);
    }
}

