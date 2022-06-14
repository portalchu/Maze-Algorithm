using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject ground;   // 노드로 사용할 프리팹
    public bool wall;           // 벽 여부
    public int x;               // x 위치
    public int y;               // y 위치

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 노드 생성자
    public Node(GameObject _ground, bool _wall, int _x, int _y)
    {
        ground = _ground;
        wall = _wall;
        x = _x;
        y = _y;
    }
}
