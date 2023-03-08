using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Cannon rotation settings")]
    [SerializeField]
    private float maxYRotation = 150f;
    [SerializeField]
    private float minYRotation = 50f;
    [SerializeField]
    private float maxXRotation = 75;
    [SerializeField]
    private float minXRotation = 15f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private Transform cannonBarrelTransform;
    [SerializeField]
    private Transform cannonBaseTransform;

    [Header("Cannon projectile settings")]
    [SerializeField]
    private float projectileFireForce;
    [SerializeField]
    private CannonBall projectilePrefab;
    [SerializeField]
    private Transform firePointTransform;

    [Header("Object pool settings")]
    [SerializeField] private PoolObjectId cannonBallTypeShot;
    [SerializeField] private ObjectsPool pool;


    [Header("input Settings")]
    [SerializeField] private bool useKeyboard;
    [SerializeField] private bool useMouse;
    [SerializeField] private bool useGrabbing;

    private ICannonInputScheme inputScheme;
    private bool fireDisabled;

    public void OnLevelEnded()
    {
        fireDisabled = true;
        inputScheme.Dispose();
    }


    void Update()
    {
        AimCannon();
        TryFireCannon();
    }

    private void AimCannon()
    {
        var input = inputScheme.AimInput();

        float newBaseRotation = cannonBaseTransform.localRotation.eulerAngles.y + rotationSpeed * input.x;
        newBaseRotation = Mathf.Clamp(newBaseRotation, minYRotation, maxYRotation);
        cannonBaseTransform.localRotation = Quaternion.Euler(0, newBaseRotation, 0);

        float newBarrelRotation = cannonBarrelTransform.localRotation.eulerAngles.x - rotationSpeed * input.y;
        newBarrelRotation = Mathf.Clamp(newBarrelRotation, minXRotation, maxXRotation);
        cannonBarrelTransform.localRotation = Quaternion.Euler(newBarrelRotation, 0, 0);
    }

    private void TryFireCannon()
    {
        if (fireDisabled || !inputScheme.FireTriggered())
        {
            return;
        }

        CannonBall instantiatedBall = pool.GetObject<CannonBall>(cannonBallTypeShot);
        instantiatedBall.transform.position = firePointTransform.position;
        instantiatedBall.Setup(firePointTransform.forward * projectileFireForce, pool);

    }

    private void Start()
    {
        GameServices.GetService<LevelController>().levelEnded += OnLevelEnded;
    }

    private void Awake()
    {
        if (useKeyboard)
            inputScheme = new CannonKeyboardInputScheme();
        if (useMouse)
            inputScheme = new CannonMouseInputScheme();
        else
            inputScheme = new CannonJoystickInputScheme();

        pool.Setup(20);
    }
}
