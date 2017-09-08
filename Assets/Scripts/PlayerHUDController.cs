using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHUDController : NetworkBehaviour {
    public PlayerHUD hudPrefab;
    PlayerHUD hudInstance;

	// Use this for initialization
	void Start () {
		if(!isLocalPlayer)
            return;
        hudInstance = Instantiate(hudPrefab);
	}

    void Prompt(GameObject other)
    {
        hudInstance.promptText.text = other.tag + " " + other.name;
    }

    void NoPrompt()
    {
        hudInstance.promptText.text = "";
    }
}
