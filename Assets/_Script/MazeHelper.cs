using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���� ���� ��ũ��Ʈ
public class MazeHelper : MonoBehaviour
{
    public Grid grid;       // ��� ������ ���� �׸��� ���� ��������
    bool isClick = false;   // Ŭ�� ������ Ȯ��

    void Update()
    {
        // Ŭ�� ������ Ȯ��
        if (Input.GetMouseButtonDown(0))
        {
            // Ŭ���� ��� ���� ��������
            Node node = CheckNode();

            if (node == null)
            {
                return;
            }

            
            if (node.start || node.end)
            {
                // ���� �� �� ����� ��� ���游 ����
                StartCoroutine("ChangeStartEndCheck", node);
            }
            else
            {
                // ������ ����� ��� �� �Ǵ� �Ϲ� ���� ��ȯ
                StartCoroutine("ChangeWallCheck", node);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    // Ŭ���� ��� Ȯ��
    public Node CheckNode()
    {
        // ���콺�� ���� ����ĳ��Ʈ ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // �ε��� ��ü ���� Ȯ��
        if (!Physics.Raycast(ray, out hit, 1000f))
        {
            return null;
        }

        // ������� Ȯ��
        if (hit.transform.gameObject.tag != "Node")
        {
            return null;
        }

        // ��� ���� ��������
        Node node = hit.transform.GetComponent<Node>();

        if (node == null)
        {
            return null;
        }

        return node;
    }

    // ��带 �� �Ǵ� �Ϲ����� ��ȯ
    IEnumerator ChangeWallCheck(Node node)
    {
        // ��尡 ������ �ƴ��� Ȯ��
        bool wallCheck = !node.wall;

        // ��ư�� ����� ���� ���
        while (Input.GetMouseButton(0))
        {
            // Ŭ���� ��� ���� ��������
            node = CheckNode();

            if (node != null)
            {
                // ��带 �� �Ǵ� �Ϲ� ���� ��ȯ
                node.ChangeWall = wallCheck;
            }

            yield return null;
        }
    }

    // ���� �� �� ��� ����
    IEnumerator ChangeStartEndCheck(Node node)
    {
        // ����� ���� Ȯ�� �� ����
        bool start = node.start;
        Node oldNode = node;

        // ��ư�� ����� ���� ���
        while (Input.GetMouseButton(0))
        {
            // Ŭ���� ��� ���� ��������
            node = CheckNode();

            // ���� ��尡 �ƴ� ���
            if (node != null && node != oldNode)
            {
                // ���� ��带 �ٲ� ���
                if (start && !node.end)
                {
                    node.ChangeStart = true;
                    oldNode.ChangeStart = false;
                    oldNode = node;
                    grid.start = node;
                }
                // �� ��带 �ٲ� ���
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
