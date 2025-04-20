using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour {
    public GameObject Canvas;
    public NetworkManager NetworkManager;

    private bool isDragging = false;
    private bool isOverDiscardPile = false;
    private bool isDraggable = true;
    private GameObject discardPile;
    private GameObject startParent;
    private Vector2 startPosition;

    private void Start() {
        Canvas = GameObject.Find("Main Canvas");
        NetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
    void Update() {
        if (isDragging) {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isOverDiscardPile = true;
        discardPile = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isOverDiscardPile = false;
        discardPile = null;
    }

    public void StartDrag() {
        if (!isDraggable) {
            return;
        }
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag() {
        if (!isDraggable) {
            return;
        }

        isDragging = false;
        if (isOverDiscardPile) {
            transform.SetParent(discardPile.transform, false);
            isDraggable = false;
            NetworkManager.SendMessage("drop");
        } else {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }
}
