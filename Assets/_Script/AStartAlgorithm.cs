using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 길찾기 알고리즘 : AStartAlgorithm
public class AStartAlgorithm : MonoBehaviour
{
    public Grid grid;   // 노드 정보를 위한 그리드 정보 가져오기

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
                if (openNodeList[i].fCost < currentNode.fCost || openNodeList[i].fCost == currentNode.fCost 
                    && openNodeList[i].hCost < currentNode.hCost)
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

            // 이웃 노드를 가져와 확인
            foreach (Node neighbour in GetNeighboursNodes(currentNode))
            {
                // 이웃 노드가 벽이거나 이미 확인을 마친 노드일 경우
                if (neighbour.wall || closedNodeList.Contains(neighbour))
                {
                    continue;
                }

                // 노드의 코스트 계산
                // 현재 코스트 = 이웃 노드까지의 가중치 + 목표 노드까지의 가중치
                int neighbourCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                Debug.Log("moveCost : " + neighbourCost);

                // 기존의 코스트 보다 작거나 확인해야할 노드 리스트에 없을 경우 코스트 계산
                if (neighbourCost < neighbour.gCost || !openNodeList.Contains(neighbour))
                {
                    // 코스트 설정
                    neighbour.gCost = neighbourCost;
                    neighbour.hCost = GetDistance(neighbour, grid.end);

                    // 경로를 위한 이전 노드 값 설정
                    neighbour.pastNode = currentNode;

                    // 확인해야할 노드 리스트에 없을 경우 추가
                    if (!openNodeList.Contains(neighbour))
                    {
                        openNodeList.Add(neighbour);

                        // 노드가 벽, 목적지 인지 확인 후 색상 변경
                        if (!neighbour.wall && !neighbour.end)
                        {
                            neighbour.ChangeColor = Color.green;
                        }
                    }
                }

            }

        }
    }

    // 특정 노드의 이웃을 찾아주는 함수
    public List<Node> GetNeighboursNodes(Node node)
    {
        Debug.Log("GetNeighboursNodes!");

        List<Node> neighbours = new List<Node>();

        // 노드 리스트의 인덱스 값으로 근처 노드 확인
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

    // 특정 노드 사이의 거리를 계산하는 함수
    int GetDistance(Node node1, Node node2)
    {
        // 두 노드 사이의 차이를 절댓값으로 계산
        int distance_x = Mathf.Abs(node1.x - node2.x);
        int distance_y = Mathf.Abs(node1.y - node2.y);

        Debug.Log("x : " + distance_x + ", y : " + distance_y);

        // 대각선 값 14, 가로 세로 값 10으로 거리 계산 
        if (distance_x > distance_y)
            return 14 * distance_y + 10 * (distance_x - distance_y);
        return 14 * distance_x + 10 * (distance_y - distance_x);

    }
}
