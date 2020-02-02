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

    private List<Collider2D> FirstPassColliders = new List<Collider2D>();
    private List<Collider2D> SelectedColliders = new List<Collider2D>();

    private Vector2 MoveMouseStart;
    private Vector2 MoveMouseEnd;
    private int SelectionNumber = 0;

    ContactFilter2D FriendlyFilter;

    // Start is called before the first frame update
    void Start()
    {
        dragBoxCollider = GetComponentInChildren<BoxCollider2D>();
        FriendlyFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Friendly") };
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
            //dragBoxCollider.OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Friendly") }, colliders);

            //Marcus: "No Collider necessary."
            Vector2 minvec = new Vector2(Mathf.Min(sizingStartWorldPos.x, sizingCurrWorldPos.x), Mathf.Min(sizingStartWorldPos.y, sizingCurrWorldPos.y));
            Vector2 maxvec = new Vector2(Mathf.Max(sizingStartWorldPos.x, sizingCurrWorldPos.x), Mathf.Max(sizingStartWorldPos.y, sizingCurrWorldPos.y));
            Physics2D.OverlapBox(minvec + ((maxvec - minvec) * .5f), maxvec - minvec, 0f, FriendlyFilter, FirstPassColliders);

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                foreach (var col in SelectedColliders)
                {
                    col.GetComponent<SpriteRenderer>().color = Color.white;
                }
                SelectedColliders.Clear();
            }
            foreach (var collider in FirstPassColliders)
            {
                collider.gameObject.GetComponentInParent<SpriteRenderer>().color = Color.blue;
            }
            SelectedColliders.AddRange(FirstPassColliders);
            transform.localScale = originalScale;

            sizing = false;
        }

        if (Input.GetMouseButtonDown(1))
            MoveMouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetMouseButtonUp(1))
        {
            MoveMouseEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (SelectedColliders.Count > 0)
                if (Vector2.Distance(MoveMouseEnd, MoveMouseStart) < .3)
                {
                    if (SelectionNumber >= SelectedColliders.Count) { SelectionNumber = 0; }
                    SelectedColliders[SelectionNumber++].GetComponent<Entity>().TargetPos = MoveMouseStart;
                }
                else
                {
                    var x = 1.0f / (SelectedColliders.Count-1);
                    for (int i = 0; i < SelectedColliders.Count; i++)
                    {
                        SelectedColliders[i].GetComponent<Entity>().TargetPos = Vector2.Lerp(MoveMouseStart, MoveMouseEnd, x * i);
                        SelectedColliders[i].GetComponent<Entity>().TargetPosDesignated = true;
                    }
                }
        }

    }
}
