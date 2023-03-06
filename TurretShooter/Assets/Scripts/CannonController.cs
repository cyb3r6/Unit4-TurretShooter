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

    [Header("Target settings")]
    public Material highlightMaterial;
    private Material defaultMaterial;
    private Transform hitTargetTransform;

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

    /// <summary>
    /// Challenge 1
    /// </summary>
    private void CannonRaycast()
    {
        if (hitTargetTransform != null)
        {
            // change to default material
            var hitTargetRenderer = hitTargetTransform.GetComponent<Renderer>();
            hitTargetRenderer.material = defaultMaterial;
            hitTargetTransform = null;
        }

        Ray ray = new Ray(firePointTransform.position, firePointTransform.forward);
        int layerMask = LayerMask.GetMask("Targets");
        RaycastHit[] raycastHits = new RaycastHit[5];

        int hits = Physics.RaycastNonAlloc(ray, raycastHits, 1000, layerMask);

        var target = raycastHits[0].transform;

        if (target)
        {
            var targetRenderer = target.GetComponent<Renderer>();
            defaultMaterial = targetRenderer.material;

            if (targetRenderer != null)
            {
                targetRenderer.material = highlightMaterial;
            }

            hitTargetTransform = target;
        }
    }
}
