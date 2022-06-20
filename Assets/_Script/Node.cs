using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ʿ� ��ġ�� ��� ��ũ��Ʈ
public class Node : MonoBehaviour
{
    public bool wall = false;   // �� ����
    public int x;               // x ��ġ
    public int y;               // y ��ġ

    public bool start = false;  // ������ ����
    public bool end = false;    // ���� ����

    public int gCost;           // �̿� �������� ����ġ
    public int hCost;           // ��ǥ �������� ����ġ
    public Node pastNode;       // ���� ���

    public TextMesh textMesh;   // �ڽ�Ʈ ����� ���� �ؽ�Ʈ �޽�

    void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        // �ڽ�Ʈ ���
        if (fCost != 0)
        {
            textMesh.text = fCost.ToString();
        }
    }

    // ��带 ������ ����
    public bool ChangeWall
    {
        set
        {
            Color color = value ? Color.gray : Color.white;
            wall = value;
            GetComponent<MeshRenderer>().material.color = color;
        }
    }

    // ��带 ���� ���� ����
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

    // ��带 ������ ���� ����
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

    // ��� ���� ����
    public Color ChangeColor
    {
        set
        {
            GetComponent<MeshRenderer>().material.color = value;
        }
    }

    // ���� �ڽ�Ʈ : �̿� �������� ����ġ + ��ǥ �������� ����ġ
    public int fCost
    {
        get { return gCost + hCost; }
    }
}
