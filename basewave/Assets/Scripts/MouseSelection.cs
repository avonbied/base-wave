using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelection : MonoBehaviour
{
    protected bool sizing = false;
    private Vector3 sizingStartScreenPos;
    private Vector3 sizingCurrScreenPos;

    private Vector3 sizingStartWorldPos;
    private Vector3 sizingCurrWorldPos;

    private BoxCollider2D dragBoxCollider;

    private Vector3 originalScale;

    private List<Collider2D> colliderTest = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        dragBoxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnGUI()
    {
        if (sizing)
        {
            float left = sizingStartScreenPos.x;
            float width = sizingCurrScreenPos.x - sizingStartScreenPos.x;
            float height = sizingCurrScreenPos.y - sizingStartScreenPos.y;

            float top = (Screen.height - sizingStartScreenPos.y) - height;
            var rt = new Rect(left, top, width, height);
            GUI.Box(rt, "");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sizingStartScreenPos = Input.mousePosition;

            sizingStartWorldPos = Camera.main.ScreenToWorldPoint(sizingStartScreenPos);
            sizingStartWorldPos.z = 0;
            originalScale = transform.localScale;

            transform.position = sizingStartWorldPos;
            sizing = true;
        }

        if (sizing)
        {
            sizingCurrScreenPos = Input.mousePosition;

            transform.localScale = originalScale;
            sizingCurrWorldPos = Camera.main.ScreenToWorldPoint(sizingCurrScreenPos);
            sizingCurrWorldPos.z = 0;
            Vector3 dif = sizingCurrWorldPos - sizingStartWorldPos;
            transform.localScale = new Vector3(dif.x, -dif.y, dif.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragBoxCollider.OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Friendly") }, colliderTest);
            Debug.Log(colliderTest.Count);
            foreach (var collider in colliderTest)
            {
                collider.gameObject.GetComponentInParent<SpriteRenderer>().color = Color.blue;
            }
            transform.localScale = originalScale;

            sizing = false;
        }
    }
}
