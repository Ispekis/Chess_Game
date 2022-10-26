using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MACRO;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private Tile redHover;
    [SerializeField] private Tile eatMark;
    public Piece savePiece;

    private void Awake()
    {
    
    }
    void ResetAllPieceHover()
    {
        GameObject tile = GameObject.Find("red_hover");
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "path" || gameObj.name == "eat_mark")
            {
                Destroy(gameObj);
            }
        }
        Destroy(tile);
    }

    bool isOutBorder(int currentX, int currentY, int maxX, int maxY) // Check if the position exceed max pos
    {
        if (currentY < 0 || currentY > maxY)
        {
            return true;
        }
        if (currentX < 0 || currentX > maxX)
        {
            return true;
        }
        return false;
    }


    void CreateEatMark(Vector2 coo, Piece piece)
    {
        switch (piece.piece_name)
        {
            case "pawn":
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

    void SetEatMark(Vector2 coo, int color)
    {
        RaycastHit2D hit = Physics2D.Raycast(coo, Vector2.zero);
        if (hit.collider != null)
        {
            Piece hit_piece = hit.collider.GetComponent<Piece>();
            if (hit_piece.color != color)
            {
                var path = Instantiate(eatMark, new Vector3(coo.x, coo.y), Quaternion.identity);
                path.name = "eat_mark";
            }
        }
    }

    bool CheckCollideAndSetPath(Vector2 coo, Piece piece) // Check if the path can be place, if so then instantiate it
    {
        RaycastHit2D hit = Physics2D.Raycast(coo, Vector2.zero);
        if (hit.collider == null && isOutBorder((int)coo.x, (int)coo.y, param.WIDTH - 1, param.HEIGHT - 1) == false)
        {
            var path = Instantiate(redHover, new Vector3(coo.x, coo.y), Quaternion.identity);
            path.name = "path";
        }
        else
        {
            CreateEatMark(coo, piece);
            return true;
        }
        return false;
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
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                break;
            }
        }
    }

    void SetRookPath(Piece piece, int currentX, int currentY)
    {
        // Path to the top
        for (int i = 0; i < param.HEIGHT - currentY - 1; i++)
        {
            Vector2 vec;

            vec.y = currentY + i + 1;
            vec.x = currentX;
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
                break;
            }
        }

        // Path to the bottom
        for (int i = 0; i < currentY; i++)
        {
            Vector2 vec;

            vec.y = currentY - i - 1;
            vec.x = currentX;
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
                break;
            }
        }
        // Path to the right
        for (int i = 0; i < param.WIDTH - currentX - 1; i++)
        {
            Vector2 vec;

            vec.y = currentY;
            vec.x = currentX + i + 1;
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
                break;
            }
        }

        // Path to the left
        for (int i = 0; i < currentX; i++)
        {
            Vector2 vec;

            vec.y = currentY;
            vec.x = currentX - i - 1;
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
                break;
            }
        }
    }

    private void SetBishopPath(Piece piece, int currentX, int currentY)
    {
        // Path to diagonal top right
        for (int i = 0; i < param.WIDTH; i++)
        {
            Vector2 vec;

            vec.y = currentY + i + 1;
            vec.x = currentX + i + 1;
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
                break;
            }
        }

        // Path to diagonal top right
        for (int i = 0; i < param.WIDTH; i++)
        {
            Vector2 vec;

            vec.y = currentY - i - 1;
            vec.x = currentX + i + 1;
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
                break;
            }
        }

        // Path to diagonal top left
        for (int i = 0; i < param.WIDTH; i++)
        {
            Vector2 vec;

            vec.y = currentY + i + 1;
            vec.x = currentX - i - 1;
            RaycastHit2D hit = Physics2D.Raycast(vec, Vector2.zero);
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
                break;
            }
        }

        // Path to diagonal bot left
        for (int i = 0; i < param.WIDTH; i++)
        {
            Vector2 vec;

            vec.y = currentY - i - 1;
            vec.x = currentX - i - 1;
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
                break;
            }
        }
    }

    void SetQueenPath(Piece piece, int currentX, int currentY)
    {
        SetBishopPath(piece, currentX, currentY);
        SetRookPath(piece, currentX, currentY);
    }


    void SetKingPath(Piece piece, int currentX, int currentY)
    {
        Debug.Log("Set King Path");
        Vector2 vec;

        // Start at the top left
        vec.y = currentY + 1;
        vec.x = currentX - 1;

        for (int i = 0; i < 3; i++)
        {
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
            }
            vec.x += 1;
        }

        // Set position to the left
        vec.y = currentY;
        vec.x = currentX - 1;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        // Set position to the right
        vec.x = currentX + 1;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        vec.y = currentY - 1;
        vec.x = currentX - 1;
        // Set position to the bot left
        for (int i = 0; i < 3; i++)
        {
            if (CheckCollideAndSetPath(vec, piece) == true)
            {
                SetEatMark(vec, piece.color);
            }
            vec.x += 1;
        }

    }


    void setKnightPath(Piece piece, int currentX, int currentY)
    {
        Vector2 vec;

        // Set position to top left
        vec.y = currentY + 2;
        vec.x = currentX - 1;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        // Set position to top right
        vec.x = currentX + 1;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        // Set position to bot left
        vec.y = currentY - 2;
        vec.x = currentX - 1;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        // Set position to bot right
        vec.x = currentX + 1;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        // Set position to right top
        vec.y = currentY + 1;
        vec.x = currentX - 2;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        // Set position to right bot
        vec.y = currentY - 1;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        // Set position to left top
        vec.y = currentY + 1;
        vec.x = currentX + 2;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }

        // Set position to left bot
        vec.y = currentY - 1;
        if (CheckCollideAndSetPath(vec, piece) == true)
        {
            SetEatMark(vec, piece.color);
        }
    }
    // Check what piece is clicked and create path
    void CreateMoveOption(GameObject gameObject, int posX, int posY)
    {
        Piece piece = gameObject.GetComponent<Piece>();
        savePiece = piece;

        if (piece != null)
        {
            switch (piece.piece_name)
            {
                case "pawn":
                    SetPawnPath(piece, posX, posY);
                    break;
                case "bishop":
                    SetBishopPath(piece, posX, posY);
                    break;
                case "rook":
                    SetRookPath(piece, posX, posY);
                    break;
                case "queen":
                    SetQueenPath(piece, posX, posY);
                    break;
                case "king":
                    SetKingPath(piece, posX, posY);
                    break;
                case "knight":
                    setKnightPath(piece, posX, posY);
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
        if (gameObject.name == "path") {
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
                Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "piece" || hit.collider.gameObject.name == "path")
                {
                    CheckPieceMovement(hit.collider.gameObject, mousePos2D);
                }
            }
        }
    }
}
