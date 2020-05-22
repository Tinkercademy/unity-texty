using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Animations;

public class TextScript : MonoBehaviour
{
	
	//declaring the GameObjects in your scene/prefabs
	//fill these in in the inspector!!
	public TextAsset gameText;//this should be your .txt file
	public Text displayText;
	public Image displayImage;
	public Button choiceButtonPrefab;
	public Canvas canvas;
	
	//custom created StoryCompiler class, defined in the StoryCompiler script
	//it breaks down the raw text from the txt file
	//it also keeps track of the flow of the story
	//read through the script to understand it
	//commands to use: storyCompiler.Compile(gameText), storyCompiler.ProgressStory(), storyCompiler.gameEnded(), storyCompiler.getImageTitle(), storyCompiler.getButtonTitles()
	private StoryCompiler storyCompiler = new StoryCompiler(); 
	
	//when true, user shouldnt be allowed to progress story
	//when should this be made true?
	private bool awaitingChoice = false;

    // Start is called before the first frame update
    void Start()
    {
		//make storyCompiler compile story
    }
	
    // Update is called once per frame
    void Update() {
		//if user presses space, storyCompiler should progress the story (what other conditions should prevent the player from progressing the story?)
		//ProgressStory() also returns a string - what string is that?
		//can be used like this: 
		//string randomString = storyCompiler.ProgressStory();
		
		//change the text of the displayText
		
		//check for any image that matches the title given by storyCompiler.getImageTitle()
		//set displayImage.color = Color.clear if there is no image, and Color.white if there is an image
		
		//create buttons for any choices the player has
		//use the createChoiceButtons function, the input should be storyCompiler.getButtonTitles()
		//what if getButtonTitles() returns null?
    }
	
	void createChoiceButtons(List<string> buttonTitles) {
		//function to create the buttons for the choices
		for (int i = 0; i<buttonTitles.Count-1; i++) {
			string buttonTitle = buttonTitles[i];
			//create the button, name it 'choice'
			//set the text in the button
			
			//adjusts the buttons y-position based on the number of buttons created so far (hint: use recttransform)
			//Vector2 buttonPosition = choice.GetComponent<RectTransform>().anchoredPosition
			//this might be useful - but read abt what anchoredPosition is!
			
			//Uncomment below when you finish
			//adding listener to button, ie when button is clicked, call the function ChoiceOnClick with the input temp
			//string temp = storyCompiler.getAfterFromText(buttonTitle);
			//choice.onClick.AddListener(delegate {ChoiceOnClick(temp);});
		}
	}
	
	void ChoiceOnClick(string titleToSearchFor) {//when button is clicked, destroy all buttons, progress story based on choice
		//find all buttons with GameObject.FindGameObjectsWithTag(something)
		//run a for loop to destroy all the GameObjects the above function returned
		awaitingChoice = false;
		storyCompiler.clearButtonTitles();
		displayText.text = storyCompiler.ProgressStory(titleToSearchFor);
	}
}


