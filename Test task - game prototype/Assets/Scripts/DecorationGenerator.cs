using System.Collections.Generic;
using UnityEngine;

public class DecorationGenerator : MonoBehaviour
{
    public GameObject[] decorationPrefab;
    public List<GameObject> allDecoration;


    public void Decorate()
    { 
        for (int x = -20; x < 50; x += 10)
        {
            for (int z = 0; z < 100; z +=10)
            {
              
                    Vector3 position = new Vector3(x + Random.Range(-10, 10), Random.Range(0, 2), z + Random.Range(-10, 10));
                if (position.x < -4 || position.x > 4)
                {  GameObject decor = Instantiate(decorationPrefab[Random.Range(0, decorationPrefab.Length)], position, Quaternion.identity);
                    allDecoration.Add(decor);
                }
            }
        }
    }
    public void Clear()
    {
        if (allDecoration.Count > 0)
        {
            for (int i = 0; i < allDecoration.Count; i++)
            {
                Destroy(allDecoration[i]);
            }
            allDecoration.Clear();
        }
    }
}
