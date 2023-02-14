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

    void Start()
    {
        
    }

    
    void Update()
    {
        AimCannon();
        TryFireCannon();
    }

    private void AimCannon()
    {
        float newBaseRotation = cannonBaseTransform.localRotation.eulerAngles.y + rotationSpeed * Input.GetAxis("RightJoystickX");
        newBaseRotation = Mathf.Clamp(newBaseRotation, minYRotation, maxYRotation);
        cannonBaseTransform.localRotation = Quaternion.Euler(0, newBaseRotation, 0);

        float newBarrelRotation = cannonBarrelTransform.localRotation.eulerAngles.x - rotationSpeed * Input.GetAxis("RightJoystickY");
        newBarrelRotation = Mathf.Clamp(newBarrelRotation, minXRotation, maxXRotation);
        cannonBarrelTransform.localRotation = Quaternion.Euler(newBarrelRotation, 0, 0);
    }

    private void TryFireCannon()
    {
        if (!Input.GetButtonDown("RightJoystickButton"))
        {
            return;
        }

        CannonBall instantiatedBall =
                Instantiate(projectilePrefab, firePointTransform.position, Quaternion.identity);
        instantiatedBall.Setup(firePointTransform.forward * projectileFireForce);

    }
}
