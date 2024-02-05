using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField]private float rotationAnglez = 1;
    [SerializeField] private float rotationAngley = 2;
    [SerializeField] private float rotationAnglex = 2;
    private float rotationSpeed = 1f;
    [SerializeField] private float speed = 40.0f;

    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private bool isChangingCamera;

    private void Start()
    {
        camera1.enabled = true;
        camera2.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isChangingCamera)
        {
            ChangeCamera();
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * Time.deltaTime;

        Quaternion xRotation = CalculateRotationQuaternion(rotationAnglex * input.y, Vector3.right);
        Quaternion yRotation = CalculateRotationQuaternion(rotationAngley * input.x, Vector3.up);
        Quaternion zRotation = CalculateRotationQuaternion(rotationAnglez * -input.x, Vector3.forward);

        if(input.x != 0)
        {
            transform.localRotation = yRotation * transform.localRotation * zRotation;
        }
        if (input.y != 0)
        {
            transform.localRotation = xRotation * transform.localRotation;
        }
             

        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void ChangeCamera()
    {
        isChangingCamera = true;
        if (camera1.isActiveAndEnabled)
        {
            camera1.enabled = false;
            camera2.enabled = true;
            Invoke(nameof(ResetIsChangingCamera), 0.5f);
        }
        else
        {
            camera1.enabled = true;
            camera2.enabled = false;
            Invoke(nameof(ResetIsChangingCamera), 0.5f);
        }
    }

    void ResetIsChangingCamera()
    {
        isChangingCamera = false;
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

