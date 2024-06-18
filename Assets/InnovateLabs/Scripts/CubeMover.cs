using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMover : MonoBehaviour
{
    public GameObject cubeA;
    public GameObject cubeB;

    public InputActionReference rightJoystick;
    public InputActionReference leftJoystick;

    private Vector2 rightInput;
    private Vector2 leftInput;

    public float speed = 0f;

    private Vector3 _initPosA;
    private Vector3 _initPosB;
    private Quaternion _initRotA;
    private Quaternion _initRotB;

    public GameObject ToAppear;
    private void OnEnable()
    {
        _initPosA = cubeA.transform.position;
        _initPosB = cubeB.transform.position;
        _initRotA = cubeA.transform.rotation;
        _initRotB = cubeB.transform.rotation;

        rightJoystick.action.performed += ctx => rightInput = ctx.ReadValue<Vector2>();
        rightJoystick.action.canceled += ctx => rightInput = Vector2.zero;

        leftJoystick.action.performed += ctx => leftInput = ctx.ReadValue<Vector2>();
        leftJoystick.action.canceled += ctx => leftInput = Vector2.zero;

        rightJoystick.action.Enable();
        leftJoystick.action.Enable();
    }

    private void OnDisable()
    {
        rightJoystick.action.Disable();
        leftJoystick.action.Disable();
    }

    private void Update()
    {
        MoveCube(cubeA, cubeB, rightInput);
        MoveCube(cubeB, cubeA, leftInput);
    }

    private void MoveCube(GameObject fromCube, GameObject toCube, Vector2 input)
    {
        var dist = Vector3.Distance(cubeA.transform.position, cubeB.transform.position);
        if (dist <= 0.5f)
        {
            //    cubeA.transform.SetPositionAndRotation(_initPosA, _initRotA);
            //    cubeB.transform.SetPositionAndRotation(_initPosB, _initRotB);
            ToAppear.SetActive(true);
            rightJoystick.action.Disable();
            leftJoystick.action.Disable();
        }
        if (input.y > 0)
        {
            float step = input.y * Time.deltaTime * speed; // Adjust the speed factor as needed
            fromCube.transform.position = Vector3.MoveTowards(fromCube.transform.position, toCube.transform.position, step);
        }
    }
}
