using System.IO;
using UnityEngine;
using System;

[System.Serializable]
public class MemoryItem {
    public string clipUrl;
    public string title;
    public string emotion;
}

public class MemoryItemArray {
    public MemoryItem[] items;
}

public class MemoryManager : MonoBehaviour {
    public GameObject memoryBubblePrefab;

    void Start()
    {
        string jsonPath = Path.Combine(Application.streamingAssetsPath, "memories.json");
        string jsonText = File.ReadAllText(jsonPath);
        MemoryItemArray memoryArray = JsonUtility.FromJson<MemoryItemArray>(jsonText);
        MemoryItem[] memories = memoryArray.items;


        foreach (var mem in memories)
        {
            GameObject bubble = Instantiate(memoryBubblePrefab, RandomPosition(), Quaternion.identity);

            if (bubble == null)
            {
                Debug.LogError("Bubble instantiation failed!");
                continue;
            }

            var script = bubble.GetComponent<MemoryBubble>();
            if (script == null)
            {
                Debug.LogError("MemoryBubble component not found on instantiated prefab!");
                continue;
            }

            script.clipUrl = mem.clipUrl;
        }
    }

    Vector3 RandomPosition()
    {
        return new Vector3(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(0.5f, 1.5f),
            UnityEngine.Random.Range(1f, 2f)
        );
    }
}