using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Canvas Canvas;

    private RectTransform _rt;
    private CanvasGroup _cg;
    private Image _img;

    private Vector3 _originPos;
    private bool _drag;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _cg = GetComponent<CanvasGroup>();

        _img = GetComponent<Image>();
        _originPos = _rt.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
        _cg.blocksRaycasts = false;
        _img.maskable = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rt.anchoredPosition += eventData.delta / Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _drag = false;
        _cg.blocksRaycasts = true;
        _img.maskable = true;
        _rt.anchoredPosition = _originPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ShopManager.Current.ShopButton_Click();

        Color c = _img.color;
        c.a = 0f;
        _img.color = c;
    }

    private void OnEnable()
    {
        _drag = false;
        _cg.blocksRaycasts = true;
        _img.maskable = true;
        _rt.anchoredPosition = _originPos;
        
        Color c = _img.color;
        c.a = 1f;
        _img.color = c;
    }
}
