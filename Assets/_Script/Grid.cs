using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public GameObject groundPrefab; // 바닥으로 사용할 노드 프리팹
    GameObject parentGrid;          // 기준으로 사용할 부모

    public Vector2 gridWorldSize;   // 그리드 크기

    Node[,] grid;                   // 배치 노드 정보

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid(); // 그리드 생성
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 그리드 생성 함수
    public void CreateGrid()
    {
        // 기준으로 사용할 그리드 생성
        if (parentGrid != null)
            Destroy(parentGrid);

        parentGrid = new GameObject("parentGrid");

        grid = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];
        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * gridWorldSize.x / 2 - 
            Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < (int)gridWorldSize.x; x++)
        {
            for (int y = 0; y < (int)gridWorldSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + 
                    Vector3.forward * (y + 0.5f);
                GameObject obj = Instantiate(groundPrefab, worldPoint, Quaternion.Euler(90, 0, 0));
                obj.transform.parent = parentGrid.transform;
                grid[x, y] = new Node(obj, true, x, y);
            }
        }


    }

    
}
