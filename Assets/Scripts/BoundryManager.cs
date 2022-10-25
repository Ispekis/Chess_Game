using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryManager : MonoBehaviour
{
    Camera cam;
    float width;
    float height;
    public EdgeCollider2D edge;
    private void Awake()
    {
        cam = Camera.main;
        edge = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FindBoundries();
        SetBounds();
    }

    void SetBounds()
    {
        Vector2 pointa = new Vector2(width / 2, height / 2);
        Vector2 pointb = new Vector2(width / 2, -height / 2);
        Vector2 pointc = new Vector2(-width / 2, -height / 2);
        Vector2 pointd = new Vector2(-width / 2, height / 2);
        Vector2[] tempArray = new Vector2[] { pointa, pointb, pointc, pointd, pointa };
        edge.points = tempArray;
    }

    void FindBoundries()
    {
        width = 1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f);
        height = 1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).y - .5f);

    }
}
