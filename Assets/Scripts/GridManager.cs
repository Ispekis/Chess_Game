using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MACRO;

namespace MACRO
{
    public class param
    {
        public static int WIDTH = 0;
        public static int HEIGHT = 0;
        public const int WHITE = 0;
        public const int BLACK = 1;
    }
}

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _tilePrefab2;
    [SerializeField] private Camera cam;
    private Tile _null;

    [SerializeField] private Piece[] piecePrefab;

    private Piece[] pawn = new Piece[2];
    private Piece[] king = new Piece[2];
    private Piece[] knight = new Piece[2];
    private Piece[] rook = new Piece[2];
    private Piece[] bishop = new Piece[2];
    private Piece[] queen = new Piece[2];

    private void Awake()
    {
        param.WIDTH = _width;
        param.HEIGHT = _height;
        _null = null;
        InitPiece();
        GenerateGrid();
        GeneratePiece();
    }

    void setPieceParam(Piece piece, int color, string name)
    {
        piece.setColor(color);
    }

    // Check what is the piece and its color, then filling them in the right piece object
    void FillingPieceValue(string value, int color, int index)
    {
        switch (value)
        {
            case "bishop":
                bishop[color] = piecePrefab[index];
                setPieceParam(bishop[color], color, value);
                break;
            case "king":
                king[color] = piecePrefab[index];
                setPieceParam(bishop[color], color, value);
                break;
            case "queen":
                queen[color] = piecePrefab[index];
                setPieceParam(bishop[color], color, value);
                break;
            case "rook":
                rook[color] = piecePrefab[index];
                setPieceParam(bishop[color], color, value);
                break;
            case "pawn":
                pawn[color] = piecePrefab[index];
                setPieceParam(bishop[color], color, value);
                break;
            case "knight":
                knight[color] = piecePrefab[index];
                setPieceParam(bishop[color], color, value);
                break;
        }
    }

    // Filling the classe var white the piece's array in parameter
    void InitPiece()
    {
        for (int i = 0; i < piecePrefab.Length; i++)
        {
            string[] arr = piecePrefab[i].name.Split('_');
            if (arr[0] == "white")
            {
                FillingPieceValue(arr[1], param.WHITE, i);
            }
            if (arr[0] == "black")
            {
                FillingPieceValue(arr[1], param.BLACK, i);
            }
        }
    }

    void SettingMirror(int minX, int minY, int maxX, int maxY, Piece tile)
    {
        SettingPiece(minX, minY, maxX, maxY, tile);
        SettingPiece(minX + (_width - ((minX * 2) + 1)), minY, maxX, maxY, tile);
    }

    string RemoveCloneTag(string str)
    {
        int start = str.IndexOf("(");
        str = str.Remove(start);
        return str;
    }

    void SettingPiece(int minX, int minY, int maxX, int maxY, Piece tile)
    {
        for (int y = minY; y < maxY + minY; y++) {
            for (int x = minX; x < maxX + minX; x++) {
                var SpawnedPiece = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                SpawnedPiece.transform.localScale = Vector3.one * 0.3f;
                SpawnedPiece.setPosition(x, y);
                SpawnedPiece.name = RemoveCloneTag(SpawnedPiece.name);
            }
        }
    }

    void GeneratePiece()
    {
        SettingPiece(0, 1, 8, 1, pawn[param.WHITE]);
        SettingMirror(0, 0, 1, 1, rook[param.WHITE]);
        SettingMirror(1, 0, 1, 1, knight[param.WHITE]);
        SettingMirror(2, 0, 1, 1, bishop[param.WHITE]);
        SettingPiece(3, 0, 1, 1, queen[param.WHITE]);
        SettingPiece(4, 0, 1, 1, king[param.WHITE]);

        SettingPiece(0, 6, 8, 1, pawn[param.BLACK]);
        SettingMirror(0, 7, 1, 1, rook[param.BLACK]);
        SettingMirror(1, 7, 1, 1, knight[param.BLACK]);
        SettingMirror(2, 7, 1, 1, bishop[param.BLACK]);
        SettingPiece(3, 7, 1, 1, queen[param.BLACK]);
        SettingPiece(4, 7, 1, 1, king[param.BLACK]);
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedTile = _null;
                if ((y + x) % 2 == 1) {
                    spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                } else
                {
                    spawnedTile = Instantiate(_tilePrefab2, new Vector3(x, y), Quaternion.identity);
                }
                spawnedTile.name = $"Tile {x} {y}";
            }
        }

        // Set the position to the middle of the plate
        cam.transform.position = new Vector3((_width / 2) - 0.5f, (_height / 2) - 0.5f, cam.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        pawn[param.WHITE].posX = 0;
        pawn[param.BLACK].posX = 0;
        pawn[param.WHITE].posY = 0;
        pawn[param.BLACK].posY = 0;
        Debug.Log("Destroy");
    }
}
