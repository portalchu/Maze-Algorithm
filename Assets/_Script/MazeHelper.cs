using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 노드 수정 관련 스크립트
public class MazeHelper : MonoBehaviour
{
    public Grid grid;       // 노드 정보를 위한 그리드 정보 가져오기
    bool isClick = false;   // 클릭 중인지 확인

    void Update()
    {
        // 클릭 중인지 확인
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭한 노드 정보 가져오기
            Node node = CheckNode();

            if (node == null)
            {
                return;
            }

            
            if (node.start || node.end)
            {
                // 시작 및 끝 노드일 경우 변경만 가능
                StartCoroutine("ChangeStartEndCheck", node);
            }
            else
            {
                // 나머지 노드의 경우 벽 또는 일반 노드로 전환
                StartCoroutine("ChangeWallCheck", node);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    // 클릭한 노드 확인
    public Node CheckNode()
    {
        // 마우스로 부터 레이캐스트 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 부딪힌 물체 정보 확인
        if (!Physics.Raycast(ray, out hit, 1000f))
        {
            return null;
        }

        // 노드인지 확인
        if (hit.transform.gameObject.tag != "Node")
        {
            return null;
        }

        // 노드 정보 가져오기
        Node node = hit.transform.GetComponent<Node>();

        if (node == null)
        {
            return null;
        }

        return node;
    }

    // 노드를 벽 또는 일반으로 변환
    IEnumerator ChangeWallCheck(Node node)
    {
        // 노드가 젹인지 아닌지 확인
        bool wallCheck = !node.wall;

        // 버튼을 누루고 있을 경우
        while (Input.GetMouseButton(0))
        {
            // 클릭한 노드 정보 가져오기
            node = CheckNode();

            if (node != null)
            {
                // 노드를 벽 또는 일반 노드로 변환
                node.ChangeWall = wallCheck;
            }

            yield return null;
        }
    }

    // 시작 및 끝 노드 변경
    IEnumerator ChangeStartEndCheck(Node node)
    {
        // 노드의 정보 확인 및 저장
        bool start = node.start;
        Node oldNode = node;

        // 버튼을 누루고 있을 경우
        while (Input.GetMouseButton(0))
        {
            // 클릭한 노드 정보 가져오기
            node = CheckNode();

            // 이전 노드가 아닐 경우
            if (node != null && node != oldNode)
            {
                // 시작 노드를 바꿀 경우
                if (start && !node.end)
                {
                    node.ChangeStart = true;
                    oldNode.ChangeStart = false;
                    oldNode = node;
                    grid.start = node;
                }
                // 끝 노드를 바꿀 경우
                else if (!start && !node.start)
                {
                    node.ChangeEnd = true;
                    oldNode.ChangeEnd = false;
                    oldNode = node;
                    grid.end = node;
                }
            }

            yield return null;
        }
    }
}
