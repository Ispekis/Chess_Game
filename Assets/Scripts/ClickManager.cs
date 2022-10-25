using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using subPattern;
using static UnityEditor.PlayerSettings;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private Tile redHover;
    public Piece savePiece;

    private void Awake()
    {
        
    }
    void ResetAllPieceHover()
    {
        GameObject tile = GameObject.Find("red_hover");
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "path")
            {
                Destroy(gameObj);
            }
        }
        Destroy(tile);
    }

    ArrayList RemoveAllVertical(ArrayList pattern, int index, int posY)
    {
        Vector2 firstPath = (Vector2)pattern[index];
        if (firstPath.y > posY)
        {
            for (int i = 0; i < pattern.Count; i++)
            {
                Vector2 pos = (Vector2)pattern[i];
                if (pos.y > firstPath.y)
                {
                    pattern.RemoveAt(i);
                }
            }
        }
        if (firstPath.y < posY)
        {
            for (int i = 0; i < pattern.Count; i++)
            {
                Vector2 pos = (Vector2)pattern[i];
                if (pos.y < firstPath.y)
                {
                    pattern.RemoveAt(i);
                    i--;
                }
            }
        }
        pattern.RemoveAt(index);
        return pattern;
    }

    Pattern CheckCollide(Pattern pattern, int posX, int posY) 
    {
        if (pattern.Vertical != null)
        {

            for (int i = 0; i < pattern.Vertical.Count; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast((Vector2)pattern.Vertical[i], Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(pattern.Vertical[i]);
                    if (hit.collider.gameObject.transform.position.x == posX)
                    {
                        Debug.Log("Vertical");
                        RemoveAllVertical(pattern.Vertical, i, posY);
                    }
                }
            }
        }
        return pattern;
    }


    void SetPawnPath(Piece piece, int posX, int posY)
    {
        for (int i = 0; i < 2; i++)
        {
            Vector2 vec;

            if (piece.color == 0)
            {
                vec.y = posY + i + 1;
                vec.x = posX;
            }
            else
            {
                vec.y = posY - i - 1;
                vec.x = posX;
            }
            RaycastHit2D hit = Physics2D.Raycast(vec, Vector2.zero);
            if (hit.collider == null)
            {
                var path = Instantiate(redHover, new Vector3(vec.x, vec.y), Quaternion.identity);
                path.name = "path";
            }
            else
            {
                break;
            }
        }
    }

    void SetRookPath(Piece piece, int currentX, int currentY)
    {

    }

    // Check what piece is clicked and create path
    void CreateMoveOption(GameObject gameObject, int posX, int posY)
    {
        Piece piece = gameObject.GetComponent<Piece>();
        savePiece = piece;
        Pattern pattern;

        if (piece != null)
        {
            switch (piece.piece_name)
            {
                case "pawn":
                    SetPawnPath(piece, posX, posY);
                    break;
                case "bishop":
                    break;
                case "rook":
                    break;
                case "queen":
                    break;
                case "king":
                    break;
                case "knight":
                    break;

            }
        }
    }

    void MovePiece(int posX, int posY)
    {
        savePiece.transform.position = new Vector3(posX, posY, 0);
        savePiece.setPosition(posX, posY);
    }

    void CheckPieceMovement(GameObject gameObject, Vector2 mousePos)
    {
        int intX = Convert.ToInt32(mousePos.x);
        int intY = Convert.ToInt32(mousePos.y);

        ResetAllPieceHover();
        var spawnHover = Instantiate(redHover, new Vector3(intX, intY), Quaternion.identity);
        if (gameObject.name == "path")
        {
            MovePiece(intX, intY);
        }
        CreateMoveOption(gameObject, intX, intY);
        spawnHover.name = "red_hover";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                CheckPieceMovement(hit.collider.gameObject, mousePos2D);
            }
        }
    }
}
