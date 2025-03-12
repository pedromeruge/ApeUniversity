using System.Collections.Generic;
using UnityEngine;

public class VaseBreakLogicScript : MonoBehaviour
{
    // class to allow to assign to each prefab a probability directly in the inspector
    [System.Serializable]
    public class PrefabProbability 
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float probability;
    }
    [SerializeField] private float numberSpawnedPrefabs = 3;
    [SerializeField] private float breakForce = 3.0f; // velocity at which the items inside the vase are thrown when broken

    [SerializeField] private List<PrefabProbability> spawnPrefabs;

    private Animator animator;
    private Collider2D vaseCollider;
    private List<GameObject> preComputedPrefabs = new List<GameObject>(); // list of prefabs to spawn are precomputed to avoid lag when brekaing the vase
    private List<Vector3> preComputedForces = new List<Vector3>();
    private List<Vector3> preComputedPositions = new List<Vector3>();

    private bool isBroken = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        preCalculateOresAndForces();
        vaseCollider = GetComponent<Collider2D>();
    }

    private void preCalculateOresAndForces() {
        for (int i = 0; i < numberSpawnedPrefabs; i++)
        {
            GameObject selectedPrefab = GetRandomPrefab(spawnPrefabs);
            if (selectedPrefab != null)
            {
                preComputedPrefabs.Add(selectedPrefab);
                Vector3 randomDirection = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.1f, 0.2f), 0).normalized;
                preComputedForces.Add(randomDirection);
                Vector3 randomPositionDisplacement = new Vector3(Random.Range(-0.01f, 0.01f), 0, 0);
                preComputedPositions.Add(randomPositionDisplacement);
            }
        }
    }
    public void BreakVase() {
        if (isBroken) return;
        isBroken = true;
        animator.SetBool("isBroken", true);
        SpawnPrefabs();
        vaseCollider.enabled = false; // disable collider to avoid collision when already broken
        Destroy(gameObject, 1f);
    }

    void SpawnPrefabs()
    {
        for (int i = 0; i < preComputedPrefabs.Count; i++)
        {
            GameObject spawnedObject = Instantiate(preComputedPrefabs[i], this.transform.position + preComputedPositions[i], Quaternion.identity);
            ApplyBreakForce(spawnedObject, preComputedForces[i]);
        }
    }

    private void ApplyBreakForce(GameObject obj, Vector3 randomDirection)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(randomDirection * breakForce, ForceMode2D.Impulse);
        }
    }

    // select a random prefab from a list of prefabs with probabilities
    GameObject GetRandomPrefab(List<PrefabProbability> prefabsWithProbability)
    {
        float totalProbability = 0f;

        // calculate total probability
        foreach (var item in prefabsWithProbability)
        {
            totalProbability += item.probability;
        }

        float randomPoint = Random.value * totalProbability;
        float cumulative = 0f;

        foreach (var item in prefabsWithProbability)
        {
            cumulative += item.probability;
            if (randomPoint <= cumulative)
            {
                return item.prefab;
            }
        }

        return null; // fallback in case probabilities are not set correctly
    }
}
