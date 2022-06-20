using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ã�� �˰��� : AStartAlgorithm
public class AStartAlgorithm : MonoBehaviour
{
    public Grid grid;   // ��� ������ ���� �׸��� ���� ��������

    public void AStartAlgorithmStart()
    {
        Debug.Log("AStartAlgorithmStart!");

        // ���� Ȯ���ؾ��� ��� ����Ʈ
        List<Node> openNodeList = new List<Node>(); 
        // �̹� Ȯ���� ���� ��� ����Ʈ
        HashSet<Node> closedNodeList = new HashSet<Node>();

        // ó�� ������ ��� �߰�
        openNodeList.Add(grid.start);

        // Ȯ���� ��尡 ���� �� ���� �ݺ�
        while (openNodeList.Count > 0)
        {
            // Ȯ���� ��� ����Ʈ���� ��� ��������
            Node currentNode = openNodeList[0];
            Debug.Log("check node : " + currentNode);

            // Ȯ���� ��� �� �ڽ�Ʈ�� ���� ���� �ڵ带 ã�� ���� Ȯ��
            for (int i = 1; i < openNodeList.Count; i++)
            {
                if (openNodeList[i].fCost < currentNode.fCost || openNodeList[i].fCost == currentNode.fCost 
                    && openNodeList[i].hCost < currentNode.hCost)
                {
                    // Ȯ���� ��� �� �ڽ�Ʈ�� ���� ���� ���
                    currentNode = openNodeList[i];
                }
            }

            // Ȯ���� ��带 �̸� ����
            openNodeList.Remove(currentNode);
            closedNodeList.Add(currentNode);

            // Ȯ���� ��尡 �������� ��� ����
            if (currentNode == grid.end)
            {
                Debug.Log("AStartAlgorithm is end node");
                return;
            }

            // Ȯ�� ���� ����� ���� ����
            if (currentNode != grid.start)
            {
                currentNode.ChangeColor = Color.cyan;
            }

            // �̿� ��带 ������ Ȯ��
            foreach (Node neighbour in GetNeighboursNodes(currentNode))
            {
                // �̿� ��尡 ���̰ų� �̹� Ȯ���� ��ģ ����� ���
                if (neighbour.wall || closedNodeList.Contains(neighbour))
                {
                    continue;
                }

                // ����� �ڽ�Ʈ ���
                // ���� �ڽ�Ʈ = �̿� �������� ����ġ + ��ǥ �������� ����ġ
                int neighbourCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                Debug.Log("moveCost : " + neighbourCost);

                // ������ �ڽ�Ʈ ���� �۰ų� Ȯ���ؾ��� ��� ����Ʈ�� ���� ��� �ڽ�Ʈ ���
                if (neighbourCost < neighbour.gCost || !openNodeList.Contains(neighbour))
                {
                    // �ڽ�Ʈ ����
                    neighbour.gCost = neighbourCost;
                    neighbour.hCost = GetDistance(neighbour, grid.end);

                    // ��θ� ���� ���� ��� �� ����
                    neighbour.pastNode = currentNode;

                    // Ȯ���ؾ��� ��� ����Ʈ�� ���� ��� �߰�
                    if (!openNodeList.Contains(neighbour))
                    {
                        openNodeList.Add(neighbour);

                        // ��尡 ��, ������ ���� Ȯ�� �� ���� ����
                        if (!neighbour.wall && !neighbour.end)
                        {
                            neighbour.ChangeColor = Color.green;
                        }
                    }
                }

            }

        }
    }

    // Ư�� ����� �̿��� ã���ִ� �Լ�
    public List<Node> GetNeighboursNodes(Node node)
    {
        Debug.Log("GetNeighboursNodes!");

        List<Node> neighbours = new List<Node>();

        // ��� ����Ʈ�� �ε��� ������ ��ó ��� Ȯ��
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int x_ = node.x + x;
                int y_ = node.y + y;

                if (x_ >= 0 && x_ < (int)grid.gridWorldSize.x 
                    && y_ >= 0 && y_ < (int)grid.gridWorldSize.y)
                {
                    neighbours.Add(grid.nodeArray[x_, y_]);
                }
            }
        }

        return neighbours;
    }

    // Ư�� ��� ������ �Ÿ��� ����ϴ� �Լ�
    int GetDistance(Node node1, Node node2)
    {
        // �� ��� ������ ���̸� �������� ���
        int distance_x = Mathf.Abs(node1.x - node2.x);
        int distance_y = Mathf.Abs(node1.y - node2.y);

        Debug.Log("x : " + distance_x + ", y : " + distance_y);

        // �밢�� �� 14, ���� ���� �� 10���� �Ÿ� ��� 
        if (distance_x > distance_y)
            return 14 * distance_y + 10 * (distance_x - distance_y);
        return 14 * distance_x + 10 * (distance_y - distance_x);

    }
}
