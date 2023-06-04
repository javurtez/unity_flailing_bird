using UnityEngine;

public class TapPanel : MonoBehaviour, IPanel
{
    [SerializeField]
    private GameObject tapObject;

    private void Awake()
    {
        ControlManager.Tap += Close;
    }
    private void OnDisable()
    {
        ControlManager.Tap -= Close;
    }
    private void Start()
    {
        tapObject.LeanScale(Vector2.one * 1.1f, .5f).setLoopPingPong(-1);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        if (!gameObject.activeInHierarchy) return;
        gameObject.SetActive(false);
    }
}