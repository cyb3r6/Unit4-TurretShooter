using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private LevelController levelController;

    public void Setup(LevelController controller)
    {
        levelController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("WaterTrigger")))
            return;

        levelController.TargetDestroyed();
        Destroy(gameObject);
    }
}
