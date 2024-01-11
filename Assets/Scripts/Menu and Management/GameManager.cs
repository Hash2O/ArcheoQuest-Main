using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Objet à instancier
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject[] zombiePrefabs;

    //Pour déterminer la position de spawn
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _secondarySpawnPoint;

    //Wave of zombies
    [SerializeField] private int _nombreDeZombies;


    [SerializeField] private GameObject _projectilePrefab;

    private float spawnRangeX = 50f;

    //private float startDelay = 2.0f;
    //private float repeatRate = 10.0f;

    [SerializeField] private InputActionReference _actionSpawnZombies;

    // Start is called before the first frame update
    void Start()
    {
        //InstantiateXobjectsRandomly();
        InstantiateXobjectsAtSpawnPoint();
        //InvokeRepeating("InstantiateXobjectsAtTwoSpawnPoints", startDelay, repeatRate);
        //SpawnRandomZombie();


        _actionSpawnZombies.action.Enable();
        _actionSpawnZombies.action.performed += SpawnRandomZombie;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateObject()
    {
        //Instancier un zombie dans la scène
        GameObject.Instantiate(_prefab);
    }

    public void InstantiateObjectAtSpawnPoint()
    {
        //Instancier un zombie dans la scène
        GameObject.Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
    }

    public void InstantiateObjectAtTwoSpawnPoints()
    {
        //Instancier un zombie dans la scène
        GameObject.Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
        GameObject.Instantiate(_prefab, _secondarySpawnPoint.position, _secondarySpawnPoint.rotation);
    }

    public void InstantiateObjectAtPosition(int x, int z)
    {
        //Instancier un zombie dans la scène
        GameObject.Instantiate(_prefab, new Vector3(x, 0.04999924f, z), Quaternion.identity);
    }

    public void InstantiateObjectAtPositionWithRandomDirection(int x, int z)
    {
        //Instancier un zombie dans la scène
        GameObject.Instantiate(_prefab, new Vector3(x, 0.04999924f, z), Quaternion.identity);
    }

    public void InstantiateXobjectsRandomly()
    {
        for(int i = 0; i < _nombreDeZombies; i++)
        {
            int a, b;
            a = Random.Range(-50, 51); 
            b = Random.Range(-50, 51);
            InstantiateObjectAtPosition(a, b);
        }
    }

    public void InstantiateXobjectsAtSpawnPoint()
    {
        for (int i = 0; i < _nombreDeZombies; i++)
        {
            InstantiateObjectAtSpawnPoint();
        }
    }

    public void InstantiateXobjectsAtTwoSpawnPoints()
    {
        for (int i = 0; i < _nombreDeZombies; i++)
        {
            InstantiateObjectAtTwoSpawnPoints();
        }
    }

    void SpawnRandomZombie(InputAction.CallbackContext obj)
    {
 
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);

        for (int i = 0; i < _nombreDeZombies; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeX, spawnRangeX));
            Instantiate(zombiePrefabs[zombieIndex], spawnPos, zombiePrefabs[zombieIndex].transform.rotation);
        }
    }

    private void OnSpawnZombies()
    {
        InstantiateObjectAtSpawnPoint();
    }


    public void LoadNextScene()
    {
        Debug.Log("Load Next Scene");
    }

    public void LoadPreviousScene()
    {
        Debug.Log("Load Previous Scene");
    }

    public void ReloadScene()
    {
        Debug.Log("Reload Scene");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
    }


}
