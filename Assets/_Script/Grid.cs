using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 노드 배치를 위한 그리드 관련 스크립트
public class Grid : MonoBehaviour
{
    public GameObject nodePrefab; // 바닥으로 사용할 노드 프리팹
    GameObject parentGrid;          // 기준으로 사용할 부모

    public Vector2 gridWorldSize;   // 그리드 크기

    public Node[,] nodeArray;       // 배치 노드 정보

    public Node start;              // 시작 노드
    public Node end;                // 끝 노드

    void Start()
    {
        CreateGrid(); // 그리드 생성
    }

    // 그리드 생성 함수
    public void CreateGrid()
    {
        // 기준으로 사용할 그리드 생성
        if (parentGrid != null)
            Destroy(parentGrid);

        // 노드 저장용 배열 생성
        nodeArray = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];

        // 부모 그리드 생성
        parentGrid = new GameObject("parentGrid");

        if (nodeArray == null)
        {
            Debug.Log("no create grid");
        }

        // 그리드를 중앙에 놓기 위해 그리드 크기만큼 계산해 기준이 될 기준점 계산
        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * gridWorldSize.x / 2 - 
            Vector3.forward * gridWorldSize.y / 2;

        Debug.Log("worldBottomLeft : " + worldBottomLeft);

        // 배치할 노드 생성
        for (int x = 0; x < (int)gridWorldSize.x; x++)
        {
            for (int y = 0; y < (int)gridWorldSize.y; y++)
            {
                // 기준점에서 노드를 배치할 중심 위치 계산
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + 
                    Vector3.forward * (y + 0.5f);

                // 노드 생성
                GameObject obj = Instantiate(nodePrefab, worldPoint, Quaternion.Euler(90, 0, 0));
                obj.transform.parent = parentGrid.transform; // 부모 그리드에 노드 자식으로 설정
                obj.name += x + ", " + y; // 이름 설정

                // 노드 세팅
                Node node = obj.GetComponent<Node>();
                node.x = x;
                node.y = y;

                // 노드 리스트에 추가
                nodeArray[x, y] = node;

                // 시작 노드의 경우 시작 노드 설정 추가
                if (x == 0 && y == 0)
                {
                    node.ChangeStart = true;
                    start = node;
                }

                // 목적지 노드의 경우 목적지 노드 설정 추가
                if (x == (int)gridWorldSize.x - 1 && y == (int)gridWorldSize.y - 1)
                {
                    node.ChangeEnd = true;
                    end = node;
                }
            }
        }

    }
}
