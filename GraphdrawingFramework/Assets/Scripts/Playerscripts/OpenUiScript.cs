/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using UnityEngine;
using Valve.VR;

/// <summary>
/// Handles actions towards the User Interface.
/// </summary>
public class OpenUiScript : MonoBehaviour
{

    /// <summary>
    /// Status of button bound to user interface.
    /// </summary>
    public SteamVR_Action_Boolean UiOnOff;

    /// <summary>
    /// Status of button bound to user interface forward movement.
    /// </summary>
    public SteamVR_Action_Boolean UiForward;

    /// <summary>
    /// Status of button bound to user interface backward movement.
    /// </summary>
    public SteamVR_Action_Boolean UiBackward;

    /// <summary>
    /// Player gameobject.
    /// </summary>
    /// <remarks>
    /// Included here, to calculate the vector the UI has to move along when coming closer or the opposite.
    /// </remarks>
    public GameObject Player;

    /// <summary>
    /// Multiplier for movement vector of UI (forward).
    /// </summary>
    private readonly float StepLengthForward = 1.1f;

    /// <summary>
    /// Multiplier for movement vector of UI (backwards).
    /// </summary>
    private readonly float StepLengthBackward = 0.9f;

    /// <summary>
    /// Input source.
    /// </summary>
    public SteamVR_Input_Sources handType;

    /// <summary>
    /// Canvas of user interface.
    /// </summary>
    public GameObject UserInterface;

    /// <summary>
    /// Placeholder of user interface canvas.
    /// </summary>
    /// <remarks>
    /// Indicates position of where the UI will be placed.
    /// </remarks>
    public GameObject UserInterfacePlaceholder;

    /// <summary>
    /// Invisible sphere as position placeholder.
    /// </summary>
    /// <remarks>
    /// It is a gameobject, which is attached to the hands at fixed positions. The UI will then be placed at its position.
    /// In future, the UserInterfacePlaceholder itself will probably take its place.
    /// </remarks>
    public GameObject FrameworkOptionsPlaceholder;

    /// <summary>
    /// Camera object of the hand.
    /// </summary>
    /// <remarks>
    /// Using a workaround with world cameras is the only way I was able to figure out how to interact with 
    /// a canvas with raycasts. This will be replaced with a better solution in future. To interact, the only assignable world camera
    /// of the canvas has to be set to thisHandsCamera.
    /// </remarks>
    public Camera thisHandsCamera;

    /// <summary>
    /// The actual canvas to interact with.
    /// </summary>
    /// <remarks>
    /// See the remarks for <seealso cref="thisHandsCamera"/>.
    /// </remarks>
    public Canvas canvasToShow;

    /// <summary>
    /// Boolean value that indicates the visibility of the UI.
    /// </summary>
    public bool uiCanvasVisible;


    /// <summary>
    /// Standard Start() function
    /// </summary>
    /// <remarks>
    /// On start assign the listeners for the physical VR controllers.
    /// </remarks>
    void Start()
    {
        UiOnOff.AddOnStateDownListener(TriggerDown, handType);
        UiOnOff.AddOnStateUpListener(TriggerUp, handType);
        UiForward.AddOnStateDownListener(ForwardDown, handType);
        UiForward.AddOnStateUpListener(ForwardUp, handType);
        UiBackward.AddOnStateDownListener(BackwardDown, handType);
        UiBackward.AddOnStateUpListener(BackwardUp, handType);
    }

    /// <summary>
    /// Release listener for the button, thats bound to the User Interface backwards movement.
    /// </summary>
    /// <remarks>
    /// Each time this event occurs, the UI will be rotated, facing the player.
    /// </remarks>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    private void BackwardUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        UserInterface.transform.LookAt(Player.transform);
        UserInterface.transform.rotation = Quaternion.Lerp(transform.rotation, FrameworkOptionsPlaceholder.transform.rotation, Time.deltaTime * 1);
    }

    /// <summary>
    /// Press listener for the button, thats bound to the User Interface backwards movement.
    /// </summary>
    /// <remarks>
    /// The movement is handeled, by calculating the vector between player and UI, making it shorter (<see cref="StepLengthBackward"/>
    /// and placing the UI at the end of it.
    /// </remarks>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    private void BackwardDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Vector3 currentVector = UserInterface.transform.position - Player.transform.position;
        UserInterface.transform.position = Player.transform.position + currentVector * StepLengthBackward;
    }

    /// <summary>
    /// Release listener for the button, thats bound to the User Interface forward movement.
    /// </summary>
    /// <remarks>
    /// Each time this event occurs, the UI will be rotated, facing the player.
    /// </remarks>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    private void ForwardUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        UserInterface.transform.LookAt(Player.transform);
        UserInterface.transform.rotation = Quaternion.Lerp(transform.rotation, FrameworkOptionsPlaceholder.transform.rotation, Time.deltaTime * 1);
    }

    /// <summary>
    /// Press listener for the button, thats bound to the User Interface forward movement.
    /// </summary>
    /// <remarks>
    /// The movement is handeled, by calculating the vector between player and UI, making it longer (see <see cref="StepLengthForward"/>)
    /// and placing the UI at the end of it.
    /// </remarks>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    private void ForwardDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Vector3 currentVector = UserInterface.transform.position - Player.transform.position;
        UserInterface.transform.position = Player.transform.position + currentVector * StepLengthForward;
    }

    /// <summary>
    /// Release listener for the button, thats bound to the User Interface appearing.
    /// </summary>
    /// <remarks>
    /// Here the UI is positioned at the location of the UI placeholder (<see cref="UserInterfacePlaceholder"/>), 
    /// rotated towards the user and receives the camera of the hand which summoned it (<see cref="thisHandsCamera"/>).
    /// Also, the placeholder is set visible.
    /// </remarks>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        uiCanvasVisible = !uiCanvasVisible;
        UserInterface.transform.position = UserInterfacePlaceholder.transform.position;
        UserInterface.transform.LookAt(FrameworkOptionsPlaceholder.transform);
        UserInterface.transform.rotation = Quaternion.Lerp(transform.rotation, FrameworkOptionsPlaceholder.transform.rotation, Time.deltaTime * 1);

        canvasToShow.worldCamera = thisHandsCamera;
        UserInterfacePlaceholder.SetActive(false);
        UserInterface.SetActive(uiCanvasVisible);
    }

    /// <summary>
    /// Press listener for the button, thats bound to the User Interface appearing.
    /// </summary>
    /// <remarks>
    /// Here the placeholder becomes visible or invisible, depending if the UI is visible or not.
    /// </remarks>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        UserInterfacePlaceholder.SetActive(!uiCanvasVisible);
    }
}