using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;
    [SerializeField] Texture2D cursorCrosshair;
    [SerializeField] Texture2D cursorPointer;
    [SerializeField] Texture2D starterCursor;
    Vector2 cursorHotSpot;

    void Start()
    {
        instance = this;
        cursorHotSpot = new Vector2(starterCursor.width / 2, starterCursor.height / 2);
        Cursor.SetCursor(starterCursor, cursorHotSpot, CursorMode.Auto);
    }  

    public void setCursorCrosshair()
    {
        cursorHotSpot = new Vector2(cursorCrosshair.width / 2, cursorCrosshair.height / 2);
        Cursor.SetCursor(cursorCrosshair, cursorHotSpot, CursorMode.Auto);
    }

    public void setCursorPointer()
    {
        cursorHotSpot = new Vector2(cursorPointer.width / 2, cursorPointer.height / 2);
        Cursor.SetCursor(cursorPointer, cursorHotSpot, CursorMode.Auto);
    }
    
}