using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int color;
    public int posX;
    public int posY;
    public string piece_name;
    public int plate_width;
    public int plate_height;

    private const int WHITE = 0;
    private const int BLACK = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPieceName(string piece_name)
    {
        this.piece_name = piece_name;
    }

    public void setPosition(int posX, int posY)
    {
        this.posX = posX;
        this.posY = posY;
    }

    public void setColor(int color)
    {
        this.color = color;
    }

    public int GetColor()
    {
        return this.color;
    }
}
