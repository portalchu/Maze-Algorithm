using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool wall = false;           // �� ����
    public int x;               // x ��ġ
    public int y;               // y ��ġ

    public bool start = false;
    public bool end = false;

    public int cost = 1000000;
    public Node pastNode;

    public TextMesh textMesh;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cost < 100000)
        {
            textMesh.text = cost.ToString();
        }
    }

    // ��� ������
    public Node(bool _wall, int _x, int _y)
    {
        wall = _wall;
        x = _x;
        y = _y;
    }

    public bool ChangeWall
    {
        set
        {
            Color color = value ? Color.gray : Color.white;
            wall = value;
            GetComponent<MeshRenderer>().material.color = color;
        }
    }

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

    public Color ChangeColor
    {
        set
        {
            GetComponent<MeshRenderer>().material.color = value;
        }
    }
}
