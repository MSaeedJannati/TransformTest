using System.Collections;
using System.Collections.Generic;
using TransformTest;
using TransformTest.General;
using UnityEngine;
using Zenject;

public class MainCanvasLogic 
{
    private MainCanvasView _view;

    public MainCanvasLogic(MainCanvasView view)
    {
        _view = view;
    }

    public void ResetScaleClicked()
    {
        EventController.TriggerEvent(EventController.OnScaleReset);
    }

    public void ResetRotationClicked()
    {
        EventController.TriggerEvent(EventController.OnRotationReset);
    }

    public class Factory : PlaceholderFactory<MainCanvasView,MainCanvasLogic>
    {
    }
}
