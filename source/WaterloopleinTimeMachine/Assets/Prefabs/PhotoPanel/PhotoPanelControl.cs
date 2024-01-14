// using GameplayIngredients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PhotoPanelControl : MonoBehaviour
{
    public float ShowDuration = 1.0f;
    public float HideDuration = 1.0f;
    public float ShowDistance = 1.0f;

    public AnimationCurve PanelSizeXAnimation = DefaultAnimationCurve();
    public AnimationCurve PanelSizeYAnimation = DefaultAnimationCurve();
    public Gradient PanelColorAnimation;
    public Gradient TextColorAnimation;

    public PhotoPanel Panel;
    public Camera TargetCamera;

    public GameObject InfoiconObject;

    private float m_Visibility;
    private Vector2 m_DefaultSize;
    private Color m_DefaultPanelColor;
    private Color m_DefaultTitleColor;
    private Color m_DefaultBodyColor;

    // private VirtualCameraManager VirtualCameraManger;

    private Vector3 m_BaseForward;

    static private AnimationCurve DefaultAnimationCurve()
    {
        return AnimationCurve.EaseInOut(0, 0, 1, 1);
    }

    static private Gradient DefaultGradient()
    {
        var g = new Gradient();
        var alphaKeys = new GradientAlphaKey[2] { new GradientAlphaKey(0, 0), new GradientAlphaKey(1, 1) };
        var colorKeys = new GradientColorKey[2] { new GradientColorKey(Color.white, 0), new GradientColorKey(Color.white, 1) };
        g.alphaKeys = alphaKeys;
        g.colorKeys = colorKeys;
        return g;
    }

    void Start()
    {
        PanelColorAnimation = DefaultGradient();
        TextColorAnimation = DefaultGradient();
        m_DefaultSize = Panel.Size;
        m_DefaultPanelColor = Panel.PanelColor;
        m_DefaultTitleColor = Panel.TitleColor;
        m_DefaultBodyColor = Panel.BodyColor;
        m_BaseForward = InfoiconObject.transform.forward;
    }

    void Update()
    {

        // Turn into direction of camera
        if (TargetCamera != null)
        {
            Vector3 fwd = Vector3.Normalize(InfoiconObject.transform.position - TargetCamera.transform.position);

            if (Vector3.Dot(fwd, m_BaseForward) > 0)
                InfoiconObject.transform.forward = m_BaseForward;
            else
                InfoiconObject.transform.forward = -m_BaseForward;
        }

        // if (VirtualCameraManger == null)
        //     VirtualCameraManger = Manager.Get<VirtualCameraManager>();

        // if (VirtualCameraManger == null)
        // {
        //     Debug.LogWarning("Virtual Camera Manager not present.");
        //     return;
        // }

        if( /* Input.GetKey(KeyCode.Q) ||  */ Vector3.Distance(TargetCamera.gameObject.transform.position, transform.position) < ShowDistance)
            m_Visibility += Time.deltaTime / ShowDuration;
        else
            m_Visibility -= Time.deltaTime / HideDuration;

        m_Visibility = Mathf.Clamp(m_Visibility, 0, 1);

        if(m_Visibility == 0)
        {
            Panel.Root.SetActive(false);
        }
        else
        {
            Vector2 scale = new Vector2(PanelSizeXAnimation.Evaluate(m_Visibility), PanelSizeYAnimation.Evaluate(m_Visibility));
            Panel.Root.SetActive(true);
            Panel.Size = new Vector2(scale.x * m_DefaultSize.x, scale.y * m_DefaultSize.y);
            Panel.PanelColor = m_DefaultPanelColor * PanelColorAnimation.Evaluate(m_Visibility);
            Panel.TitleColor = m_DefaultTitleColor * TextColorAnimation.Evaluate(m_Visibility);
            Panel.BodyColor = m_DefaultBodyColor * TextColorAnimation.Evaluate(m_Visibility);
            Panel.Layout();

            foreach (var obj in Panel.OtherSubObjects)
            {
                obj.transform.localScale = new Vector3(scale.x, scale.y, 1);
                //if (m_Visibility == 1 && !obj.activeSelf) obj.SetActive(true);
                //if (m_Visibility < 1 && obj.activeSelf) obj.SetActive(false);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ShowDistance);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0.3f,0,0.5f);
        Gizmos.DrawSphere(transform.position, ShowDistance);
    }

    public void OnOpenInBrowserKey(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && m_Visibility == 1 && Panel.URL != null)
        {
            // Want to be 100% sure this is a safe URL: OpenURL can run random commands on some systems.
            var uri = new System.Uri(Panel.URL);
            if (uri.Host == "archief.amsterdam"){
                Application.OpenURL(uri.AbsoluteUri);
            }
        }
    }
}