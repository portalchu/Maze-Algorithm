using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject groundPrefab; // �ٴ����� ����� ��� ������
    GameObject parentGrid;          // �������� ����� �θ�

    public Vector2 gridWorldSize;   // �׸��� ũ��

    public Node[,] nodeArray;       // ��ġ ��� ����

    public Node start;              // ���� ���
    public Node end;                // �� ���

    void Start()
    {
        CreateGrid(); // �׸��� ����
    }

    // �׸��� ���� �Լ�
    public void CreateGrid()
    {
        // �������� ����� �׸��� ����
        if (parentGrid != null)
            Destroy(parentGrid);

        nodeArray = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];

        parentGrid = new GameObject("parentGrid");

        if (nodeArray == null)
        {
            Debug.Log("no create grid");
        }

        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * gridWorldSize.x / 2 - 
            Vector3.forward * gridWorldSize.y / 2;

        Debug.Log("worldBottomLeft : " + worldBottomLeft);

        for (int x = 0; x < (int)gridWorldSize.x; x++)
        {
            for (int y = 0; y < (int)gridWorldSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + 
                    Vector3.forward * (y + 0.5f);

                GameObject obj = Instantiate(groundPrefab, worldPoint, Quaternion.Euler(90, 0, 0));
                obj.transform.parent = parentGrid.transform;
                obj.name += x + ", " + y;

                Node node = obj.GetComponent<Node>();
                node.x = x;
                node.y = y;

                nodeArray[x, y] = node;

                if (x == 0 && y == 0)
                {
                    node.ChangeStart = true;
                    node.cost = 0;
                    start = node;
                }

                if (x == (int)gridWorldSize.x - 1 && y == (int)gridWorldSize.y - 1)
                {
                    node.ChangeEnd = true;
                    end = node;
                }
            }
        }

    }

    public void CheckGrid()
    {
        for (int x = 0; x < (int)gridWorldSize.x; x++)
        {
            for (int y = 0; y < (int)gridWorldSize.y; y++)
            {
                if (nodeArray[x, y] == null)
                {
                    Debug.Log("no grid");
                    return;
                }

                Debug.Log("grid[" + x + "][" + y + "] = " + nodeArray[x, y].name);
            }
        }
    }
}
