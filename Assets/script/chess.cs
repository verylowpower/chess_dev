using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;


public class Unit
{
    public Sprite king, queen, pawn, knight, bishop, rook;

    public Unit(Sprite king, Sprite queen, Sprite pawn, Sprite knight, Sprite bishop, Sprite rook)
    {
        this.king = king; 
        this.queen = queen; 
        this.pawn = pawn;
        this.knight = knight; 
        this.bishop = bishop;
        this.rook = rook;
    }
}



public class chess : MonoBehaviour
{

    public GameObject controller;
    public GameObject moveplate;

    //vi tri

    private int X_board = -1;
    private int Y_board = -1;

    //track 2 nguoi choi

    private string player;

    //tham chieu cho tat ca sprites cua chesspiece
    [SerializeField] Sprite black_king, black_queen, black_pawn, black_knight, black_bishop, black_rook;
    [SerializeField] Sprite white_king, white_queen, white_pawn, white_knight, white_bishop, white_rook;

    Unit white;
    Unit black;

    private void Awake()
    {
        white = new Unit(white_king, white_queen, white_pawn, white_knight, white_bishop, white_rook);
        black = new Unit(black_king, black_queen, black_pawn, black_knight, black_bishop, black_rook);
    }

    public void Activate() //khi mot chesspiece dc tao ra thi ham nay se dc goi
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //xac dinh vi tri quan co
        SetCoords();

        switch (this.name) //tao sprite cho cac quan co
        {
            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black.queen;
                break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black.king;
                break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black.knight;
                break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black.bishop;
                break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black.pawn;
                break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black.rook;
                break;


            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white.queen;
                break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white.king;
                break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white.knight;
                break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white.bishop;
                break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white.pawn;
                break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white.rook;
                break;
        }
    }

    public void SetCoords()
    {
        float x = X_board;
        float y = Y_board;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);

    }

    //dung de truy cap vao toa do X va Y
    public int GetXboard()
    {
        return X_board;
    }

    public int GetYBoard()
    {
        return Y_board;
    }

    public void SetXboard(int x)
    {
        X_board = x;
    }

    public void SetYboard(int y)
    {
        Y_board = y;
    }

    public void OnMouseUp()
    {
        DestroyMovePlate();

        InitiateMovePlate();
    }

    public void DestroyMovePlate()
    {
        GameObject[] movePlate = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlate.Length; i++)
        {
            Destroy(movePlate[i]);
        }
    }

    //khai bao va khoi tao nuoc di cho tung quan
    public void InitiateMovePlate()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(0, 1);
                LineMovePlate(1, 0);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(X_board, Y_board - 1);
                break;
            case "white_pawn":
                PawnMovePlate(X_board, Y_board + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        gamecontrol sc = controller.GetComponent<gamecontrol>();

        int x = X_board + xIncrement;
        int y = Y_board + yIncrement;

        while (sc.PositionsBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if(sc.PositionsBoard(x, y)&&sc.GetPosition(x, y).GetComponent<chess>().player != player)
        {
            MovePlateAtkSpawn(x, y);
        } 
    }
    public void LMovePlate()
    {
        PointMovePlate(X_board + 1, Y_board + 2);
        PointMovePlate(X_board - 1, Y_board + 2);
        PointMovePlate(X_board + 2, Y_board + 1);
        PointMovePlate(X_board + 2, Y_board - 1);
        PointMovePlate(X_board + 1, Y_board - 2);
        PointMovePlate(X_board - 1, Y_board - 2);
        PointMovePlate(X_board - 2, Y_board + 1);
        PointMovePlate(X_board - 2, Y_board - 1);
    }

    public void SMovePlate()
    {
        PointMovePlate(X_board, Y_board + 1);
        PointMovePlate(X_board, Y_board - 1);
        PointMovePlate(X_board - 1, Y_board - 1);
        PointMovePlate(X_board - 1, Y_board + 1);
        PointMovePlate(X_board - 1, Y_board - 0);
        PointMovePlate(X_board + 1, Y_board - 1);
        PointMovePlate(X_board + 1, Y_board - 0);
        PointMovePlate(X_board + 1, Y_board + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        gamecontrol sc = controller.GetComponent<gamecontrol>();

        if(sc.PositionsBoard(x,y)) 
        {
            GameObject cp = sc.GetPosition(x,y);
            if(cp == null) 
            {
                MovePlateSpawn(x, y);
            }else if (cp.GetComponent<chess>().player != player)
            {
                MovePlateAtkSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x,int y)
    {
        gamecontrol sc= controller.GetComponent<gamecontrol>();

        if (sc.PositionsBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionsBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null &&
                sc.GetPosition(x + 1, y).GetComponent<chess>().player != player)
            {
                MovePlateAtkSpawn(x+1, y);
            }

            if (sc.PositionsBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
               sc.GetPosition(x - 1, y).GetComponent<chess>().player != player)
            {
                MovePlateAtkSpawn(x - 1, y);
            }
        }
    }
    public void MovePlateSpawn(int plateX,int plateY)
    {
        float x = plateX;
        float y = plateY;

        x *= 0.66f;
        y *= 0.66f;

        x += - 2.3f;
        y += - 2.3f;

        //hien thi tren unity (tren bang)
        GameObject mp = Instantiate(moveplate,new Vector3(x,y,-3.0f), Quaternion.identity);

        moveplate mpScript = mp.GetComponent<moveplate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(plateX, plateY);
    }

    public void MovePlateAtkSpawn(int plateX, int plateY)
    {
        float x = plateX;
        float y = plateY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        //hien thi tren unity (tren bang)
        GameObject mp = Instantiate(moveplate, new Vector3(x, y, -3.0f), Quaternion.identity);

        moveplate mpScript = mp.GetComponent<moveplate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(plateX, plateY);
    }

}