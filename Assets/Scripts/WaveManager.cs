using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject prefab;
    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(prefab, new Vector2(-7.5f,-3.5f), Quaternion.identity);
        }
    }
}