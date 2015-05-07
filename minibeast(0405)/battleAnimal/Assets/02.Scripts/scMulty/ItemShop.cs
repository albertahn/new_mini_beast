using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour {
	private RectTransform itemWindow;
	private RectTransform Xbutton;
	private RectTransform[] itemlist;
	public RectTransform[] inventorylist;

	private Image[] inventoryWindow;
	public Sprite sword,amor,blank;

	private float sx,sy;
	private float xx,xy;
	private float ix,iy;

	private int[] stack;
	private int sp;
	private int count;
	private bool[] invBool;


	// Use this for initialization
	void Start () {
		itemWindow = GameObject.Find ("ItemWindow").GetComponent<RectTransform>();
		Xbutton = GameObject.Find ("Xbutton").GetComponent<RectTransform>();
		itemlist = GameObject.Find ("item_list").GetComponentsInChildren<RectTransform> ();
		inventorylist = GameObject.Find ("inventory_list").GetComponentsInChildren<RectTransform> ();
		inventoryWindow = GameObject.Find ("inventory_list").GetComponentsInChildren<Image> ();

		invBool = new bool[6];
		for(int i=0;i<6;i++){
			inventoryWindow[i].sprite = blank;
			invBool[i] = false;
		}

		sx = itemWindow.sizeDelta.x;
		sy = itemWindow.sizeDelta.y;

		xx = Xbutton.sizeDelta.x;
		xy = Xbutton.sizeDelta.y;

		ix = itemlist [1].sizeDelta.x;
		iy = itemlist [1].sizeDelta.y;

		stack = new int[6];
		stack[0]=5;
		stack[1]=4;
		stack[2]=3;
		stack[3]=2;
		stack[4]=1;
		stack[5]=0;
		sp=5;
		count = 0;

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
		if (count < 6) {
			inventoryWindow[stack[sp]].sprite = sword;
			ClientState.addInventory ("sword",stack[sp]);
			invBool[stack[sp]] = true;
			sp--;
			count++;	
		}
	}

	public void buyAmor(){
		if (count < 6) {
			inventoryWindow [stack[sp]].sprite = amor;
			ClientState.addInventory ("amor",stack[sp]);
			invBool[stack[sp]] = true;
			sp--;
			count++;	
		}
	}



	public void sellItem1(){
		if (invBool [0]) {
			sp++;
			stack[sp] = 0;
			inventoryWindow[0].sprite = blank;
			ClientState.addInventory ("",0);
			count--;
			invBool[0] = false;
		}
	}

	public void sellItem2(){
		if (invBool [1]) {
			sp++;
			stack[sp] = 1;
			inventoryWindow[1].sprite = blank;
			ClientState.addInventory ("",1);
			count--;
			invBool[1] = false;
		}
	}

	public void sellItem3(){
		if (invBool [2]) {
			sp++;
			stack[sp] = 2;
			inventoryWindow[2].sprite = blank;
			ClientState.addInventory ("",2);
			count--;
			invBool[2] = false;
		}
	}

	public void sellItem4(){
		if (invBool [3]) {
			sp++;
			stack[sp] = 3;
			inventoryWindow[3].sprite = blank;
			ClientState.addInventory ("",3);
			count--;
			invBool[3] = false;
		}
	}

	public void sellItem5(){
		if (invBool [4]) {
			sp++;
			stack[sp] = 4;
			inventoryWindow[4].sprite = blank;
			ClientState.addInventory ("",4);
			count--;
			invBool[4] = false;
		}
	}

	public void sellItem6(){
		if (invBool [5]) {
			sp++;
			stack[sp] = 5;
			inventoryWindow[5].sprite = blank;
			ClientState.addInventory ("",5);
			count--;
			invBool[5] = false;
		}
	}
}