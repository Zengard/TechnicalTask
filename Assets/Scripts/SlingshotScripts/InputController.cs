using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : ITouchable, IMouseable
{
    private Slingshot _slingshot;

    public InputController(Slingshot slingshot)
    {
         _slingshot = slingshot;
    }

    public bool IsFingerTouchScreen { get; set; }
    public bool IsMouseClickedScreen { get; set; }

    public void FingerTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (RectTransformUtility.RectangleContainsScreenPoint(_slingshot.TouchArea, touch.position))
            {
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
                {
                    IsFingerTouchScreen = true;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (IsFingerTouchScreen == true)
                    {
                        IsFingerTouchScreen = false;
                        _slingshot.ShootController.Shoot();
                        _slingshot.ResetLines();
                    }
                }
            }
        }
    }

    public void ScreenTouched()
    {
        if (IsFingerTouchScreen == true)
        {
            Vector2 mousePosition = Input.mousePosition;
            _slingshot.CurrentPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            _slingshot.CurrentPosition = _slingshot.CenterPosition.position +
                Vector3.ClampMagnitude(_slingshot.CurrentPosition - _slingshot.CenterPosition.position, _slingshot.MaxLength);

            _slingshot.UpdateLinesPos(_slingshot.CurrentPosition);
        }
    }

    public void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_slingshot.TouchArea, Input.mousePosition))
            {
                IsMouseClickedScreen = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (IsMouseClickedScreen == true)
            {
                IsMouseClickedScreen = false;
                _slingshot.ShootController.Shoot();
                _slingshot.ResetLines();
            }
        }
    }

    public void ScreenClicked()
    {
        if (IsMouseClickedScreen == true)
        {
            Vector2 mousePosition = Input.mousePosition;
            _slingshot.CurrentPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            _slingshot.CurrentPosition = _slingshot.CenterPosition.position +
                Vector3.ClampMagnitude(_slingshot.CurrentPosition - _slingshot.CenterPosition.position, _slingshot.MaxLength);

            _slingshot.UpdateLinesPos(_slingshot.CurrentPosition);
        }
    }
}
