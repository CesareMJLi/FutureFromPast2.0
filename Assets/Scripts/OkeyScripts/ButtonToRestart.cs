using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonToRestart : MonoBehaviour {


		void Start () {
		}

		void Awake()
		{
			Button button = GetComponent<Button>() as Button;
			button.onClick.AddListener(btClick);
		}


		void Update () {
		}

		void btClick()
		{
				print("Button Click");
			Collectable._piecesCollected = 0;
			SceneManager.LoadScene(ButtonLoadingScripts.levelNum);

		}
}
