using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStartAlgorithm : MonoBehaviour
{
    public Grid grid;

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
                if (openNodeList[i].cost < currentNode.cost || openNodeList[i].cost == currentNode.cost 
                    && openNodeList[i].cost < currentNode.cost)
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

            // �̿� ��带 ������ 
            foreach (Node neighbour in GetNeighboursNodes(currentNode))
            {
                if (neighbour.wall || closedNodeList.Contains(neighbour))
                {
                    Debug.Log("this node is already count or wall : " + neighbour.name);
                    continue;
                }

                int moveCost = currentNode.cost + 1;
                Debug.Log("moveCost : " + moveCost);


                if (moveCost < neighbour.cost || !openNodeList.Contains(neighbour))
                {
                    neighbour.cost = moveCost;
                    neighbour.pastNode = currentNode;

                    if (!openNodeList.Contains(neighbour))
                    {
                        openNodeList.Add(neighbour);
                        if (!neighbour.wall && !neighbour.end)
                        {
                            neighbour.ChangeColor = Color.green;
                        }
                    }
                }

            }

        }
    }

    public List<Node> GetNeighboursNodes(Node node)
    {
        Debug.Log("GetNeighboursNodes!");

        List<Node> neighbours = new List<Node>();
        int[,] near = {
            { 0, 1 }, 
            { 1, 0 }, 
            { 0, -1 }, 
            { -1, 0 } 
        };

        for (int i = 0; i < 4; i++)
        {
            int x_ = node.x + near[i, 0];
            int y_ = node.y + near[i, 1];

            Debug.Log("x_ : " + x_);
            Debug.Log("y_ : " + y_);

            if (x_ >= 0 && x_ < (int)grid.gridWorldSize.x 
                && y_ >= 0 && y_ < (int)grid.gridWorldSize.y)
            {
                Debug.Log("find neighbours : " + grid.nodeArray[x_, y_].name);
                neighbours.Add(grid.nodeArray[x_, y_]);
            }
            else
            {
                Debug.Log("there no neighbours");
            }
        }

        return neighbours;
    }
}
