using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    Vector3 previousMousePosition;
    Vector3 mouseDelta;

    public GameObject target;

    float speed=200f;
    // Start is called before the first frame update
    void Start()
    {
        previousMousePosition=Input.mousePosition;
    }
    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
    }
    void Drag(){
        if (Input.GetMouseButton(1)){
            //gdy przycisk myszy jest przytrzymany, kostka może być obracana
            mouseDelta=Input.mousePosition-previousMousePosition;
            mouseDelta*=0.1f;//zmniejszenie prędkości obrotu
            transform.rotation=Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0)*transform.rotation;
        }
        else{
            //automatyczny ruch do docelowej pozycji o prędkości step
            if( transform.rotation!=target.transform.rotation){
            var step=speed*Time.deltaTime;
            transform.rotation=Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
        }
        }
        previousMousePosition=Input.mousePosition;
    }
    void Swipe(){
        if (Input.GetMouseButtonDown(1)){
            //dostajemy współrzędne położenia kursora myszki  przy pierwszym kliknięciu
            firstPressPos=new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            print(firstPressPos);
        }
        if(Input.GetMouseButtonUp(1)){
            //dostajemy współrzędne położenia kursora myszki  przy drugim kliknięciu
            secondPressPos=new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //stworzenie wektoru z obu klikniec
            currentSwipe=new Vector2(secondPressPos.x-firstPressPos.x, secondPressPos.y - firstPressPos.y);
            //normalize the 2d vector
            currentSwipe.Normalize();
            if(LeftSwipe(currentSwipe)){
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if(RightSwipe(currentSwipe)){
                target.transform.Rotate(0, -90, 0, Space.World);
            }else if(UpLeftSwipe(currentSwipe)){
                target.transform.Rotate(90,0,0, Space.World);
            }else if(UpRightSwipe(currentSwipe)){
                target.transform.Rotate(0,0,-90, Space.World);
            }else if(DownLeftSwipe(currentSwipe)){
                target.transform.Rotate(0,0,90, Space.World);
            }else if(DownRightSwipe(currentSwipe)){
                target.transform.Rotate(-90,0,0, Space.World);
            }
        }
    }
    //funkcje które sprawdzają w którą stronę ma się obrócić kostka
    bool LeftSwipe(Vector2 swipe){
        return swipe.x<0 && swipe.y>-0.5f && swipe.y<0.5f;
    }
    bool RightSwipe(Vector2 swipe){
        return swipe.x>0 && currentSwipe.y>-0.5f && swipe.y<0.5f;
    }
    bool UpLeftSwipe(Vector2 swipe){
        return swipe.y>0 && swipe.x<0f;
    }
    bool UpRightSwipe(Vector2 swipe){
        return swipe.y>0 && swipe.x>0f;
    }
    bool DownLeftSwipe(Vector2 swipe){
        return swipe.y<0 && swipe.x < 0f;
    }
    bool DownRightSwipe(Vector2 swipe){
        return swipe.y<0 && swipe.x>0f;
    }
}
