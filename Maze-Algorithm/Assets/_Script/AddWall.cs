using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Node node;
        }
    }

    public Node WallSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject ground = hit.collider.gameObject;
            //return 
        }


        return null;
    }
}
