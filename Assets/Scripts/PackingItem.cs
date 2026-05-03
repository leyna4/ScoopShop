using UnityEngine;
using UnityEngine.EventSystems;

public class PackingItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum ItemType { Pink, Red, Blue }
    public ItemType itemType;

    private Vector2 startAnchoredPos;
    private Transform startParent;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Baþlangýç pozisyonunu kaydet
        startParent = transform.parent;
        startAnchoredPos = rectTransform.anchoredPosition;

        // Canvas'a taþý üstte görünsün
        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // BoxZone'a býrakýldý mý?
        GameObject boxZone = GameObject.FindWithTag("BoxZone");
        if (boxZone == null)
        {
            GeriDon();
            return;
        }

        RectTransform boxRect = boxZone.GetComponent<RectTransform>();
        if (RectTransformUtility.RectangleContainsScreenPoint(
            boxRect, eventData.position, eventData.pressEventCamera))
        {
            // Kutuya býrak
            transform.SetParent(boxZone.transform, true);
            rectTransform.anchoredPosition = new Vector2(
                Random.Range(-50f, 50f),
                Random.Range(-25f, 25f));
            FindObjectOfType<PackingManager>().ItemAddedToBox(gameObject);
        }
        else
        {
            GeriDon();
        }
    }

    void GeriDon()
    {
        transform.SetParent(startParent, true);
        rectTransform.anchoredPosition = startAnchoredPos;
    }
}