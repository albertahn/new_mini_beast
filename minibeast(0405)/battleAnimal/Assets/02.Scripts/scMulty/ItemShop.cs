using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour {
	private RectTransform itemWindow;
	private RectTransform Xbutton;
	private RectTransform[] itemlist;
	public RectTransform[] inventorylist;

	private Image[] inventoryWindow;
	public Sprite sword,amor;

	private float sx,sy;
	private float xx,xy;
	private float ix,iy;


	// Use this for initialization
	void Start () {
		itemWindow = GameObject.Find ("ItemWindow").GetComponent<RectTransform>();
		Xbutton = GameObject.Find ("Xbutton").GetComponent<RectTransform>();
		itemlist = GameObject.Find ("item_list").GetComponentsInChildren<RectTransform> ();
		inventorylist = GameObject.Find ("inventory_list").GetComponentsInChildren<RectTransform> ();
		inventoryWindow = GameObject.Find ("inventory_list").GetComponentsInChildren<Image> ();

		sx = itemWindow.sizeDelta.x;
		sy = itemWindow.sizeDelta.y;

		xx = Xbutton.sizeDelta.x;
		xy = Xbutton.sizeDelta.y;

		ix = itemlist [1].sizeDelta.x;
		iy = itemlist [1].sizeDelta.y;
		closeWindow ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void openWindow(){
		itemWindow.sizeDelta = new Vector2 (sx, sy);
		Xbutton.sizeDelta = new Vector2 (xx, xy);

		for (int i=1; i<itemlist.Length; i++) {
			itemlist[i].sizeDelta = new Vector2 (ix, iy);
		}
		for (int i=1; i<inventorylist.Length; i++) {
			inventorylist[i].sizeDelta = new Vector2 (ix, iy);
		}
	}

	public void closeWindow(){
		itemWindow.sizeDelta = new Vector2 (0, 0);
		Xbutton.sizeDelta = new Vector2 (0,0);

		for (int i=1;i< itemlist.Length; i++) {
			itemlist[i].sizeDelta = new Vector2 (0, 0);
		}
		for (int i=1; i<inventorylist.Length; i++) {
			inventorylist[i].sizeDelta = new Vector2 (0, 0);
		}
	}

	public void buySword(){
		if (ClientState.idx < 3) {
						inventoryWindow [ClientState.idx].sprite = sword;
						ClientState.addInventory ("sword");
				}
	}

	public void buyAmor(){
		if (ClientState.idx < 3) {
						inventoryWindow [ClientState.idx].sprite = amor;
						ClientState.addInventory ("amor");
				}
	}
}