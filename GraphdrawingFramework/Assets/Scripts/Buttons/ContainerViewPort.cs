/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InheritingViewPort
{

    /// <summary>
    /// The viewport, which contains most of the genrated UI-content.
    /// </summary>
    public class ContainerViewPort : MonoBehaviour
    {

        /// <summary>
        /// The actual viewport.
        /// </summary>
        public Transform inheritor;

        /// <summary>
        /// The viewports parent.
        /// </summary>
        public Transform ContentContainer;

        /// <summary>
        /// The status text displayed at the top.
        /// </summary>
        public TextMeshProUGUI StatusText;

        /// <summary>
        /// A function, which enables to show and hide new content containers in the viewport.
        /// </summary>
        /// <remarks>
        /// The panel given as input will be set active, while all others will be set inactive.
        /// This approach enables easy addition of new content containers inside the UI.
        /// </remarks>
        /// <param name="PanelToShow">The container inside of the viewport, which has to be shown.</param>
        public void ShowPanel(GameObject PanelToShow)
        {
            if (PanelToShow != null)
            {
                foreach (Transform child in inheritor)
                {
                    if (child.gameObject == PanelToShow)
                    {
                        //original
                        /*child.gameObject.SetActive(true);
                        ContentContainer.GetComponent<ScrollRect>().content = (RectTransform)child;*/
                        //Modified
                        if (child.gameObject.name == "Content_Results")
                        {

                            foreach (Transform ContentChild in child)
                            {
                                Debug.Log(ContentChild.gameObject.name);
                                Destroy(ContentChild.gameObject);
                            }
                        }

                        child.gameObject.SetActive(true);
                        ContentContainer.GetComponent<ScrollRect>().content = (RectTransform)child;
                        
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                foreach (Transform child in inheritor)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}