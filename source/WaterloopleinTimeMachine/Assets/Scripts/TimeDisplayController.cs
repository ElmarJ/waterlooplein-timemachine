using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PanelRenderer))]
public class TimeDisplayController : MonoBehaviour
{
    public TimeController timeController;
    private PanelRenderer timeOsdPanel;

    void OnEnable()
    {
        this.timeOsdPanel = GetComponent<PanelRenderer>();
        this.timeOsdPanel.RegisterUIReloadCallback(OnUIReload);
    }

    void OnDisable()
    {
        if (this.timeOsdPanel != null)
        {
            this.timeOsdPanel.UnregisterUIReloadCallback(OnUIReload);
        }
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement rootElement)
    {
        ActivateOSDBinding(rootElement);
    }

    private void ActivateOSDBinding(VisualElement timeOsdElement)
    {
        if (timeOsdElement != null)
        {
            timeOsdElement.dataSource = this.timeController;
        }
    }
}
