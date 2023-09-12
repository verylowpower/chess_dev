using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;


/*coi ban co nhu 1 mang 2 chieu co kich thuoc 8x8,
sau do dem quan co nhu cac phan tu cua mang dem dat 
len mang 2 chieu do. Co the coi ban co la 1 ma tran 8x8
va cac quan co la phan tu cua no*/


public class gamecontrol : MonoBehaviour
{   
    public GameObject chesspiece;

    //vi tri va doi cho tung quan co
    private GameObject[,] positions = new GameObject[8, 8]; //tao mang 2 chieu 
    private GameObject[] playerBlack = new GameObject[16]; //doi trang co 16 quan
    private GameObject[] playerWhite = new GameObject[16]; //den co 16 quan

    private string currentPlayer = "white";

    private bool gameOver=false; //xac dinh end game

    public void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("white_rook",0,0),Create("white_knight",1,0),Create("white_bishop",2,0),Create("white_queen",3,0),
            Create("white_king",4,0),Create("white_bishop",5,0),Create("white_knight",6,0),Create("white_rook",7,0),
            Create("white_pawn",0,1),Create("white_pawn",1,1),Create("white_pawn",2,1),Create("white_pawn",3,1),
            Create("white_pawn",4,1),Create("white_pawn",5,1),Create("white_pawn",6,1),Create("white_pawn",7,1)
        };
        playerBlack = new GameObject[]
        {
            Create("black_rook",0,7),Create("black_knight",1,7),Create("black_bishop",2,7),Create("black_queen",3,7),
            Create("black_king",4,7),Create("black_bishop",5,7),Create("black_knight",6,7),Create("black_rook",7,7),
            Create("black_pawn",0,6),Create("black_pawn",1,6),Create("black_pawn",2,6),Create("black_pawn",3,6),
            Create("black_pawn",4,6),Create("black_pawn",5,6),Create("black_pawn",6,6),Create("black_pawn",7,6)
        };


        for(int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }

        
    } 
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece,new Vector3(0,0,-1),Quaternion.identity); //tao chesspiece tai vi tri (0,0,-1)
        //chess: truy cap vao script 
        chess cm = obj.GetComponent<chess>(); //truy cap vao compoment cua script chess
        cm.name=name;
        //set vi tri cho ban co
        cm.SetXboard(x); 
        cm.SetYboard(y);
        //active sprite
        cm.Activate();

        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        chess cm= obj.GetComponent<chess>();

        //đè quân lên khoảng trống hay bất cứ cái đ gì ở trong đấy
        positions[cm.GetXboard(), cm.GetXboard()] = obj;

    }
    
    //vi tri ban dau
   public void Emptyposition(int x,int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x,int y)
    {
       return positions[x,y];
    }
    

    public bool PositionsBoard(int x, int y)
    {

        if(x<0|| y < 0||x>=positions.GetLength(0)||y>=positions.GetLength(1)) return false;
        return true;
    }




}
