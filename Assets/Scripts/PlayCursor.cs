using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCursor : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform cursorImage;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        cursorImage.position = mousePos;
    }
}
