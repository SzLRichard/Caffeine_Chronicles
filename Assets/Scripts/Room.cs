using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    public uint number_of_enemies;
    void Start()
    {
        
    }

    List<GameObject> FindChildrenWithTags(GameObject parent, params string[] tags)
    {
        List<GameObject> matchingChildren = new List<GameObject>();

        // Iterate through children of the parent object
        foreach (Transform child in parent.transform)
        {
            // Check if the child has any of the specified tags
            foreach (string tag in tags)
            {
                if (child.CompareTag(tag))
                {
                    matchingChildren.Add(child.gameObject);
                    break; // Exit inner loop once tag is found
                }
            }

            // Recursively search child's children
            matchingChildren.AddRange(FindChildrenWithTags(child.gameObject, tags));
        }

        return matchingChildren;
    }

    // Update is called once per frame
    void Update()
    {
        if (number_of_enemies == 0)
        {
            string[] door_tags = { "Door" };
            List<GameObject> matchingChildren = FindChildrenWithTags(gameObject, door_tags);
            foreach (GameObject child in matchingChildren)
            {
                Collider2D col2D = child.GetComponent<Collider2D>();
                Collider col = child.GetComponent<Collider>();

                // Set isTrigger to true if a Collider2D is found
                if (col2D != null)
                {
                    col2D.isTrigger = true;
                }

                // Set isTrigger to true if a Collider (3D) is found
                if (col != null)
                {
                    col.isTrigger = true;
                }
            }
        }
        else
        {
            List<GameObject> matchingChildren = FindChildrenWithTags(gameObject, "Enemy");
            if (matchingChildren.Count == 0) number_of_enemies = 0;
        }
    }
}
