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
	public GameObject loginbtn;

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
			
			Selection_UI.FillBar.value += async.progress / 1999	;
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
		Selection_UI.loginbtn.SetActive(false);
		Selection_UI.LoadingScreen.SetActive(true);
		
		async = SceneManager.LoadSceneAsync(NextScene.ToString());
		yield return async;
		
	}
}
