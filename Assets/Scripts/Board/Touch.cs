using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : BoardElement, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        /*Vector3 pointerPosition = eventData.pressPosition;
        Vector3 newPosition = board.gameObject.transform.InverseTransformPoint(pointerPosition);
        Debug.Log(newPosition - new Vector3(Screen.width/2, Screen.height/2, 0));*/
    }

    private void Update()
    {
        /*if (!Model.getInstance().gameModel.IsGameOver && !Model.getInstance().gameModel.IsPause)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Up");
                board.Notify(BoardNotification.MoveCell, null, DIRECTIONS.UP);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Down");
                board.Notify(BoardNotification.MoveCell, null, DIRECTIONS.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Left");
                board.Notify(BoardNotification.MoveCell, null, DIRECTIONS.RIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Right");
                board.Notify(BoardNotification.MoveCell, null, DIRECTIONS.LEFT);
            }
        }*/
    }
}
