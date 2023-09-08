using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //vi tri cua plate tren ban co
    int plateX;
    int plateY;

    public bool attack = false;

    //khi an 1 quan co thi plate se doi mau
    public void Start()
    {
        if(attack)
        {   
            //doi sang mau do
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    private void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if(attack)
        {
            GameObject cp= controller.GetComponent<gamecontrol>().GetPosition(plateX, plateY);

            Destroy(cp);
        }

        controller.GetComponent<gamecontrol>().Emptyposition(reference.GetComponent<chess>().GetXboard(),
                                                             reference.GetComponent<chess>().GetYBoard());

        reference.GetComponent<chess>().SetXboard(plateX);
        reference.GetComponent<chess>().SetYboard(plateY);
        reference.GetComponent<chess>().SetCoords();

        controller.GetComponent<gamecontrol>().SetPosition(reference);

        reference.GetComponent<chess>().DestroyMovePlate();
    }

    public void SetCoords(int x,int y) //xac dinh toa do cua con tro
    {
        plateX = x; 
        plateY = y;
    }

    public void SetReference(GameObject obj) 
    {
        obj = reference; //khai bao reference la 1 gameobject
    }

    public GameObject GetReference()
    {
        return reference;
    }

}
