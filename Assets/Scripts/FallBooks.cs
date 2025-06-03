using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallBooks : MonoBehaviour
{
    public GameObject spritePrefab;
    public float lifetime = 2f;
    public int maxSprites = 50;

    private List<GameObject> activeSprites = new List<GameObject>();
    private bool isSpawning = false;


    public void SpawnSprites(float clickPower)
    {
        if (clickPower <= 0) return;
        if (isSpawning) return; 
        int count = Mathf.Clamp(Mathf.FloorToInt(clickPower / 10f), 1, 5);

        int canSpawn = Mathf.Min(count, maxSprites - activeSprites.Count);
        if (canSpawn <= 0) return; 

        StartCoroutine(SpawnSpritesCoroutine(canSpawn));
    }

    private IEnumerator SpawnSpritesCoroutine(int count)
    {
        isSpawning = true;

        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(-3f, 3f);
            float y = Random.Range(5f, 6.5f);
            Vector2 spawnPos = new Vector2(x, y);
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            GameObject sprite = Instantiate(spritePrefab, spawnPos, rotation);
            activeSprites.Add(sprite);

            StartCoroutine(DestroyAfter(sprite, lifetime));

            yield return new WaitForSeconds(0.1f);
        }

        isSpawning = false;
    }

    private IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if (activeSprites.Contains(obj))
            activeSprites.Remove(obj);
        Destroy(obj);
    }
}
