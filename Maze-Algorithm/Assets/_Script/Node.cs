using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject ground;   // ���� ����� ������
    public bool wall;           // �� ����
    public int x;               // x ��ġ
    public int y;               // y ��ġ

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ��� ������
    public Node(GameObject _ground, bool _wall, int _x, int _y)
    {
        ground = _ground;
        wall = _wall;
        x = _x;
        y = _y;
    }
}
