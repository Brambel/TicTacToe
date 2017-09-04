using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text[] buttonList;
	public GameObject gameOverPanel;

	private Text gameOverText = null;
	private string playerSide;

	void Awake()
	{
		SetGameControllerReferenceOnButtons();
		playerSide = "X";
		gameOverText = gameOverPanel.GetComponentInChildren<Text>();

		if(gameOverText == null) {
			Debug.Log("failed to get gameover text object");
		} else {
			gameOverText.text = "";
			gameOverPanel.SetActive(false);
		}
	}

	void SetGameControllerReferenceOnButtons()
	{

		GridSpace g = null;

		for (int i = 0; i < 9; ++i)
		{
			g = buttonList[i].GetComponentInParent<GridSpace>();

			if(g != null)
			{
				g.SetGameControllerReference(this);
			}
		}
	}

	public string GetPlayerSide()
	{
		return playerSide;
	}

	public void EndTurn(){

		//horizontal
		for (int i = 0; i < 9; i += 3) {

			if(buttonList[0 + i].text == playerSide && buttonList[1 + i].text == playerSide && buttonList[2 + i].text == playerSide) {
				GameOver();
			}
		}

		//vertical
		for(int i = 0;i<3;++i){
			
			if(buttonList[0+i].text == playerSide && buttonList[3+i].text == playerSide && buttonList[6+i].text == playerSide) {
				GameOver();
			}
		}

		//left -> right diagonal
		if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide)
		{
			GameOver();
		}

		//right -> left diagonal
		if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide)
		{
			GameOver();
		}
		changeSides();
	}

	private void GameOver(){
		Button b = null;
		for(int i = 0; i < 9; i++){
			b = buttonList[i].GetComponentInParent<Button>();

			if(b != null) {
				b.interactable = false;
			}
		}

		gameOverText.text = playerSide+"'s win!";
		gameOverPanel.SetActive(true);
		reset();
	}

	void changeSides(){
		playerSide = (playerSide == "X") ? "O" : "X";
	}

	void reset(){
		GridSpace gs = null;
		for(int i = 0; i < 9; i++){
			gs = buttonList[i].GetComponentInParent<GridSpace>();

			if(gs != null) {
				gs.reset();
			}
		}
	}
}
