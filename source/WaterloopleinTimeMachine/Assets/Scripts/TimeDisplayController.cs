using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class TimeDisplayController : MonoBehaviour
{
    // HACK: We have to rebind every time the UIDocument is enabled, but there is no event for that, 
    // so we keep track of the state ourselves (OSDWasEnabled).
    // A better approach would be to not enable / disable the UIDocument, but activate / deactivate its GameObject,
    // but that gave weird issues with other UIDocuments being unloaded.

    public TimeController timeController;
    private bool OSDWasEnabled = false;
    private UIDocument timeOsdDocument;

    public void Start()
    {
        timeOsdDocument = GetComponent<UIDocument>();
        OSDWasEnabled = false;
    }

    
    public void Update()
    {
        if (timeOsdDocument.enabled && !OSDWasEnabled)
        {
            this.ActivateOSDBinding();
            OSDWasEnabled = true;
        }
        else if (!timeOsdDocument.enabled && OSDWasEnabled)
        {
            OSDWasEnabled = false;
        }
    }

    public void ActivateOSDBinding()
    {
        var timeOsdElement = timeOsdDocument.rootVisualElement; //.Q<VisualElement>("time-osd");
        timeOsdElement.dataSource = this.timeController;
    }
}
