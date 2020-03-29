using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;


namespace TestTask
{
    public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static bool dragDisabled = false;
        private Image _image;
        public ItemData Data;
        public int type;

        public static DragAndDropItem draggedItem;
        public static GameObject icon;
        public static DragAndDropCell sourceCell;

        public delegate void DragEvent(DragAndDropItem item);
        public static event DragEvent OnItemDragStartEvent;
        public static event DragEvent OnItemDragEndEvent;

        private static Canvas canvas;
        private static string canvasName = "DragAndDropCanvas";
        private static int canvasSortOrder = 100;

        void GetTypeItem()
        {
            type = Data.Type.GetHashCode();
            _image = gameObject.GetComponent<Image>();
            if (_image.sprite == null)
            {
                _image.sprite = Data.Image;
            }            
        }

        void Awake()
        {
            Invoke("GetTypeItem", 1);
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject(canvasName);
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = canvasSortOrder;
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (dragDisabled == false)
            {
                sourceCell = GetCell();
                draggedItem = this;

                icon = new GameObject();
                icon.transform.SetParent(canvas.transform);
                icon.name = "Icon";
                Image myImage = GetComponent<Image>();
                myImage.raycastTarget = false;
                Image iconImage = icon.AddComponent<Image>();
                iconImage.raycastTarget = false;
                iconImage.sprite = myImage.sprite;
                RectTransform iconRect = icon.GetComponent<RectTransform>();
                RectTransform myRect = GetComponent<RectTransform>();
                iconRect.pivot = new Vector2(0.5f, 0.5f);
                iconRect.anchorMin = new Vector2(0.5f, 0.5f);
                iconRect.anchorMax = new Vector2(0.5f, 0.5f);
                iconRect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height);

                if (OnItemDragStartEvent != null)
                {
                    OnItemDragStartEvent(this);
                }
            }
        }

        public void OnDrag(PointerEventData data)
        {
            if (icon != null)
            {
                icon.transform.position = Input.mousePosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetConditions();
        }

        private void ResetConditions()
        {
            if (icon != null)
            {
                Destroy(icon);
            }
            if (OnItemDragEndEvent != null)
            {
                OnItemDragEndEvent(this);
            }
            draggedItem = null;
            icon = null;
            sourceCell = null;
        }

        public void MakeRaycast(bool condition)
        {
            Image image = GetComponent<Image>();
            if (image != null)
            {
                image.raycastTarget = condition;
            }
        }

        public DragAndDropCell GetCell()
        {
            return GetComponentInParent<DragAndDropCell>();
        }

        void OnDisable()
        {
            ResetConditions();
        }
    }
}

