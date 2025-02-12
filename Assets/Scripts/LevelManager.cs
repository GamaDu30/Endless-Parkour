using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 margin;
    [SerializeField] float spawnDistance;
    [SerializeField] int rowCount;
    [SerializeField] PhysicMaterial smoothMaterial;
    [SerializeField] List<Vector3> blockSizes;


    List<List<Transform>> platforms = new List<List<Transform>>();


    void Start()
    {
        for (int i = 0; i < rowCount; i++)
        {
            platforms.Add(new List<Transform>());
        }

        platforms[1].Add(GameObject.Find("SpawnPlatform").transform);
    }

    void Update()
    {
        UpdatePlatforms();
    }

    void UpdatePlatforms()
    {
        //Moving the platforms
        for (int row = platforms.Count - 1; row >= 0; row--)
        {
            for (int id = platforms[row].Count - 1; id >= 0; id--)
            {
                Transform curPlatform = platforms[row][id];
                curPlatform.position += Vector3.back * speed * Time.deltaTime;

                if (curPlatform.position.z < -10)
                {
                    Destroy(curPlatform.gameObject);
                    platforms[row].Remove(curPlatform);
                }
            }
        }

        //Checking to spawn new platforms
        for (int row = platforms.Count - 1; row >= 0; row--)
        {
            if (platforms[row].Count == 0 || platforms[row][^1].position.z < spawnDistance - margin.y)
            {
                SpawnPlatform(row, Random.Range(0, blockSizes.Count - 1));
            }
        }
    }

    void SpawnPlatform(int row, int id)
    {
        Transform newPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        platforms[row].Add(newPlatform);
        Vector3 size = blockSizes[id];

        newPlatform.localScale = size;
        newPlatform.position = new Vector3((row - 1) * margin.x - blockSizes[id].x * 0.5f, 0, spawnDistance + Random.Range(0, 5)) + newPlatform.localScale * 0.5f;
        newPlatform.SetParent(transform);
        newPlatform.tag = size.y == 1 ? "Platform" : "Wall";

        newPlatform.GetComponent<BoxCollider>().material = smoothMaterial;

        Rigidbody rb = newPlatform.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.isKinematic = true;
    }
}
