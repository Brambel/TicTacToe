using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text[] buttonList;

	void Awake(){
		SetGameControllerReferenceOnButtons ();
	}

	void SetGameControllerReferenceOnButtons(){

		GridSpace g = null;

		for (int i = 0; i < 9; ++i){
			g = buttonList[i].GetComponentInParent<GridSpace>();

			if(g != null){
				g.SetGameControllerReference(this);
			}
		}
	}

		public string GetPlayerSide(){
		return "?";
	}

	public void EndTurn(){
		Debug.Log ("EndTurn is not implemented");
	}
}
