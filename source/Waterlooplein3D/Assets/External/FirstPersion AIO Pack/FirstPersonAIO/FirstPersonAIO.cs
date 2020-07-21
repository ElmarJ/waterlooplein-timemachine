/// Original Code written and designed by Aeden C Graves.
///
///
/// CHANGE LOG:
///
/// DATE || msg: "" || Author Signature: SNG || version VERSION
///
/// 10/17/19 || msg: "Fixed inconsistant jumping/ground detection. Fixed headbobing axis amplification. Added toggle crouching." || Author Signature: Aedan Graves || version: 19.9.20f >> 19.10.17f 
/// 09/20/19 || msg: "Added support Email to the bottom of the inspector. Fixed issues with sticking to the walls. Removed the need for external assigning of a min and max friction material" || Author Signature: Aedan Graves || version: 19.9.13 >> 19.9.20f
/// 09/13/19 || msg: "New Editor script, Fixed Stamina, Fixed Crouching, Put 'FOV Kick' Under reconstruction, made dynamic foot steps easier to understand." || Author Signature: Aedan Graves || version: 19.7.28cu >> 19.9.13cu
/// 07/28/19 || msg: "Added function to effect mouse sensitivity bassed on the cameras FOV." || Author Signature: Aedan Graves || version: 19.6.7cu >> 19.7.28cu
/// 06/07/19 || msg: "Added ability to toggle the ability to jump from the editor." || Author Signature: Adam Worrell || 19.5.12feu >> version 19.6.7cu
/// 05/12/19 || msg: "Fixed non dymanic footsteping. Remade crouching system to be more efficiant and added an input over ride. || Author Signature: Aedan Graves || version 19.3.22 cl >> 19.5.12feu
/// 03/22/19 || msg: "Cleaned up code" || Author Signature: Aedan Graves || version 19.3.19cu >> 19.3.22cl
/// 03/19/19 || msg: "Added a rudimentary slope detection system." || Author Signature: Aedan Graves || version 19.3.18a >> 19.3.19cu
/// 03/18/19 || msg: "Fixed Stamina" || Author Signature: Aedan Graves || version 19.3.11p >> 19.3.18a
/// 03/02/19 || msg: "Improved camera System" || Author Signature: Aecan Graves || version 19.3.2 >> 19.3.11p
/// 03/02/19 || msg: "Lowered maximum walk, sprint, and jump values" || Author Signature: Aedan Graves || version: 19.2.21 >> 19.3.2
/// 02/21/19 || msg: "Removed dynamic speed curve. Modified headbob logic || Author Signature: Aedan Graves || version: 19.2.15 >> 19.2.21
/// 02/15/19 || msg: "Added Camera shake. Made it possable to disable camera movement when jumping and landing." || Author Signature: Aedan Graves || version: 19.2.12 >> 19.2.15
/// 02/12/19 || msg: "Seperated Dynamic Footsteps from the Headbob calculations." || Author Signature: Aedan Graves || version: 1.6b >> 19.2.12
/// 02/08/19 || msg: "Added some more tooltips." || Author Signature: Aedan C Graves || version 1.6a >> 1.6b
/// 02/04/19 || msg: "Changed crouch funtion to use an In Editor defined input axis" || Author Signature: Aedan Graves || version 1.6 >> 1.6a
/// 12/13/18 || msg: "Added 'Custom' entry for Dynamic footstep system" || Author Signature: Aedan Graves || version 1.5b >> 1.6
/// 12/11/18 || msg: "Added Volume control to Footstep and Jump/land SFX." || Author Signature: Aedan Graves || version 1.5a >> 1.5b
/// 02/18/18 || msg: "Updated mouse rotation to allow pre-play rotiation." || Author Signature: Aedan Graves || version 1.5 >> 1.5a
/// 01/31/18 || msg: "Changed Dynamic footstep system to use physics materials." || Author Signature: Aedan Graves || version 1.4c >> 1.5
/// 12/19/17 || msg: "Added headbob passthrough variables" || Auther Signature: Aeden Graves || version 1.4b >> 1.4c
/// 12/02/17 || msg: "Made camera movement toggleable" || Auther Signature: Aeden Graves || version 1.4a >> 1.4b
/// 10/16/17 || msg: "Made all sounds optional." || Author Signature: Aedan Graves || version 1.4 >> 1.4a
/// 10/09/17 || msg: "Added Optional FOV Kick" || Author Signature: Aedan Graves || version 1.3b >> 1.4
/// 10/08/17 || msg: "Improved Dynamic Footsteps." || Author Signature: Aedan Graves || version 1.3a >> 1.3b
/// 10/07/17 || msg: "BetaTesting Class" || Author Signature: Aedan Graves || version 1.3 >> 1.3a
/// 10/07/17 || msg: "Added Optional Dynamic Footsteps. Added optional Dynamic Speed Curve." || Author Signature: Aedan C Graves || version 1.2 >> 1.3
/// 10/03/17 || msg: "Added optional Crouch." || Author Signature: Aedan Graves || version v1.1 >> v1.2
/// 09/26/17 || msg: "Fixed Headbobbing in mid air. Added a option for head bobbing, Added optional Stamina. Added Auto Crosshair Feature." || Author Signature: Aedan Graves|| version v1.0 >> v1.1
/// 09/21/17 || msg: "Finished SMB FPS Logic." || Author Signature: Aedan Graves || version v0.0 >> v1.0
///
/// 
/// 
/// Made changes that you think should come "Out of the box"? E-mail the modified Script with A new entry on the top of the Change log to: modifiedassets@aedangraves.info

using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
    using UnityEditor;
#endif

[RequireComponent(typeof(CapsuleCollider)),RequireComponent(typeof(Rigidbody)),AddComponentMenu("First Person AIO")]

public class FirstPersonAIO : MonoBehaviour {

    public string versionNum = "19.10.17f";

    #region Variables

    #region Input Settings

    #endregion

    #region Look Settings
    public bool enableCameraMovement;
    public float verticalRotationRange = 170;
    public float mouseSensitivity = 10;
    public float mouseSensitivityInternal;
    public  float fOVToMouseSensitivity = 1;
    public float cameraSmoothing = 5f;
    public bool lockAndHideCursor = false;
    public Camera playerCamera;
    public bool enableCameraShake=false;
    internal Vector3 cameraStartingPosition;
    float baseCamFOV;
    

    public bool autoCrosshair = false;
    public bool drawStaminaMeter = true;
    float smoothRef;
    Image StaminaMeter;
    Image StaminaMeterBG;
    public Sprite Crosshair;
    public Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Vector3 originalRotation;
    #endregion

    #region Movement Settings

    public bool playerCanMove = true;
    public bool walkByDefault = true;
    public float walkSpeed = 4f;
    public float sprintSpeed = 8f;
    public float jumpPower = 5f;
    public bool canJump = true;
    public bool canHoldJump;
    public bool useStamina = true;
    public float staminaDepletionSpeed = 5f;
    public float staminaLevel = 50;
    public float speed;
    public float staminaInternal;
    internal float walkSpeedInternal;
    internal float sprintSpeedInternal;
    internal float jumpPowerInternal;

    [System.Serializable]
    public class CrouchModifiers {
        public bool useCrouch = true;
        public bool toggleCrouch = false;
        public KeyCode crouchKey = KeyCode.LeftControl;
        public float crouchWalkSpeedMultiplier = 0.5f;
        public float crouchJumpPowerMultiplier = 0f;
        public bool crouchOverride;
        internal float colliderHeight;
        
    }
    public CrouchModifiers _crouchModifiers = new CrouchModifiers();
    [System.Serializable]
    public class FOV_Kick
    {
        public bool useFOVKick = false;
        public float FOVKickAmount = 4;
        public float changeTime = 0.1f;
        public AnimationCurve KickCurve = new AnimationCurve();
        public float fovStart;
    }
    public FOV_Kick fOVKick = new FOV_Kick();
    public class AdvancedSettings {
        public float gravityMultiplier = 1.0f;
        public PhysicMaterial zeroFrictionMaterial;
        public PhysicMaterial highFrictionMaterial;
        public float maxSlopeAngle=70;
        public bool tooSteep;
        public RaycastHit surfaceAngleCheck;
    }
    public AdvancedSettings advanced = new AdvancedSettings();
    private CapsuleCollider capsule;
    private const float jumpRayLength = 0.7f;
    public bool IsGrounded { get; private set; }
    Vector2 inputXY;
    public bool isCrouching;
    bool isSprinting = false;

    public Rigidbody fps_Rigidbody;

    #endregion

    #region Headbobbing Settings
    public bool useHeadbob = true;
    public Transform head = null;
    public float headbobFrequency = 1.5f;
    public float headbobSwayAngle = 5f;
    public float headbobHeight = 3f;
    public float headbobSideMovement =5f;  
    public bool useJumdLandMovement = true;
    public float jumpAngle =3f;
    public float landAngle = 60;
    private Vector3 originalLocalPosition;
    private float nextStepTime = 0.5f;
    private float headbobCycle = 0.0f;
    private float headbobFade = 0.0f;
    private float springPosition = 0.0f;
    private float springVelocity = 0.0f;
    private float springElastic = 1.1f;
    private float springDampen = 0.8f;
    private float springVelocityThreshold = 0.05f;
    private float springPositionThreshold = 0.05f;
    Vector3 previousPosition;
    Vector3 previousVelocity = Vector3.zero;
    Vector3 miscRefVel;
    bool previousGrounded;
    AudioSource audioSource;

    #endregion

    #region Audio Settings

    public float Volume = 5f;
    public AudioClip jumpSound = null;
    public AudioClip landSound = null;
    public bool _useFootStepSounds = false;
    public List<AudioClip> footStepSounds = null;
    public enum FSMode{Static, Dynamic}
    public FSMode fsmode;
 
    [System.Serializable]
    public class DynamicFootStep{
        public PhysicMaterial woodPhysMat;
        public PhysicMaterial metalAndGlassPhysMat;
        public PhysicMaterial grassPhysMat;
        public PhysicMaterial dirtAndGravelPhysMat;
        public PhysicMaterial rockAndConcretePhysMat;
        public PhysicMaterial mudPhysMat;
        public PhysicMaterial customPhysMat;
        public List<AudioClip> currentClipSet;

        public List<AudioClip> woodClipSet;
        public List<AudioClip> metalAndGlassClipSet;
        public List<AudioClip> grassClipSet;
        public List<AudioClip> dirtAndGravelClipSet;
        public List<AudioClip> rockAndConcreteClipSet;
        public List<AudioClip> mudClipSet;
        public List<AudioClip> customClipSet;
    }
    public DynamicFootStep dynamicFootstep = new DynamicFootStep();

    #endregion

    #region BETA Settings
    /*
     [System.Serializable]
public class BETA_SETTINGS{

}

            [Space(15)]
    [Tooltip("Settings in this feild are currently in beta testing and can prove to be unstable.")]
    [Space(5)]
    public BETA_SETTINGS betaSettings = new BETA_SETTINGS();
     */
    
    #endregion

    #endregion

    private void Awake()
    {
        #region Look Settings - Awake
        originalRotation = transform.localRotation.eulerAngles;

        #endregion 

        #region Movement Settings - Awake
        walkSpeedInternal = walkSpeed;
        sprintSpeedInternal = sprintSpeed;
        jumpPowerInternal = jumpPower;
        capsule = GetComponent<CapsuleCollider>();
        IsGrounded = true;
        isCrouching = false;
        fps_Rigidbody = GetComponent<Rigidbody>();
        _crouchModifiers.colliderHeight = capsule.height;
        #endregion

        #region Headbobbing Settings - Awake

        #endregion

        #region BETA_SETTINGS - Awake
    
#endregion

    }

    private void Start()
    {
        #region Look Settings - Start

        if(autoCrosshair || drawStaminaMeter){
            Canvas canvas = new GameObject("AutoCrosshair").AddComponent<Canvas>();
            canvas.gameObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.pixelPerfect = true;
            canvas.transform.SetParent(playerCamera.transform);
            canvas.transform.position = Vector3.zero;

            if(autoCrosshair){
                Image crossHair = new GameObject("Crosshair").AddComponent<Image>();
                crossHair.sprite = Crosshair;
                crossHair.rectTransform.sizeDelta = new Vector2(25,25);
                crossHair.transform.SetParent(canvas.transform);
                crossHair.transform.position = Vector3.zero;
            }

            if(drawStaminaMeter){
                StaminaMeterBG = new GameObject("StaminaMeter").AddComponent<Image>();
                StaminaMeter = new GameObject("Meter").AddComponent<Image>();
                StaminaMeter.transform.SetParent(StaminaMeterBG.transform);
                StaminaMeterBG.transform.SetParent(canvas.transform);
                StaminaMeterBG.transform.position = Vector3.zero;
                StaminaMeterBG.rectTransform.anchorMax = new Vector2(0.5f,0);
                StaminaMeterBG.rectTransform.anchorMin = new Vector2(0.5f,0);
                StaminaMeterBG.rectTransform.anchoredPosition = new Vector2(0,15);
                StaminaMeterBG.rectTransform.sizeDelta = new Vector2(250,6);
                StaminaMeterBG.color = new Color(0,0,0,0);
                StaminaMeter.rectTransform.sizeDelta = new Vector2(250,6);
                StaminaMeter.color = new Color(0,0,0,0);
            }
        }
        mouseSensitivityInternal = mouseSensitivity;
        cameraStartingPosition = playerCamera.transform.localPosition;
        if(lockAndHideCursor) { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
        baseCamFOV = playerCamera.fieldOfView;
        #endregion

        #region Movement Settings - Start  
        staminaInternal = staminaLevel;
        advanced.zeroFrictionMaterial = new PhysicMaterial("Zero_Friction");
        advanced.zeroFrictionMaterial.dynamicFriction =0;
        advanced.zeroFrictionMaterial.staticFriction =0;
        advanced.zeroFrictionMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        advanced.zeroFrictionMaterial.bounceCombine = PhysicMaterialCombine.Minimum;
        advanced.highFrictionMaterial = new PhysicMaterial("Max_Friction");
        advanced.highFrictionMaterial.dynamicFriction =1;
        advanced.highFrictionMaterial.staticFriction =1;
        advanced.highFrictionMaterial.frictionCombine = PhysicMaterialCombine.Maximum;
        advanced.highFrictionMaterial.bounceCombine = PhysicMaterialCombine.Average;
        #endregion

        #region Headbobbing Settings - Start
        originalLocalPosition = head.localPosition;
        if(GetComponent<AudioSource>() == null) { gameObject.AddComponent<AudioSource>(); }
        previousPosition = fps_Rigidbody.position;
        audioSource = GetComponent<AudioSource>();
        #endregion

        #region BETA_SETTINGS - Start
        fOVKick.fovStart = playerCamera.fieldOfView;
        #endregion
    }

    private void Update()
    {
        #region Look Settings - Update

            if(enableCameraMovement){
            float mouseXInput;
            float mouseYInput;
            float camFOV = playerCamera.fieldOfView;
            mouseXInput = Input.GetAxis("Mouse Y");
            mouseYInput = Input.GetAxis("Mouse X");
            if(targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; } else if(targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
            if(targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x -= 360; } else if(targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }
            targetAngles.y += mouseYInput * (mouseSensitivityInternal - ((baseCamFOV-camFOV)*fOVToMouseSensitivity)/6f);
            targetAngles.x += mouseXInput * (mouseSensitivityInternal - ((baseCamFOV-camFOV)*fOVToMouseSensitivity)/6f);
            targetAngles.y = Mathf.Clamp(targetAngles.y, -0.5f * Mathf.Infinity, 0.5f * Mathf.Infinity);
            targetAngles.x = Mathf.Clamp(targetAngles.x, -0.5f * verticalRotationRange, 0.5f * verticalRotationRange);
            followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, (cameraSmoothing)/100);
            playerCamera.transform.localRotation = Quaternion.Euler(-followAngles.x + originalRotation.x,0,0);
            transform.localRotation =  Quaternion.Euler(0, followAngles.y+originalRotation.y, 0);
        }
    
        #endregion

        #region Movement Settings - Update

        #endregion

        #region Headbobbing Settings - Update

        #endregion

        #region BETA_SETTINGS - Update

        #endregion
    }

    private void FixedUpdate()
    {
        #region Look Settings - FixedUpdate

        #endregion

        #region Movement Settings - FixedUpdate
        
        bool wasWalking = !isSprinting;
        if(useStamina){
            isSprinting = Input.GetKey(KeyCode.LeftShift) && !isCrouching && staminaInternal > 0 && (Mathf.Abs(fps_Rigidbody.velocity.x) > 0.01f || Mathf.Abs(fps_Rigidbody.velocity.x) > 0.01f);
            if(isSprinting){
                staminaInternal -= (staminaDepletionSpeed*2)*Time.deltaTime;
                if(drawStaminaMeter){
                    StaminaMeterBG.color = Vector4.MoveTowards(StaminaMeterBG.color, new Vector4(0,0,0,0.5f),0.15f);
                    StaminaMeter.color = Vector4.MoveTowards(StaminaMeter.color, new Vector4(1,1,1,1),0.15f);
                }
            }else if((!Input.GetKey(KeyCode.LeftShift)||Mathf.Abs(fps_Rigidbody.velocity.x)< 0.01f || Mathf.Abs(fps_Rigidbody.velocity.x)< 0.01f || isCrouching)&&staminaInternal<staminaLevel){
                staminaInternal += staminaDepletionSpeed*Time.deltaTime;
            }
                if(drawStaminaMeter&&staminaInternal==staminaLevel){
                    StaminaMeterBG.color = Vector4.MoveTowards(StaminaMeterBG.color, new Vector4(0,0,0,0),0.15f);
                    StaminaMeter.color = Vector4.MoveTowards(StaminaMeter.color, new Vector4(1,1,1,0),0.15f);
                }
                staminaInternal = Mathf.Clamp(staminaInternal,0,staminaLevel);
                float x = Mathf.Clamp(Mathf.SmoothDamp(StaminaMeter.transform.localScale.x,(staminaInternal/staminaLevel)*StaminaMeterBG.transform.localScale.x,ref smoothRef,(1)*Time.deltaTime,1),0.001f, StaminaMeterBG.transform.localScale.x);
                StaminaMeter.transform.localScale = new Vector3(x,1,1); 
        } else{isSprinting = Input.GetKey(KeyCode.LeftShift);}

        advanced.tooSteep = false;
        float inrSprintSpeed;
        inrSprintSpeed = sprintSpeedInternal;
        Vector3 dMove = Vector3.zero;
        speed = walkByDefault ? isCrouching ? walkSpeedInternal : (isSprinting ? inrSprintSpeed : walkSpeedInternal) : (isSprinting ? walkSpeedInternal : inrSprintSpeed);
        Ray ray = new Ray(transform.position - new Vector3(0,(capsule.height/2)-0.01f,0) , -transform.up);
        Debug.DrawLine(ray.origin,ray.origin - new Vector3(0,0.05f,0),Color.black);
        if(IsGrounded || fps_Rigidbody.velocity.y < 0.1) {
            RaycastHit[] hits = Physics.RaycastAll(ray, 0.05f);
            float nearest = float.PositiveInfinity;
            IsGrounded = false;
            for(int i = 0; i < hits.Length; i++) {
                if(!hits[i].collider.isTrigger && hits[i].distance < nearest) {
                    IsGrounded = true;
                    nearest = hits[i].distance;
                }
            }
        }
  


       
    if(advanced.maxSlopeAngle>0){
        if(Physics.Raycast(new Vector3(transform.position.x,transform.position.y-0.75f,transform.position.z+0.1f), Vector3.down,out advanced.surfaceAngleCheck,1f)){
        
            if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)<89){
                        advanced.tooSteep = false;                       
                        dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * speed;           
              if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)>advanced.maxSlopeAngle){
                        advanced.tooSteep = true;
                         isSprinting=false;
                        dMove = new Vector3(0,-4,0);
                        
            }else if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)>44){
                        advanced.tooSteep = true;
                        isSprinting=false;
                        dMove = (transform.forward * inputXY.y * speed + transform.right * inputXY.x) + new Vector3(0,-4,0);
                }
            }    
    }
    
      else  if(Physics.Raycast( new Vector3(transform.position.x-0.086f,transform.position.y-0.75f,transform.position.z-0.05f), Vector3.down,out advanced.surfaceAngleCheck,1f)){
       
            if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)<89){
                        advanced.tooSteep = false;             
                        dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;           
              if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)>70){
                        advanced.tooSteep = true;
                         isSprinting=false;
                        dMove = new Vector3(0,-4,0);
                        
            }else if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)>45){
                        advanced.tooSteep = true;
                        isSprinting=false;
                        dMove = (transform.forward * inputXY.y * speed + transform.right * inputXY.x) + new Vector3(0,-4,0);
                       
                }
            }    
            else  if(Physics.Raycast( new Vector3(transform.position.x+0.086f,transform.position.y-0.75f,transform.position.z-0.05f), Vector3.down,out advanced.surfaceAngleCheck,1f)){
        
            if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)<89){
                        advanced.tooSteep = false;                   
                        dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;
              if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)>70){
                        advanced.tooSteep = true;
                         isSprinting=false;
                        dMove = new Vector3(0,-4,0);
                        
            }else if(Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up)>45){
                        advanced.tooSteep = true;
                        isSprinting=false;
                        dMove = (transform.forward * inputXY.y * speed + transform.right * inputXY.x) + new Vector3(0,-4,0);
                    }
                }
            }
        }else{advanced.tooSteep = false;
                        dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;
            }    
    }
         else{advanced.tooSteep = false;
                        dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;
            }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        inputXY = new Vector2(horizontalInput, verticalInput);
        if(inputXY.magnitude > 1) { inputXY.Normalize(); }
       
        float yv = fps_Rigidbody.velocity.y;
        bool didJump = canHoldJump?Input.GetButton("Jump"): Input.GetButtonDown("Jump");

        if (!canJump) didJump = false;

        if(IsGrounded && didJump && jumpPowerInternal > 0)
        {
            yv += jumpPowerInternal;
            IsGrounded = false;
            didJump=false;
        }

        if(playerCanMove)
        {
            fps_Rigidbody.velocity = dMove + Vector3.up * yv;
        } else{fps_Rigidbody.velocity = Vector3.zero;}

        if(dMove.magnitude > 0 || !IsGrounded || advanced.tooSteep) {
            capsule.sharedMaterial = advanced.zeroFrictionMaterial;
        } else { capsule.sharedMaterial = advanced.highFrictionMaterial; }

        fps_Rigidbody.AddForce(Physics.gravity * (advanced.gravityMultiplier - 1));
        /* if(fOVKick.useFOVKick && wasWalking == isSprinting && fps_Rigidbody.velocity.magnitude > 0.1f && !isCrouching){
            StopAllCoroutines();
            StartCoroutine(wasWalking ? FOVKickOut() : FOVKickIn());
        } */

        if(_crouchModifiers.useCrouch) {
            if(!_crouchModifiers.toggleCrouch){ isCrouching = _crouchModifiers.crouchOverride || Input.GetKey(_crouchModifiers.crouchKey);}
            else{if(Input.GetKeyDown(_crouchModifiers.crouchKey)){isCrouching = !isCrouching || _crouchModifiers.crouchOverride;}}

            if(isCrouching) {
                    capsule.height = Mathf.MoveTowards(capsule.height, _crouchModifiers.colliderHeight/2, 5*Time.deltaTime);
                        walkSpeedInternal = walkSpeed*_crouchModifiers.crouchWalkSpeedMultiplier;
                        jumpPowerInternal = jumpPower* _crouchModifiers.crouchJumpPowerMultiplier;
                } else {
                capsule.height = Mathf.MoveTowards(capsule.height, _crouchModifiers.colliderHeight, 5*Time.deltaTime);    
                walkSpeedInternal = walkSpeed;
                jumpPowerInternal = jumpPower;
            }
        }

        #endregion

        #region BETA_SETTINGS - FixedUpdate

        #endregion

        #region Headbobbing Settings - FixedUpdate
        float yPos = 0;
        float xPos = 0;
        float zTilt = 0;
        float xTilt = 0;
        float bobSwayFactor = 0;
        float bobFactor = 0;
        float strideLangthen = 0;
        float flatVel = 0;

        if(useHeadbob == true || fsmode == FSMode.Dynamic || _useFootStepSounds == true){
            Vector3 vel = (fps_Rigidbody.position - previousPosition) / Time.deltaTime;
            Vector3 velChange = vel - previousVelocity;
            previousPosition = fps_Rigidbody.position;
            previousVelocity = vel;
            springVelocity -= velChange.y;
            springVelocity -= springPosition * springElastic;
            springVelocity *= springDampen;
            springPosition += springVelocity * Time.deltaTime;
            springPosition = Mathf.Clamp(springPosition, -0.3f, 0.3f);

            if(Mathf.Abs(springVelocity) < springVelocityThreshold && Mathf.Abs(springPosition) < springPositionThreshold) { springPosition = 0; springVelocity = 0; }
            flatVel = new Vector3(vel.x, 0.0f, vel.z).magnitude;
            strideLangthen = 1 + (flatVel * ((headbobFrequency*2)/10));
            headbobCycle += (flatVel / strideLangthen) * (Time.deltaTime / headbobFrequency);
            bobFactor = Mathf.Sin(headbobCycle * Mathf.PI * 2);
            bobSwayFactor = Mathf.Sin(Mathf.PI * (2 * headbobCycle + 0.5f));
            bobFactor = 1 - (bobFactor * 0.5f + 1);
            bobFactor *= bobFactor;

            yPos = 0;
            xPos = 0;
            zTilt = 0;
            if(useJumdLandMovement){xTilt = -springPosition * landAngle;}
            else{xTilt = -springPosition;}

            if(IsGrounded)
            {
                if(new Vector3(vel.x, 0.0f, vel.z).magnitude < 0.1f) { headbobFade = Mathf.MoveTowards(headbobFade, 0.0f,0.5f); } else { headbobFade = Mathf.MoveTowards(headbobFade, 1.0f, Time.deltaTime); }
                float speedHeightFactor = 1 + (flatVel * 0.3f);
                xPos = -(headbobSideMovement/10) * headbobFade *bobSwayFactor;
                yPos = springPosition * (jumpAngle/10) + bobFactor * (headbobHeight/10) * headbobFade * speedHeightFactor;
                zTilt = bobSwayFactor * (headbobSwayAngle/10) * headbobFade;
            }
        }

            if(useHeadbob == true){
                if(fps_Rigidbody.velocity.magnitude >0.1f){
                    head.localPosition = Vector3.MoveTowards(head.localPosition, originalLocalPosition + new Vector3(xPos, yPos, 0),0.5f);
                }else{
                    head.localPosition = Vector3.SmoothDamp(head.localPosition, originalLocalPosition,ref miscRefVel, 0.15f);
                }
                head.localRotation = Quaternion.Euler(xTilt, 0, zTilt);
                
           
        }

            if(fsmode == FSMode.Dynamic)
            {
                Vector3 dwn = Vector3.down;
                RaycastHit hit = new RaycastHit();
                if(Physics.Raycast(transform.position, dwn, out hit))
                {
                    dynamicFootstep.currentClipSet = (dynamicFootstep.woodPhysMat && hit.collider.sharedMaterial == dynamicFootstep.woodPhysMat && dynamicFootstep.woodClipSet.Any()) ? // If standing on Wood
                    dynamicFootstep.woodClipSet : ((dynamicFootstep.grassPhysMat && hit.collider.sharedMaterial == dynamicFootstep.grassPhysMat && dynamicFootstep.grassClipSet.Any()) ? // If standing on Grass
                    dynamicFootstep.grassClipSet : ((dynamicFootstep.metalAndGlassPhysMat && hit.collider.sharedMaterial == dynamicFootstep.metalAndGlassPhysMat && dynamicFootstep.metalAndGlassClipSet.Any()) ? // If standing on Metal/Glass
                    dynamicFootstep.metalAndGlassClipSet : ((dynamicFootstep.rockAndConcretePhysMat && hit.collider.sharedMaterial == dynamicFootstep.rockAndConcretePhysMat && dynamicFootstep.rockAndConcreteClipSet.Any()) ? // If standing on Rock/Concrete
                    dynamicFootstep.rockAndConcreteClipSet : ((dynamicFootstep.dirtAndGravelPhysMat && hit.collider.sharedMaterial == dynamicFootstep.dirtAndGravelPhysMat && dynamicFootstep.dirtAndGravelClipSet.Any()) ? // If standing on Dirt/Gravle
                    dynamicFootstep.dirtAndGravelClipSet : ((dynamicFootstep.mudPhysMat && hit.collider.sharedMaterial == dynamicFootstep.mudPhysMat && dynamicFootstep.mudClipSet.Any())? // If standing on Mud
                    dynamicFootstep.mudClipSet : ((dynamicFootstep.customPhysMat && hit.collider.sharedMaterial == dynamicFootstep.customPhysMat && dynamicFootstep.customClipSet.Any())? // If standing on the custom material 
                    dynamicFootstep.customClipSet : footStepSounds)))))); // If material is unknown, fall back

                    if(IsGrounded)
                    {
                        if(!previousGrounded)
                        {
                            if(_useFootStepSounds && dynamicFootstep.currentClipSet.Any()) { audioSource.PlayOneShot(dynamicFootstep.currentClipSet[Random.Range(0, dynamicFootstep.currentClipSet.Count)],Volume/10); }
                            nextStepTime = headbobCycle + 0.5f;
                        } else
                        {
                            if(headbobCycle > nextStepTime)
                            {
                                nextStepTime = headbobCycle + 0.5f;
                                if(_useFootStepSounds && dynamicFootstep.currentClipSet.Any()){ audioSource.PlayOneShot(dynamicFootstep.currentClipSet[Random.Range(0, dynamicFootstep.currentClipSet.Count)],Volume/10); }
                            }
                        }
                        previousGrounded = true;
                    } else
                    {
                        if(previousGrounded)
                        {
                            if(_useFootStepSounds && dynamicFootstep.currentClipSet.Any()){ audioSource.PlayOneShot(dynamicFootstep.currentClipSet[Random.Range(0, dynamicFootstep.currentClipSet.Count)],Volume/10); }
                        }
                        previousGrounded = false;
                    }

                } else {
                    dynamicFootstep.currentClipSet = footStepSounds;
                    if(IsGrounded)
                    {
                        if(!previousGrounded)
                        {
                            if(_useFootStepSounds && landSound){ audioSource.PlayOneShot(landSound,Volume/10); }
                            nextStepTime = headbobCycle + 0.5f;
                        } else
                        {
                            if(headbobCycle > nextStepTime)
                            {
                                nextStepTime = headbobCycle + 0.5f;
                                int n = Random.Range(0, footStepSounds.Count);
                                if(_useFootStepSounds && footStepSounds.Any()){ audioSource.PlayOneShot(footStepSounds[n],Volume/10); }
                                footStepSounds[n] = footStepSounds[0];
                            }
                        }
                        previousGrounded = true;
                    } else
                    {
                        if(previousGrounded)
                        {
                            if(_useFootStepSounds && jumpSound){ audioSource.PlayOneShot(jumpSound,Volume/10); }
                        }
                        previousGrounded = false;
                    }
                }
                
            } else
            {
                if(IsGrounded)
                {
                    if(!previousGrounded)
                    {
                        if(_useFootStepSounds && landSound) { audioSource.PlayOneShot(landSound,Volume/10); }
                        nextStepTime = headbobCycle + 0.5f;
                    } else
                    {
                        if(headbobCycle > nextStepTime)
                        {
                            nextStepTime = headbobCycle + 0.5f;
                            int n = Random.Range(0, footStepSounds.Count);
                            if(_useFootStepSounds && footStepSounds.Any()){ audioSource.PlayOneShot(footStepSounds[n],Volume/10);}
                            
                        }
                    }
                    previousGrounded = true;
                } else
                {
                    if(previousGrounded)
                    {
                        if(_useFootStepSounds && jumpSound) { audioSource.PlayOneShot(jumpSound,Volume/10); }
                    }
                    previousGrounded = false;
                }
            }

        
        #endregion

    }

/*     public IEnumerator FOVKickOut()
    {
        float t = Mathf.Abs((playerCamera.fieldOfView - fOVKick.fovStart) / fOVKick.FOVKickAmount);
        while(t < fOVKick.changeTime)
        {
            playerCamera.fieldOfView = fOVKick.fovStart + (fOVKick.KickCurve.Evaluate(t / fOVKick.changeTime) * fOVKick.FOVKickAmount);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FOVKickIn()
    {
        float t = Mathf.Abs((playerCamera.fieldOfView - fOVKick.fovStart) / fOVKick.FOVKickAmount);
        while(t > 0)
        {
            playerCamera.fieldOfView = fOVKick.fovStart + (fOVKick.KickCurve.Evaluate(t / fOVKick.changeTime) * fOVKick.FOVKickAmount);
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        playerCamera.fieldOfView = fOVKick.fovStart;
    } */

    public IEnumerator CameraShake(float Duration, float Magnitude){
        float elapsed =0;
        while(elapsed<Duration && enableCameraShake){
            playerCamera.transform.localPosition =Vector3.MoveTowards(playerCamera.transform.localPosition, new Vector3(cameraStartingPosition.x+ Random.Range(-1,1)*Magnitude,cameraStartingPosition.y+Random.Range(-1,1)*Magnitude,cameraStartingPosition.z), Magnitude*2);
            yield return new WaitForSecondsRealtime(0.001f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.localPosition = cameraStartingPosition;
    }

}
#if UNITY_EDITOR
    [CustomEditor(typeof(FirstPersonAIO)),InitializeOnLoadAttribute]
    public class FPAIO_Editor : Editor{
        FirstPersonAIO t;
        SerializedObject SerT;
        static bool showCrouchMods = false;
        static bool showFOVKickSet = false;
        static bool showAdvanced = false;
        static bool showStaticFS = false;
        SerializedProperty staticFS;
        static bool showWoodFS = false;
        SerializedProperty woodFS;
        static bool showMetalFS = false;
        SerializedProperty metalFS;
        static bool showGrassFS = false;
        SerializedProperty grassFS;
        static bool showDirtFS = false;
        SerializedProperty dirtFS;
        static bool showConcreteFS = false;
        SerializedProperty concreteFS;
        static bool showMudFS = false;
        SerializedProperty mudFS;
        static bool showCustomFS = false;
        SerializedProperty customFS;
        void OnEnable(){
            t = (FirstPersonAIO)target;
            SerT = new SerializedObject(t);
            staticFS = SerT.FindProperty("footStepSounds");
            woodFS = SerT.FindProperty("dynamicFootstep.woodClipSet");
            metalFS = SerT.FindProperty("dynamicFootstep.metalAndGlassClipSet");
            grassFS = SerT.FindProperty("dynamicFootstep.grassClipSet");
            dirtFS = SerT.FindProperty("dynamicFootstep.dirtAndGravelClipSet");
            concreteFS = SerT.FindProperty("dynamicFootstep.rockAndConcreteClipSet");
            mudFS = SerT.FindProperty("dynamicFootstep.woodClipSet");
            customFS = SerT.FindProperty("dynamicFootstep.customClipSet");

            #if _FPAIO == false
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone,"_FPAIO");
            #endif
        }
        public override void OnInspectorGUI(){
            SerT.Update();
            EditorGUILayout.Space();
           
            GUILayout.Label("First Person AIO",new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16});
            GUILayout.Label("version: "+ t.versionNum,new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter});
            EditorGUILayout.Space();


            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            GUILayout.Label("Camera Setup",new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter,fontStyle = FontStyle.Bold, fontSize = 13},GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            t.enableCameraMovement = EditorGUILayout.ToggleLeft(new GUIContent("Enable Camera Movement","Determines whether the player can move camera or not."),t.enableCameraMovement);
            EditorGUILayout.Space();
            GUI.enabled = t.enableCameraMovement;
            t.verticalRotationRange = EditorGUILayout.Slider(new GUIContent("Vertical Rotation Range","Determines how much range does the camera have to move vertically."),t.verticalRotationRange,90,180);
            t.mouseSensitivityInternal = t.mouseSensitivity = EditorGUILayout.Slider(new GUIContent("Mouse Sensitivity","Determines how sensitive the mouse is."),t.mouseSensitivity, 1,15);
            //t.mouseSensitivity = EditorGUILayout.Slider(new GUIContent("Mouse Sensitivity","Determines how sensitive the mouse is."),t.mouseSensitivity, 1,15);
            t.fOVToMouseSensitivity = EditorGUILayout.Slider(new GUIContent("FOV to Mouse Sensitivity","Determines how much the camera's Field Of View will effect the mouse sensitivity. \n\n0 = no effect, 1 = full effect on sensitivity."),t.fOVToMouseSensitivity,0,1);
            t.cameraSmoothing = EditorGUILayout.Slider(new GUIContent("Camera Smoothing","Determines how smooth the camera movement is."),t.cameraSmoothing,1,25);
            t.playerCamera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Player Camera", "Camera attached to this controller"),t.playerCamera,typeof(Camera),true);
            if(!t.playerCamera){EditorGUILayout.HelpBox("A Camera is required for operation.",MessageType.Error);}
            t.enableCameraShake = EditorGUILayout.ToggleLeft(new GUIContent("Enable Camera Shake?", "Call this Coroutine externally with duration ranging from 0.01 to 1, and a magnitude of 0.01 to 0.5."), t.enableCameraShake);
            t.lockAndHideCursor = EditorGUILayout.ToggleLeft(new GUIContent("Lock and Hide Cursor","For debuging or if You don't plan on having a pause menu or quit button."),t.lockAndHideCursor);
            t.autoCrosshair = EditorGUILayout.ToggleLeft(new GUIContent("Auto Crosshair","Determines if a basic crosshair will be generated."),t.autoCrosshair);
            if(t.autoCrosshair){EditorGUI.indentLevel++; EditorGUILayout.BeginHorizontal(); EditorGUILayout.PrefixLabel(new GUIContent("Crosshair","Sprite to use as a crosshair."));t.Crosshair = (Sprite)EditorGUILayout.ObjectField(t.Crosshair,typeof(Sprite),false); EditorGUILayout.EndHorizontal(); EditorGUI.indentLevel--;}
            GUI.enabled = true;
            EditorGUILayout.Space();


            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            GUILayout.Label("Movement Setup",new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter,fontStyle = FontStyle.Bold, fontSize = 13},GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            t.playerCanMove = EditorGUILayout.ToggleLeft(new GUIContent("Enable Player Movement","Determines if the player is allowed to move."),t.playerCanMove);
            GUI.enabled = t.playerCanMove;
            t.walkByDefault = EditorGUILayout.ToggleLeft(new GUIContent("Walk By Default","Determines if the default mode of movement is 'Walk' or 'Srpint'."),t.walkByDefault);
            t.walkSpeed = EditorGUILayout.Slider(new GUIContent("Walk Speed","Determines how fast the player walks."),t.walkSpeed,0.1f,10);
            t.sprintSpeed = EditorGUILayout.Slider(new GUIContent("Sprint Speed","Determines how fast the player sprints."),t.sprintSpeed,0.1f,20);
            t.canJump = EditorGUILayout.ToggleLeft(new GUIContent("Can Player Jump?","Determines if the player is allowed to jump."),t.canJump);
            GUI.enabled = t.playerCanMove && t.canJump; EditorGUI.indentLevel++;
            t.jumpPower = EditorGUILayout.Slider(new GUIContent("Jump Power","Determines how high the player can jump."),t.jumpPower,0.1f,15);
            t.canHoldJump = EditorGUILayout.ToggleLeft(new GUIContent("Hold Jump","Determines if the jump button needs to be pressed down to jump, or if the player can hold the jump button to automaticly jump every time the it hits the ground."),t.canHoldJump);
            EditorGUI.indentLevel --;GUI.enabled = t.playerCanMove;
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            showCrouchMods = EditorGUILayout.BeginFoldoutHeaderGroup(showCrouchMods,new GUIContent("Crouch Modifiers","Stat modifiers that will apply when player is crouching."));
            if(showCrouchMods){
                t._crouchModifiers.useCrouch = EditorGUILayout.ToggleLeft(new GUIContent("Enable Coruch","Determines if the player is allowed to crouch."),t._crouchModifiers.useCrouch);
                GUI.enabled = t.playerCanMove && t._crouchModifiers.useCrouch;
                t._crouchModifiers.crouchKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Crouch Key","Determines what key needs to be pressed to crouch"),t._crouchModifiers.crouchKey);
                t._crouchModifiers.toggleCrouch = EditorGUILayout.ToggleLeft(new GUIContent("Toggle Crouch?","Determines if the crouching behaviour is on a toggle or momentary basis."),t._crouchModifiers.toggleCrouch);
                t._crouchModifiers.crouchWalkSpeedMultiplier = EditorGUILayout.Slider(new GUIContent("Crouch Movement Speed Multiplier","Determines how fast the player can move while crouching."),t._crouchModifiers.crouchWalkSpeedMultiplier,0.01f,1.5f);
                t._crouchModifiers.crouchJumpPowerMultiplier = EditorGUILayout.Slider(new GUIContent("Crouching Jump Power Mult.","Determines how much the player's jumping power is increased or reduced while crouching."),t._crouchModifiers.crouchJumpPowerMultiplier,0,1.5f);
                t._crouchModifiers.crouchOverride = EditorGUILayout.ToggleLeft(new GUIContent("Force Crouch Override","A Toggle that will override the crouch key to force player to crouch."),t._crouchModifiers.crouchOverride);
            }
            GUI.enabled = t.playerCanMove;
            EditorGUILayout.EndFoldoutHeaderGroup();      
            EditorGUILayout.Space();
            showFOVKickSet = EditorGUILayout.BeginFoldoutHeaderGroup(showFOVKickSet, new GUIContent("FOV Kick Settings","Settings for FOV Kick"));
            if(showFOVKickSet){
                GUILayout.Label("Under Development",new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter,fontStyle = FontStyle.Bold, fontSize = 13},GUILayout.ExpandWidth(true));
                GUI.enabled = false;
                t.fOVKick.useFOVKick = EditorGUILayout.ToggleLeft(new GUIContent("Enable FOV Kick","Determines if the camera's Field of View will kick when entering a sprint."),t.fOVKick.useFOVKick);
                //GUI.enabled = t.playerCanMove&&t.fOVKick.useFOVKick;
                t.fOVKick.FOVKickAmount = EditorGUILayout.Slider(new GUIContent("Kick Amount","Determines how much the camera's FOV will kick upon entering a sprint."),t.fOVKick.FOVKickAmount,0,10);
                t.fOVKick.changeTime = EditorGUILayout.Slider(new GUIContent("Change Time","Determines the duration of the FOV kick"),t.fOVKick.changeTime,0.01f,5);
                t.fOVKick.KickCurve = EditorGUILayout.CurveField(new GUIContent("Kick Curve",""),t.fOVKick.KickCurve);
            }
            GUI.enabled =t.playerCanMove;
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
            showAdvanced = EditorGUILayout.BeginFoldoutHeaderGroup(showAdvanced,new GUIContent("Advanced Movement","Advanced movenet settings"));
            if(showAdvanced){
                t.useStamina = EditorGUILayout.ToggleLeft(new GUIContent("Enable Stamina","Determines if spriting will be limited by stamina."),t.useStamina);
                GUI.enabled = t.playerCanMove && t.useStamina; EditorGUI.indentLevel++;
                t.staminaLevel = EditorGUILayout.Slider(new GUIContent("Stamina Level","Determines how much stamina the player has. if left 0, stamina will not be used."),t.staminaLevel,0,100);
                t.staminaDepletionSpeed = EditorGUILayout.Slider(new GUIContent("Stamina Depletion Speed","Determines how quickly the player's stamina depletes."),t.staminaDepletionSpeed,0.1f,9);
                t.drawStaminaMeter = EditorGUILayout.ToggleLeft(new GUIContent("Draw Stamina Meter","Determines if a basic stamina meter will be generated."),t.drawStaminaMeter);
                GUI.enabled = t.playerCanMove; EditorGUI.indentLevel --;
                EditorGUILayout.Space();
                t.advanced.gravityMultiplier = EditorGUILayout.Slider(new GUIContent("Gravity Multiplier","Determines how much the physics engine's gravitational force is multiplied."),t.advanced.gravityMultiplier,0.1f,5);
                t.advanced.maxSlopeAngle = EditorGUILayout.Slider(new GUIContent("Max Slope Angle","EXPERIMENTAL! Determines the maximum angle the player can walk up. If left 0, the slope detection/limiting system will not be used."),t.advanced.maxSlopeAngle,0,89);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            GUI.enabled = true;
            EditorGUILayout.Space();


            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            GUILayout.Label("Headbobbing Setup",new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter,fontStyle = FontStyle.Bold, fontSize = 13},GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            t.useHeadbob = EditorGUILayout.ToggleLeft(new GUIContent("Enable Headbobbing","Determines if headbobbing will be used."),t.useHeadbob);
            GUI.enabled = t.useHeadbob;
            t.head = (Transform)EditorGUILayout.ObjectField(new GUIContent("Head Transform","A transform representing the head. The camera should be a child to this transform."),t.head,typeof(Transform),true);
            if(!t.head){EditorGUILayout.HelpBox("A Head Transform is required for headbobbing.",MessageType.Error);}
            GUI.enabled = t.useHeadbob && t.head;
            t.headbobFrequency = EditorGUILayout.Slider(new GUIContent("Headbob Frequency","Determines how fast the headbobbing cycle is."),t.headbobFrequency,0.1f,10);
            t.headbobSwayAngle = EditorGUILayout.Slider(new GUIContent("Tilt Angle","Determines the angle the head will tilt."),t.headbobSwayAngle,0,10);
            t.headbobHeight = EditorGUILayout.Slider(new GUIContent("Headbob Hight","Determines the highest point the head will reach in the headbob cycle."),t.headbobHeight,0,10);
            t.headbobSideMovement = EditorGUILayout.Slider(new GUIContent("Headbob Horizontal Movement","Determines how much vertical movement will occur in the headbob cycle."),t.headbobSideMovement,0,10);
            t.useJumdLandMovement = EditorGUILayout.ToggleLeft(new GUIContent("Enable Jump/Land Movement","Determines if the headbob system will react to jumping or landing."),t.useJumdLandMovement);
            GUI.enabled = t.useHeadbob && t.head && t.useJumdLandMovement;
            t.jumpAngle = EditorGUILayout.Slider(new GUIContent("Jump Angle","Determines the angle the head will rotate to when player jumps."),t.jumpAngle,0,10);
            t.landAngle = EditorGUILayout.Slider(new GUIContent("Land Angle","Determines the angle the head will rotate to when player lands."),t.landAngle,50,90);
            GUI.enabled = true;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);


            GUILayout.Label("Audio/SFX Setup",new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter,fontStyle = FontStyle.Bold, fontSize = 13},GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            t.Volume = EditorGUILayout.Slider(new GUIContent("Volume","Volume to play audio at."),t.Volume,0,10);
            EditorGUILayout.Space();
            t.fsmode = (FirstPersonAIO.FSMode)EditorGUILayout.EnumPopup(new GUIContent("Footstep Mode","Determines the method used to trigger footsetps."),t. fsmode);
            EditorGUILayout.Space();
        
            if(t.fsmode == FirstPersonAIO.FSMode.Static){
                showStaticFS = EditorGUILayout.BeginFoldoutHeaderGroup(showStaticFS,new GUIContent("Footstep Clips","Audio clips available as footstep sounds."));
                if(showStaticFS){
                    GUILayout.BeginVertical("box");
                    for(int i=0; i<staticFS.arraySize; i++){
                    SerializedProperty LS_ref = staticFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ this.t.footStepSounds.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ this.t.footStepSounds.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ this.t.footStepSounds.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                } 
                EditorGUILayout.EndFoldoutHeaderGroup();
                t.jumpSound = (AudioClip)EditorGUILayout.ObjectField(new GUIContent("Jump Clip","An audio clip that will play when jumping."),t.jumpSound,typeof(AudioClip),false);
                t.landSound = (AudioClip)EditorGUILayout.ObjectField(new GUIContent("Land Clip","An audio clip that will play when landing."),t.landSound,typeof(AudioClip),false);

            }else{
                showWoodFS = EditorGUILayout.BeginFoldoutHeaderGroup(showWoodFS,new GUIContent("Wood Clips","Audio clips available as footsteps when walking on a collider with the Physic Material assigned to 'Wood Physic Material'"));
                if(showWoodFS){
                    GUILayout.BeginVertical("box");
                    t.dynamicFootstep.woodPhysMat = (PhysicMaterial)EditorGUILayout.ObjectField(new GUIContent("Wood Physic Material","Determines what Physic Material will trigger this set of clips"),t.dynamicFootstep.woodPhysMat,typeof(PhysicMaterial),false);
                    if(! t.dynamicFootstep.woodPhysMat){EditorGUILayout.HelpBox("A Physic Material must be assigned first.",MessageType.Warning);}
                    GUI.enabled = t.dynamicFootstep.woodPhysMat; 
                    for(int i=0; i<woodFS.arraySize; i++){ 
                    SerializedProperty LS_ref = woodFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ t.dynamicFootstep.woodClipSet.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ t.dynamicFootstep.woodClipSet.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ t.dynamicFootstep.woodClipSet.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                } 
                GUI.enabled = true;
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.Space();
            
                showMetalFS = EditorGUILayout.BeginFoldoutHeaderGroup(showMetalFS,new GUIContent("Metal & Glass Clips","Audio clips available as footsteps when walking on a collider with the Physic Material assigned to 'Metal & Glass Physic Material'"));
                if(showMetalFS){
                    GUILayout.BeginVertical("box");
                    t.dynamicFootstep.metalAndGlassPhysMat = (PhysicMaterial)EditorGUILayout.ObjectField(new GUIContent("Metal & Glass Physic Material","Determines what Physic Material will trigger this set of clips"),t.dynamicFootstep.metalAndGlassPhysMat,typeof(PhysicMaterial),false);
                    if(! t.dynamicFootstep.metalAndGlassPhysMat){EditorGUILayout.HelpBox("A Physic Material must be assigned first.",MessageType.Warning);}                    
                    GUI.enabled = t.dynamicFootstep.metalAndGlassPhysMat;
                    for(int i=0; i<metalFS.arraySize; i++){ 
                    SerializedProperty LS_ref = metalFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ t.dynamicFootstep.metalAndGlassClipSet.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ t.dynamicFootstep.metalAndGlassClipSet.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ t.dynamicFootstep.metalAndGlassClipSet.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                } 
                GUI.enabled = true;
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.Space();

                showGrassFS = EditorGUILayout.BeginFoldoutHeaderGroup(showGrassFS,new GUIContent("Grass Clips","Audio clips available as footsteps when walking on a collider with the Physic Material assigned to 'Grass Physic Material'"));
                if(showGrassFS){
                    GUILayout.BeginVertical("box");
                    t.dynamicFootstep.grassPhysMat = (PhysicMaterial)EditorGUILayout.ObjectField(new GUIContent("Grass Physic Material","Determines what Physic Material will trigger this set of clips"),t.dynamicFootstep.grassPhysMat,typeof(PhysicMaterial),false);
                    if(! t.dynamicFootstep.grassPhysMat){EditorGUILayout.HelpBox("A Physic Material must be assigned first.",MessageType.Warning);}                    
                    GUI.enabled = t.dynamicFootstep.grassPhysMat;
                    for(int i=0; i<grassFS.arraySize; i++){ 
                    SerializedProperty LS_ref = grassFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ t.dynamicFootstep.grassClipSet.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ t.dynamicFootstep.grassClipSet.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ t.dynamicFootstep.grassClipSet.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                } 
                GUI.enabled = true;
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.Space();

                showDirtFS = EditorGUILayout.BeginFoldoutHeaderGroup(showDirtFS,new GUIContent("Dirt & Gravel Clips","Audio clips available as footsteps when walking on a collider with the Physic Material assigned to 'Dirt & Gravel Physic Material'"));
                if(showDirtFS){
                    GUILayout.BeginVertical("box");
                    t.dynamicFootstep.dirtAndGravelPhysMat = (PhysicMaterial)EditorGUILayout.ObjectField(new GUIContent("Dirt & Gravel Physic Material","Determines what Physic Material will trigger this set of clips"),t.dynamicFootstep.dirtAndGravelPhysMat,typeof(PhysicMaterial),false);
                    if(! t.dynamicFootstep.dirtAndGravelPhysMat){EditorGUILayout.HelpBox("A Physic Material must be assigned first.",MessageType.Warning);}                    
                    GUI.enabled = t.dynamicFootstep.dirtAndGravelPhysMat;
                    for(int i=0; i<dirtFS.arraySize; i++){ 
                    SerializedProperty LS_ref = dirtFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ t.dynamicFootstep.dirtAndGravelClipSet.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ t.dynamicFootstep.dirtAndGravelClipSet.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ t.dynamicFootstep.dirtAndGravelClipSet.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                } 
                GUI.enabled = true;
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.Space();

                showConcreteFS = EditorGUILayout.BeginFoldoutHeaderGroup(showConcreteFS,new GUIContent("Rock & Concrete Clips","Audio clips available as footsteps when walking on a collider with the Physic Material assigned to 'Rock & Concrete Physic Material'"));
                if(showConcreteFS){
                    GUILayout.BeginVertical("box");
                    t.dynamicFootstep.rockAndConcretePhysMat = (PhysicMaterial)EditorGUILayout.ObjectField(new GUIContent("Rock & Concrete Physic Material","Determines what Physic Material will trigger this set of clips"),t.dynamicFootstep.rockAndConcretePhysMat,typeof(PhysicMaterial),false);
                    if(! t.dynamicFootstep.rockAndConcretePhysMat){EditorGUILayout.HelpBox("A Physic Material must be assigned first.",MessageType.Warning);}                    
                    GUI.enabled = t.dynamicFootstep.rockAndConcretePhysMat;
                    for(int i=0; i<concreteFS.arraySize; i++){ 
                    SerializedProperty LS_ref = concreteFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ t.dynamicFootstep.rockAndConcreteClipSet.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ t.dynamicFootstep.rockAndConcreteClipSet.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ t.dynamicFootstep.rockAndConcreteClipSet.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                } 
                GUI.enabled = true;
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.Space();

                showMudFS = EditorGUILayout.BeginFoldoutHeaderGroup(showMudFS,new GUIContent("Mud Clips","Audio clips available as footsteps when walking on a collider with the Physic Material assigned to 'Mud Physic Material'"));
                if(showMudFS){
                    GUILayout.BeginVertical("box");
                    t.dynamicFootstep.mudPhysMat = (PhysicMaterial)EditorGUILayout.ObjectField(new GUIContent("Mud Physic Material","Determines what Physic Material will trigger this set of clips"),t.dynamicFootstep.mudPhysMat,typeof(PhysicMaterial),false);
                    if(! t.dynamicFootstep.mudPhysMat){EditorGUILayout.HelpBox("A Physic Material must be assigned first.",MessageType.Warning);}                    
                    GUI.enabled = t.dynamicFootstep.mudPhysMat;
                    for(int i=0; i<mudFS.arraySize; i++){ 
                    SerializedProperty LS_ref = mudFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ t.dynamicFootstep.mudClipSet.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ t.dynamicFootstep.mudClipSet.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ t.dynamicFootstep.mudClipSet.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                } 
                GUI.enabled = true;
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.Space();

                showCustomFS = EditorGUILayout.BeginFoldoutHeaderGroup(showCustomFS,new GUIContent("Custom Material Clips","Audio clips available as footsteps when walking on a collider with the Physic Material assigned to 'Custom Physic Material'"));
                if(showCustomFS){
                    GUILayout.BeginVertical("box");
                    t.dynamicFootstep.customPhysMat = (PhysicMaterial)EditorGUILayout.ObjectField(new GUIContent("Custom Physic Material","Determines what Physic Material will trigger this set of clips"),t.dynamicFootstep.customPhysMat,typeof(PhysicMaterial),false);
                    if(! t.dynamicFootstep.customPhysMat){EditorGUILayout.HelpBox("A Physic Material must be assigned first.",MessageType.Warning);}                    
                    GUI.enabled = t.dynamicFootstep.customPhysMat;
                    for(int i=0; i<customFS.arraySize; i++){ 
                    SerializedProperty LS_ref = customFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ t.dynamicFootstep.customClipSet.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ t.dynamicFootstep.customClipSet.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ t.dynamicFootstep.customClipSet.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                }
                GUI.enabled = true;
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.Space();

                showStaticFS = EditorGUILayout.BeginFoldoutHeaderGroup(showStaticFS,new GUIContent("Fallback Footstep Clips","Audio clips available as footsteps in case a collider with an unrecognized/null Physic Material is walked on."));
                if(showStaticFS){
                    GUILayout.BeginVertical("box");
                    for(int i=0; i<staticFS.arraySize; i++){
                    SerializedProperty LS_ref = staticFS.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal("box");
                    LS_ref.objectReferenceValue = EditorGUILayout.ObjectField("Clip "+(i+1)+":",LS_ref.objectReferenceValue,typeof(AudioClip),false);
                    if(GUILayout.Button(new GUIContent("X", "Remove this clip"),GUILayout.MaxWidth(20))){ this.t.footStepSounds.RemoveAt(i);}
                    EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    
                    if(GUILayout.Button(new GUIContent("Add Clip", "Add new clip entry"))){ this.t.footStepSounds.Add(null);}
                    if(GUILayout.Button(new GUIContent("Remove All Clips", "Remove all clip entries"))){ this.t.footStepSounds.Clear();}
                    EditorGUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                } 
                EditorGUILayout.EndFoldoutHeaderGroup();

            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Label(new GUIContent("Support Address","Need help? No Problem! We're always happy to help with any issue you may have."),new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter},GUILayout.ExpandWidth(true));
            EditorGUILayout.SelectableLabel(new GUIContent("support@aedangraves.info","Need help? No Problem! We're always happy to help with any issue you may have.").text,new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter,fontStyle = FontStyle.Bold, fontSize = 13},GUILayout.ExpandWidth(true));
            EditorGUILayout.EndVertical();
            if(GUI.changed){EditorUtility.SetDirty(t); Undo.RecordObject(t,"FPAIO Change"); SerT.ApplyModifiedProperties();}
        }
    }
#endif




