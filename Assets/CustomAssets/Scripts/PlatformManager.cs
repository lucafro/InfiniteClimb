using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject XrRig;
    public GameObject NonVrPlayer;

    [SerializeField]
    private bool _VR = true;

    private void Awake()
    {
        SetPlatform();
    }

    [MyBox.ButtonMethod]
    public void SetPlatform()
    {
        if (XrRig != null && NonVrPlayer != null)
        {
            if (_VR)
            {
                XrRig.SetActive(true);
                NonVrPlayer.SetActive(false);
            }
            else
            {
                XrRig.SetActive(false);
                NonVrPlayer.SetActive(true);
            }
        }
    }
}
