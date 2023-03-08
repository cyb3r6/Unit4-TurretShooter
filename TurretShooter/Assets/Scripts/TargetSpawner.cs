using System.Collections.Generic;
using UnityEngine;

namespace CannonApp
{
    public class TargetSpawner : MonoBehaviour
    {
        [SerializeField] private List<Target> availableTargetPrefabs;
        [SerializeField] private Transform spawnAnchor1;
        [SerializeField] private Transform spawnAnchor2;
        [SerializeField] private float spawnMinTime;
        [SerializeField] private float spawnMaxTime;

        private float remainingTime;
        private Vector3 spawnAreaSize;

        private void Update()
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime > 0) return;

            remainingTime = Random.Range(spawnMinTime, spawnMaxTime);

            var target = availableTargetPrefabs[Random.Range(0, availableTargetPrefabs.Count)];
            var randomPositionDelta = new Vector3(
                Random.Range(0, spawnAreaSize.x),
                Random.Range(0, spawnAreaSize.y),
                Random.Range(0, spawnAreaSize.z));
            var position = spawnAnchor1.position + randomPositionDelta;

            Instantiate(target, position, Quaternion.identity);
        }

        private void OnLevelEnded()
        {
            enabled = false;
        }

        private void Start()
        {
            spawnAreaSize = spawnAnchor2.position - spawnAnchor1.position;
            GameServices.GetService<LevelController>().levelEnded += OnLevelEnded;
        }
    }
}