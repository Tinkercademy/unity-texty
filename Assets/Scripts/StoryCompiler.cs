using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class processes the raw text file and outputs the various elements of the text as needed
public class StoryCompiler
{
	private string story;
	private List<string[]> storyList = new List<string[]>();//list of string arrays - each array is [title, next title it should find. text]
	
	private int currentProgress = 0;
	
	private string imageTitle;
	private List<string> buttonTitles = new List<string>();
	
	private bool gameEnd = false;
	
	//goes through the gametext file and compiles it into a list of string arrays
    public void Compile(TextAsset textAsset) {
		story = textAsset.ToString();
		
		//these 3 strings form an array of [title, title of next part of story after this, text for this part of story]
		string tempStoryCompilerTitle = "";
		string tempStoryCompilerAfter = "";
		string tempStoryCompiler = "";
		
		for (int i = 0; i<story.Length;i++)
		{
			if (story[i].ToString() == "\n" || story[i].ToString() == "\r") { //if new line, create array
				if (!System.String.IsNullOrEmpty(tempStoryCompiler)) {
						storyList.Add(new string[]{tempStoryCompilerTitle, tempStoryCompilerAfter, tempStoryCompiler});
						tempStoryCompiler = "";
						tempStoryCompilerTitle = "";
						tempStoryCompilerAfter = "";
				}
			}
			else if (story[i].ToString() == "|"){ //after title, there should be a '|' character to indicate that is the title
				tempStoryCompilerTitle = tempStoryCompiler;
				tempStoryCompiler = "";
			}
			else if (story[i].ToString() == ">") { //after title of next part of story, there should be a '>' character to indicate that
				tempStoryCompilerAfter = tempStoryCompiler;
				tempStoryCompiler = "";
			}
			else {
				tempStoryCompiler += story[i].ToString();
			}
		}
		storyList.Add(new string[]{tempStoryCompilerTitle, tempStoryCompilerAfter, tempStoryCompiler});//get the last line
	}
	
	public string ProgressStory(string titleToSearchFor) //returns the string of text to display on screen, titleToSearchFor will have a value if triggered by a button
	{
		if (titleToSearchFor != "") {
			for (int i=0; i<storyList.Count; i++) {
				if (storyList[i][0] == titleToSearchFor) {
					currentProgress = i;
				}
			}
		}
		if (storyList[currentProgress][2].ToString()[0] == '*') {//* denotes an option, should create buttons for each option
			int temp = currentProgress; //saves currentprogress because findchoices iterates through the storylist, so the text field remains the same with the buttons underneath
			FindChoices();//recursive function that goes through next lines that have * in them as well
			imageTitle = storyList[temp-1][0];//-1 because should show the previous string before the button texts
			return storyList[temp-1][2];
		}
		else {
			currentProgress++;
			if (currentProgress == (storyList.Count)) {
				gameEnd = true;
			}
			imageTitle = storyList[currentProgress-1][0];//-1 because incremented to check if this should be the last text
			return storyList[currentProgress-1][2];
		}
	}
	
	void FindChoices() {
		buttonTitles.Add(storyList[currentProgress][2]);
		string nextLine = storyList[currentProgress+1][2].ToString();
		if (nextLine[0] == '*') {
			currentProgress++;
			FindChoices();
		}
		else {
			return;
		}
	}
	
	public bool gameEnded() {
		return gameEnd;
	}
	
	
	public string getImageTitle() {
		return imageTitle;
	}
	
	public List<string> getButtonTitles() {
		return buttonTitles;
	}
	
	public void clearButtonTitles() {
		buttonTitles = new List<string>();
	}
	
	public string getAfterFromText(string text) {//given the text on the button, find the next title it points to
		foreach (string[] storyPoint in storyList) {
			if (text == storyPoint[2]) {
				return storyPoint[1];
			}
		}
		return "";
	}
}
