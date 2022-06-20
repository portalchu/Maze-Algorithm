using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 맵에 배치할 노드 스크립트
public class Node : MonoBehaviour
{
    public bool wall = false;   // 벽 여부
    public int x;               // x 위치
    public int y;               // y 위치

    public bool start = false;  // 시작점 여부
    public bool end = false;    // 끝점 여부

    public int gCost;           // 이웃 노드까지의 가중치
    public int hCost;           // 목표 노드까지의 가중치
    public Node pastNode;       // 이전 노드

    public TextMesh textMesh;   // 코스트 출력을 위한 텍스트 메쉬

    void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        // 코스트 출력
        if (fCost != 0)
        {
            textMesh.text = fCost.ToString();
        }
    }

    // 노드를 벽으로 변경
    public bool ChangeWall
    {
        set
        {
            Color color = value ? Color.gray : Color.white;
            wall = value;
            GetComponent<MeshRenderer>().material.color = color;
        }
    }

    // 노드를 시작 노드로 변경
    public bool ChangeStart
    {
        set
        {
            if (value)
            {
                start = value;
                Color color = Color.black;
                GetComponent<MeshRenderer>().material.color = color;
            }
            else
            {
                start = value;
                ChangeWall = wall;
            }
        }
    }

    // 노드를 목적지 노드로 변경
    public bool ChangeEnd
    {
        set
        {
            if (value)
            {
                end = value;
                Color color = Color.red;
                GetComponent<MeshRenderer>().material.color = color;
            }
            else
            {
                end = value;
                ChangeWall = wall;
            }
        }
    }

    // 노드 색깔 변경
    public Color ChangeColor
    {
        set
        {
            GetComponent<MeshRenderer>().material.color = value;
        }
    }

    // 최종 코스트 : 이웃 노드까지의 가중치 + 목표 노드까지의 가중치
    public int fCost
    {
        get { return gCost + hCost; }
    }
}
