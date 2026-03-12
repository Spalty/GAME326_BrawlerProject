using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private GameObject controlPanel;

    private void Awake()
    {
        controlPanel.SetActive(false);
    }

    public void EnableControlPanel()
    {
        controlPanel.SetActive(true);
    }

    public void DisableControlPanel()
    {
        controlPanel.SetActive(false);
    }
}
