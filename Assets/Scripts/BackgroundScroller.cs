using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeedY = 0.03f;
    //[SerializeField] float backgroundScrollSpeedX = 0.01f; Need a vertical and horizontal looping background
    Material myMaterial;
    Vector2 offset;
    [SerializeField] List<GameObject> asteroidPrefab;
    [SerializeField] float asteroidSpawnMin = 1f;
    [SerializeField] float asteroidSpawnMax = 3f;
    float asteroidSpawnTime;
    float asteroidXMin = -5.5f;
    float asteroidXMax = 5.5f;
    float asteroidXLocation;
    Vector2 asteroidVector;
    int asteroidRandomType;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeedY);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
        asteroidSpawnTime -= Time.deltaTime;
        if (asteroidSpawnTime <= 0f)
        {
            SpawnAsteroids();
        }

    }

    void SpawnAsteroids()
    {
        asteroidSpawnTime = Random.Range(asteroidSpawnMin, asteroidSpawnMax);
        asteroidRandomType = Random.Range(0, 2);
        asteroidXLocation = Random.Range(asteroidXMin, asteroidXMax);
        asteroidVector = new Vector2(asteroidXLocation, 11.5f);
        Instantiate(asteroidPrefab[asteroidRandomType], asteroidVector, Quaternion.identity);
    }
}
