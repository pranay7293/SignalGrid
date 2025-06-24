using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Image background;
    public Color onColor = Color.yellow;
    public Color offColor = Color.black;
    public AudioClip clip;

    public Vector2Int Position { get; private set; }
    public bool IsOn { get; private set; }

    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Init(Vector2Int pos)
    {
        Position = pos;
        IsOn = false;
        UpdateVisual();
    }

    public void Toggle()
    {
        IsOn = !IsOn;
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        background.color = IsOn ? onColor : offColor;
    }

    public void OnClick()
    {
        source.PlayOneShot(clip);
        if (UIManager.Instance.IsPeekMode())
        {
            UIManager.Instance.HandlePeekTile(Position);
            return;
        }
        SignalGridManager.Instance.ToggleTileGroup(Position);
        UIManager.Instance.IncrementMoveCount();
        
    }
}
