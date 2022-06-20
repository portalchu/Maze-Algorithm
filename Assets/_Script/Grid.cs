using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ��ġ�� ���� �׸��� ���� ��ũ��Ʈ
public class Grid : MonoBehaviour
{
    public GameObject nodePrefab; // �ٴ����� ����� ��� ������
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

        // ��� ����� �迭 ����
        nodeArray = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];

        // �θ� �׸��� ����
        parentGrid = new GameObject("parentGrid");

        if (nodeArray == null)
        {
            Debug.Log("no create grid");
        }

        // �׸��带 �߾ӿ� ���� ���� �׸��� ũ�⸸ŭ ����� ������ �� ������ ���
        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * gridWorldSize.x / 2 - 
            Vector3.forward * gridWorldSize.y / 2;

        Debug.Log("worldBottomLeft : " + worldBottomLeft);

        // ��ġ�� ��� ����
        for (int x = 0; x < (int)gridWorldSize.x; x++)
        {
            for (int y = 0; y < (int)gridWorldSize.y; y++)
            {
                // ���������� ��带 ��ġ�� �߽� ��ġ ���
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + 
                    Vector3.forward * (y + 0.5f);

                // ��� ����
                GameObject obj = Instantiate(nodePrefab, worldPoint, Quaternion.Euler(90, 0, 0));
                obj.transform.parent = parentGrid.transform; // �θ� �׸��忡 ��� �ڽ����� ����
                obj.name += x + ", " + y; // �̸� ����

                // ��� ����
                Node node = obj.GetComponent<Node>();
                node.x = x;
                node.y = y;

                // ��� ����Ʈ�� �߰�
                nodeArray[x, y] = node;

                // ���� ����� ��� ���� ��� ���� �߰�
                if (x == 0 && y == 0)
                {
                    node.ChangeStart = true;
                    start = node;
                }

                // ������ ����� ��� ������ ��� ���� �߰�
                if (x == (int)gridWorldSize.x - 1 && y == (int)gridWorldSize.y - 1)
                {
                    node.ChangeEnd = true;
                    end = node;
                }
            }
        }

    }
}
