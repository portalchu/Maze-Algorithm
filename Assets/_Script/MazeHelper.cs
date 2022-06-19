using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeHelper : MonoBehaviour
{
    public Grid grid;
    bool isClick = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Node node = CheckNode();

            if (node == null)
            {
                Debug.Log("No result node");
                return;
            }

            if (node.start || node.end)
            {
                StartCoroutine("ChangeStartEndCheck", node);
            }
            else
            {
                StartCoroutine("ChangeWallCheck", node);
            }
        }
    }

    public Node CheckNode()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 1000f))
        {
            Debug.Log("No hit anything");
            return null;
        }

        if (hit.transform.gameObject.tag != "Node")
        {
            Debug.Log("No hit node");
            return null;
        }

        Node node = hit.transform.GetComponent<Node>();

        if (node == null)
        {
            Debug.Log("No component node");
            return null;
        }

        Debug.Log("Check Node!");

        return node;
    }

    IEnumerator ChangeWallCheck(Node node)
    {
        bool wallCheck = !node.wall;

        Debug.Log("ChangeWallCheck!");
        while (Input.GetMouseButton(0))
        {
            node = CheckNode();

            if (node != null)
            {
                node.ChangeWall = wallCheck;
            }

            yield return null;
        }
    }

    IEnumerator ChangeStartEndCheck(Node node)
    {
        bool start = node.start;
        Node oldNode = node;

        Debug.Log("ChangeStartEndCheck!");
        while (Input.GetMouseButton(0))
        {
            node = CheckNode();

            if (node != null && node != oldNode)
            {
                if (start && !node.end)
                {
                    node.ChangeStart = true;
                    oldNode.ChangeStart = false;
                    oldNode = node;
                    grid.start = node;
                }
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
