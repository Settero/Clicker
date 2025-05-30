using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallBooks : MonoBehaviour
{
    public GameObject spritePrefab;
    public float lifetime = 2f;      
    public int maxSprites = 100;      

    private List<GameObject> activeSprites = new List<GameObject>();

    public void SpawnSprites(int n)
    {
        int canSpawn = Mathf.Min(n, maxSprites - activeSprites.Count);

        for (int i = 0; i < canSpawn; i++)
        {
            float x = Random.Range(-3f, 3f);
            float y = Random.Range(5f, 6.5f);
            Vector2 spawnPos = new Vector2(x,y);
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            GameObject sprite = Instantiate(spritePrefab, spawnPos, rotation);
            activeSprites.Add(sprite);

            StartCoroutine(DestroyAfter(sprite, lifetime));
        }
    }

    private IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if (activeSprites.Contains(obj))
            activeSprites.Remove(obj);
        Destroy(obj);
    }
}
