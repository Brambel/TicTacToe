using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text[] buttonList;
	public GameObject gameOverPanel;
	public Button restartButton;
    public Text turnIndicator;

	private Text gameOverText = null;
	private string playerSide;
	private int turnCount;


	void Awake()
	{
		SetGameControllerReferenceOnButtons();
		playerSide = "X";
        turnIndicator.text = playerSide;
		turnCount = 0;
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

		turnCount++;
		bool gameFinished = false;
		//horizontal
		for (int i = 0; i < 9; i += 3) {

			if(buttonList[0 + i].text == playerSide && buttonList[1 + i].text == playerSide && buttonList[2 + i].text == playerSide) {
				gameFinished = true;
			}
		}

		//vertical
		for(int i = 0;i<3;++i){
			
			if(buttonList[0+i].text == playerSide && buttonList[3+i].text == playerSide && buttonList[6+i].text == playerSide) {
				gameFinished = true;
			}
		}

		//left -> right diagonal
		if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide)
		{
			gameFinished = true;
		}

		//right -> left diagonal
		if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide)
		{
			gameFinished = true;
		}

		//check to see if we call gameover for win or draw
		if(gameFinished) {
			GameOver(true);
		} else {
			//check for draw
			if(turnCount > 8) {
				GameOver(false);
			} else {
				changeSides();
			}
		}


	}

	private void GameOver(bool win){
		
		Button b = null;
		for(int i = 0; i < 9; i++){
			b = buttonList[i].GetComponentInParent<Button>();

			if(b != null) {
				b.interactable = false;
			}
		}
		if(win) {
			gameOverText.text = playerSide + "'s win!";
		} else {
			gameOverText.text = "It's a draw!";
		}
		gameOverPanel.SetActive(true);
		restartButton.interactable = true;
	}

	void changeSides(){
		playerSide = (playerSide == "X") ? "O" : "X";
        turnIndicator.text = playerSide;
	}

	public void reset(){
		GridSpace gs = null;
		turnCount = 0;
		playerSide = "X";

		for(int i = 0; i < 9; i++){
			gs = buttonList[i].GetComponentInParent<GridSpace>();

			if(gs != null) {
				gs.reset();
			}
		}

		gameOverPanel.SetActive(false);
	}
}
