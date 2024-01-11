using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnItemManager : MonoBehaviour
{
    //Objet à instancier
    //[SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject[] itemPrefabs;

    //Pour déterminer la position de spawn
    //[SerializeField] private Transform _spawnPoint;
    //[SerializeField] private Transform _secondarySpawnPoint;

    [SerializeField] private Transform[] spawnPoints;

    //Nombre d'items à chaque spawn
    [SerializeField] private int _itemNumber;

    //[SerializeField] private float spawnRangeX = 5f;

    //private float startDelay = 2.0f;
    //private float repeatRate = 10.0f;

    [SerializeField] private InputActionReference _actionSpawnItems;

    // Start is called before the first frame update
    void Start()
    {
        _actionSpawnItems.action.Enable();
        _actionSpawnItems.action.performed += InstantiateXobjectsAtSpawnPoint;
    }

    public void InstantiateObjectAtSpawnPoint()
    {
        int itemIndex = Random.Range(0, itemPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        //Instancier un item dans la scène
        GameObject.Instantiate(itemPrefabs[itemIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
    }

    public void InstantiateXobjectsAtSpawnPoint(InputAction.CallbackContext obj)
    {
        for (int i = 0; i < _itemNumber; i++)
        {
            InstantiateObjectAtSpawnPoint();
        }
    }

    private void OnSpawnItems()
    {
        //InstantiateObjectAtSpawnPoint();
        Debug.Log("On Spawn Items Click");
    }

    /// <summary>
    /// Méthodes à faire évoluer en fonction des besoins
    /// </summary>

    /*
    public void InstantiateObjectAtTwoSpawnPoints()
    {
        //Instancier un item dans la scène
        GameObject.Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
        GameObject.Instantiate(_prefab, _secondarySpawnPoint.position, _secondarySpawnPoint.rotation);
    }

    public void InstantiateObjectAtPosition(int x, int z)
    {
        //Instancier un item dans la scène
        GameObject.Instantiate(_prefab, new Vector3(x, 0.04999924f, z), Quaternion.identity);
    }

    public void InstantiateObjectAtPositionWithRandomDirection(int x, int z)
    {
        //Instancier un item dans la scène
        GameObject.Instantiate(_prefab, new Vector3(x, 0.04999924f, z), Quaternion.identity);
    }

    public void InstantiateXobjectsRandomly()
    {
        for (int i = 0; i < _itemNumber; i++)
        {
            int a, b;
            a = Random.Range(-50, 51);
            b = Random.Range(-50, 51);
            InstantiateObjectAtPosition(a, b);
        }
    }

    public void InstantiateXobjectsAtTwoSpawnPoints()
    {
        for (int i = 0; i < _itemNumber; i++)
        {
            InstantiateObjectAtTwoSpawnPoints();
        }
    }

    void SpawnRandomItem(InputAction.CallbackContext obj)
    {

        int itemIndex = Random.Range(0, itemPrefabs.Length);

        for (int i = 0; i < _itemNumber; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeX, spawnRangeX));
            Instantiate(itemPrefabs[itemIndex], spawnPos, itemPrefabs[itemIndex].transform.rotation);
        }
    }
    */
}
