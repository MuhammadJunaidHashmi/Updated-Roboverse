using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


[System.Serializable]
public class Selection_Element
{
	public GameObject LoadingScreen;

	public Slider FillBar;
	public TMP_Text value;
	public GameObject titleScreen;
	public GameObject signupScreen;
	public GameObject loginScreen;
	public GameObject passwordForgot;
	public GameObject passwordReset;
	public AudioSource menuAudio;


}


public class GF_SplashScreen : MonoBehaviour {

	[Header("Scene Selection")]
	public Scenes NextScene;

	[Header("Scene Settings")]
	public float WaitTime;

	[Header("UI Elements")]
	public Selection_Element Selection_UI;

	AsyncOperation async = null;

	void Start () {

		Time.timeScale = 1;
		AudioListener.pause = false;

		if (!GameManager.Instance.Initialized) {
			InitializeGame();
		}

		//StartCoroutine (StartGame ());
	}

	void InitializeGame() {
		SaveData.Instance = new SaveData();
		GF_SaveLoad.LoadProgress();
		GameManager.Instance.Initialized = true;
	}
    public void Update()
    {
		if (async != null)
		{
			async.allowSceneActivation = false;
			
			Selection_UI.FillBar.value += async.progress / 500;
			Selection_UI.value.text = (Mathf.Floor(Selection_UI.FillBar.value * 100)) + "%";
			if (Selection_UI.FillBar.value >= (Selection_UI.FillBar.maxValue))
				async.allowSceneActivation = true;
		}
	}
    public void startgame()
    {
		StartCoroutine(StartGame());
	}
	IEnumerator StartGame(){
		yield return new WaitForSeconds(WaitTime);
		Selection_UI.titleScreen.SetActive(false);
		Selection_UI.LoadingScreen.SetActive(true);
		
		async = SceneManager.LoadSceneAsync(NextScene.ToString());
		yield return async;
		
	}

	public void login()
    {
		Selection_UI.titleScreen.SetActive(false);
		Selection_UI.signupScreen.SetActive(false);
		Selection_UI.loginScreen.SetActive(true);
	}

	public void closeLogin()
    {
		Selection_UI.loginScreen.SetActive(false);
		Selection_UI.signupScreen.SetActive(false);
		Selection_UI.titleScreen.SetActive(true);

	}
	public void signup()
	{
		Selection_UI.loginScreen.SetActive(false);
		Selection_UI.signupScreen.SetActive(true);

	}
	public void buttonAudio()
	{
		Selection_UI.menuAudio.Play();

	}
	public void clickForgot()
	{
		Selection_UI.passwordForgot.SetActive(true);
	}
	public void closeForgot()
	{
		Selection_UI.passwordForgot.SetActive(false);
	}
	
	public void closeReset()
	{
		Selection_UI.passwordReset.SetActive(false);
	}

}
