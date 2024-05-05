using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 SpawnPos;
    public GameObject spawnObject;
    GameObject tmpObject;
    private float newSpawnDuration = 1f;

    #region Singleton

    public static Spawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        SpawnPos = transform.position;
       
        NewSpawnRequest();
    }

    void SpawnNewObject()
    {
        tmpObject= Instantiate(spawnObject, SpawnPos, Quaternion.identity);
        
    }

    public void NewSpawnRequest()
    {
        Invoke("SpawnNewObject", newSpawnDuration);
    }

    public void RetrySpawnRequest()
    {

       
        Invoke("SpawnNewObject", newSpawnDuration);
        Destroy(tmpObject,2f);
    }
}