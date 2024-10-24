using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseable 
{
    public bool IsMouseClickedScreen { get; set; }

    public void MouseClick();

    public void ScreenClicked();
}
