using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStartAlgorithm : MonoBehaviour
{
    public Grid grid;

    public void AStartAlgorithmStart()
    {
        Debug.Log("AStartAlgorithmStart!");

        // 현재 확인해야할 노드 리스트
        List<Node> openNodeList = new List<Node>(); 
        // 이미 확인이 끝난 노드 리스트
        HashSet<Node> closedNodeList = new HashSet<Node>();

        // 처음 시작할 노드 추가
        openNodeList.Add(grid.start);

        // 확인할 노드가 없을 때 까지 반복
        while (openNodeList.Count > 0)
        {
            // 확인할 노드 리스트에서 노드 가져오기
            Node currentNode = openNodeList[0];
            Debug.Log("check node : " + currentNode);

            // 확인할 노드 중 코스트가 제일 작은 코드를 찾기 위해 확인
            for (int i = 1; i < openNodeList.Count; i++)
            {
                if (openNodeList[i].cost < currentNode.cost || openNodeList[i].cost == currentNode.cost 
                    && openNodeList[i].cost < currentNode.cost)
                {
                    // 확인할 노드 중 코스트가 제일 작은 노드
                    currentNode = openNodeList[i];
                }
            }

            // 확인할 노드를 미리 제거
            openNodeList.Remove(currentNode);
            closedNodeList.Add(currentNode);

            // 확인할 노드가 목적지일 경우 종료
            if (currentNode == grid.end)
            {
                Debug.Log("AStartAlgorithm is end node");
                return;
            }

            // 확인 중인 노드의 색깔 변경
            if (currentNode != grid.start)
            {
                currentNode.ChangeColor = Color.cyan;
            }

            // 이웃 노드를 가져와 
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
