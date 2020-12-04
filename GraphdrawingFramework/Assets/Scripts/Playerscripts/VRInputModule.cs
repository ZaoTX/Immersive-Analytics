/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle
/// 
/// This raycasting approach is made by VR with Andrew - https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ
/// 
/// It does not suit the framework in its final version and will therefore be replaced in future.

using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

/// <summary>
/// This module handles the interaction with a canvas.
/// </summary>
public class VRInputModule : BaseInputModule
{
    /// <summary>
    /// The camera of the entering raycast.
    /// </summary>
    public Camera m_Camera;

    /// <summary>
    /// The canvas that has to be interacted with.
    /// </summary>
    public Canvas CanvasToInteractWith;

    /// <summary>
    /// The input source from the physical controller.
    /// </summary>
    public SteamVR_Input_Sources m_TargetSource;

    /// <summary>
    /// On click interaction with the canvas.
    /// </summary>
    public SteamVR_Action_Boolean m_ClickAction;

    /// <summary>
    /// The object on the canvas that has been interacted with.
    /// </summary>
    private GameObject m_CurrentObject = null;

    /// <summary>
    /// Information about the hit object.
    /// </summary>
    private PointerEventData m_Data = null;

    /// <summary>
    /// Standard awake function assigning the eventsystem.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        m_Data = new PointerEventData(eventSystem);
    }

    /// <summary>
    /// This function handles the actual interaction.
    /// </summary>
    /// <remarks>
    /// The function processes the object that has been interacted with. Further it triggers the button press (<see cref="ProcessPress(PointerEventData)"/>)
    /// and the button release (<see cref="ProcessRelease(PointerEventData)"/>).
    /// </remarks>
    public override void Process()
    {
        // Reset data, set camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);

        // Raycast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

        // Clear Raycast
        m_RaycastResultCache.Clear();

        // Handle Hoverstates
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);

        // Deal with Press event
        if (m_ClickAction.GetStateDown(m_TargetSource))
        {
            ProcessPress(m_Data);
        }

        // Deal with release event
        if (m_ClickAction.GetStateUp(m_TargetSource))
        {
            ProcessRelease(m_Data);
        }
    }

    /// <summary>
    /// Returns the interacted object data.
    /// </summary>
    /// <returns>
    /// Information about the object interacted with.
    /// </returns>
    public PointerEventData GetData()
    {
        return m_Data;
    }

    /// <summary>
    /// Processes the button down event on interaction with the canvas.
    /// </summary>
    /// <remarks>
    /// The object currently interacted with is stored to check in <see cref="ProcessRelease(PointerEventData)"/>, if
    /// it is still the same and only then actually execute the attached event.
    /// </remarks>
    /// <param name="data"></param>
    private void ProcessPress(PointerEventData data)
    {
        // Set raycast for the press
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        // Check for object hit, get the down handler, call it
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

        // If no down handler, try and get click handler
        if (newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);
        }

        // Set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;
    }

    /// <summary>
    /// Processes the button up event on interaction with the canvas.
    /// </summary>
    /// <remarks>
    /// If the object interacted with is the same as in <see cref="ProcessPress(PointerEventData)"/>,
    /// execute the related event. Reset the eventsystem data.
    /// </remarks>
    /// <param name="data"></param>
    private void ProcessRelease(PointerEventData data)
    {
        // Execute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // Check for clickhandler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // check if actual
        if (data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        // clear selected gameobject
        eventSystem.SetSelectedGameObject(null);

        // Reset data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}