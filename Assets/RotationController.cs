using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField]private float rotationAnglez = 1;
    [SerializeField] private float rotationAngley = 2;
    private float rotationSpeed = 1f;
    [SerializeField] private float speed = 40.0f;

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * Time.deltaTime;

        Quaternion yRotation = CalculateRotationQuaternion(rotationAngley * input.x, Vector3.up);
        Quaternion zRotation = CalculateRotationQuaternion(rotationAnglez * -input.x, Vector3.forward);

        print(zRotation);

        transform.localRotation = yRotation * transform.localRotation * zRotation;


        transform.position += transform.forward * Time.deltaTime * speed;
    }

    Quaternion CalculateRotationQuaternion(float angle, Vector3 axis)
    {
        float angleInRadians = angle * Mathf.Deg2Rad;

        Quaternion rotationQuaternion = new Quaternion(
            axis.x * Mathf.Sin(angleInRadians / 2), 
            axis.y * Mathf.Sin(angleInRadians / 2),
            axis.z * Mathf.Sin(angleInRadians / 2), 
            Mathf.Cos(angleInRadians / 2)
        );

        return rotationQuaternion;
    }
    Quaternion ConjugateQuaternion(Quaternion q)
    {
        return new Quaternion(-q.x, -q.y, -q.z, q.w);
    }
}

