using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections;
using UnityEngine;

public enum GameState { 
    PreStart,
    Start,
}

public class CallibrationManager : MonoBehaviour
{
    public TimeManager timeManager;
    public SpawnManager spawnManager;
    public enum BreathingState { 
        Inhale,
        Exhale
    }

    public BreathingState breatheState;
    public GameState gameState;

    public bool CanCalibrate;

    private float InhaleTime;
    private float ExhaleTime;

    public int calibrateTime = 4;
    private int currentCalibrateTime;

    public float InhaleTimeAvg;
    public float ExhaleTimeAvg;

    private float t;
    private bool isGrowing;

    private void Start()
    {
        gameState = GameState.PreStart;
        CanCalibrate = false;
        InhaleTime = 0;
        isGrowing = false;
    }

    void Update()
    {
        InputSetting();
        if (CanCalibrate) {
            if (currentCalibrateTime < calibrateTime)
            {
                switch (breatheState)
                {
                    case BreathingState.Inhale:
                        InhaleTime += Time.deltaTime;
                        break;
                    case BreathingState.Exhale:
                        if (InhaleTime > 0)
                        {
                            ExhaleTime += Time.deltaTime;
                        }
                        break;
                }
            }
            else {
                gameState = GameState.Start;
            }
        }

        if (gameState == GameState.Start) {
            if (!isGrowing)
            {
                spawnManager.SpawnObject();
                isGrowing = true;
            }
            InhaleTimeAvg = InhaleTime / 3;
            ExhaleTimeAvg = ExhaleTime / 3;
            switch (breatheState) {
                case BreathingState.Inhale:
                    timeManager.TimeSlowDown();
                    if (t < InhaleTimeAvg)
                    {
                        Debug.Log("Slow time!");
                        t += Time.unscaledDeltaTime;
                    }
                    else
                    {
                        t = 0;
                        breatheState = BreathingState.Exhale;
                    }
                    break;
                case BreathingState.Exhale:
                    timeManager.ResetTimeScale();
                    if (t < ExhaleTimeAvg)
                    {
                        t += Time.unscaledDeltaTime;
                    }
                    else
                    {
                        t = 0;
                        breatheState = BreathingState.Inhale;
                    }
                    break;
            
            }

        }
    }

    private void InputSetting() {
        var rightInput = GetInput(VRInputDeviceHand.Right);
        var leftInput = GetInput(VRInputDeviceHand.Left);

        
        if (rightInput != null)
        {
            if (rightInput.GetButtonDown(VRButton.One))
            {
                breatheState = BreathingState.Inhale;
            }
            else if (rightInput.GetButtonUp(VRButton.One))
            {
                breatheState = BreathingState.Exhale;
                if (CanCalibrate)
                {
                    currentCalibrateTime += 1;
                }
            }
        }

        if (leftInput != null)
        {

        }

    }

    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }

    public void StartCalibrate() {
        StartCoroutine(DelayToCalibrate());
    }

    IEnumerator DelayToCalibrate() {
        yield return new WaitForSeconds(0.5f);
        CanCalibrate = true;

    }
}
