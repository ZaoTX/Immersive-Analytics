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

/// <summary>
/// Handles the raycast.
/// </summary>
public class Pointer : MonoBehaviour
{

    /// <summary>
    /// Default length of the raycast.
    /// </summary>
    public float m_DefaultLength = 5.0f;

    /// <summary>
    /// Little dot at the end of the raycast.
    /// </summary>
    public GameObject m_Dot;

    /// <summary>
    /// Input module which handles canvas interaction. <see cref="VRInputModule"/>
    /// </summary>
    public VRInputModule m_InputModule;

    /// <summary>
    /// The canvas, the raycast has to interact with.
    /// </summary>
    public Canvas CanvasToInteractWith;

    /// <summary>
    /// A linerenderer, which resembles the ray.
    /// </summary>
    private LineRenderer m_LineRenderer = null;

    /// <summary>
    /// Standard awake function, which assigns the linerenderer.
    /// </summary>
    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Standard update function, which handles the raycast in the scene.
    /// </summary>
    private void Update()
    {
        UpdateLine();
    }

    /// <summary>
    /// Updates the raycast.
    /// </summary>
    /// <remarks>
    /// This function updates the length of the raycast, the direction it faces and the position of the dot at the end.
    /// </remarks>
    private void UpdateLine()
    {
        // use default distance
        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default endposition
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        // or based on hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

        // set position of the dot
        m_Dot.transform.position = endPosition;

        // set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);

    }

    /// <summary>
    /// Creates the raycast.
    /// </summary>
    /// <param name="length"></param>
    /// <returns>
    /// Hit of the raycast.
    /// </returns>
    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
}