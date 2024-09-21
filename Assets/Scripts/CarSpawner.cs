using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
[System.Serializable]
    public class SpawnPoint
    {
        public Transform point;
        public float spawnInterval = 3f;
        public float carSpeed = 5f;
        internal float timeSinceLastSpawn;
    }   

    [SerializeField] public GameObject carPrefab;
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] public float destroyXPosition = -9f;
    public int poolSize = 50;
    private List<GameObject> carPool;

    private void Start()
    {
        InitializeCarPool();
    }

    private void Update()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.timeSinceLastSpawn += Time.deltaTime;
            if (spawnPoint.timeSinceLastSpawn >= spawnPoint.spawnInterval)
            {
                SpawnCar(spawnPoint);
                spawnPoint.timeSinceLastSpawn = 0f;
            }
        }
    }

    private void InitializeCarPool()
    {
        carPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject car = Instantiate(carPrefab);
            car.SetActive(false);
            carPool.Add(car);
        }
    }

    private void SpawnCar(SpawnPoint spawnPoint)
    {
        GameObject car = GetCarFromPool();
        if (car != null)
        {
            car.transform.position = spawnPoint.point.position;
            car.transform.rotation = spawnPoint.point.rotation;
            // car.transform.rotation = Quaternion.Euler(0, 90, 0);
            car.SetActive(true);

            Rigidbody2D rb = car.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                //rb.velocity = Vector3.left * spawnPoint.carSpeed;
                rb.velocity = Vector3.left * spawnPoint.carSpeed;
                rb.gravityScale = 0f;
            }

            StartCoroutine(CheckCarPosition(car));
        }
    }

    private GameObject GetCarFromPool()
    {
        foreach (var car in carPool)
        {
            if (!car.activeInHierarchy)
            {
                return car;
            }
        }
        return null;
    }

    private IEnumerator CheckCarPosition(GameObject car)
    {
        while (car.activeInHierarchy)
        {
            if (car.transform.position.x <= destroyXPosition)
            {
                car.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.point != null)
            {
                Gizmos.DrawSphere(spawnPoint.point.position, 0.5f);
            }
        }

        Gizmos.color = Color.blue;
        // Gizmos.DrawLine(new Vector3(destroyXPosition, -10, 0), new Vector3(destroyXPosition, 10, 0));
    }
}
