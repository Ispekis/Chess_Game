using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using subPattern;

namespace subPattern
{
    public class Pattern
    {
        public ArrayList Vertical = new ArrayList();
    }
}

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

    public Pattern getPawnPattern(int posX, int posY, int color)
    {
        Pattern pattern = new Pattern();
        Vector2 pos;

        pos.x = posX;
        pos.y = posY;
        switch (color)
        {
            case BLACK:
                for (int i = 0; i < 2; i++)
                {
                    pos.y -= 1;
                    pattern.Vertical.Add(pos);
                }
                break;
            case WHITE:
                for (int i = 0; i < 2; i++)
                {
                    pos.y += 1;
                    pattern.Vertical.Add(pos);
                }
                break;
        }
        return pattern;
    }

    public Pattern getRookPattern(int posX, int posY, int color)
    {
        Pattern pattern = new Pattern();
        Vector2 pos;

        pos.x = posX;
        pos.y = posY;
        switch (color)
        {
            case BLACK:
                for (int i = 0; i < 8; i++)
                {
                    pos.y -= 1;
                    pattern.Vertical.Add(pos);
                }
                break;
            case WHITE:
                for (int i = 0; i < 8; i++)
                {
                    pos.y += 1;
                    pattern.Vertical.Add(pos);
                }
                break;
        }
        return pattern;
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
