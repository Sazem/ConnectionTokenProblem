using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UdpKit;
using System;

public class MultiplayerMenu : Bolt.GlobalEventListener
{
	public String playerName;
	public Color playerColor;

	private void Start() {
		if(playerName == "")
		{
			playerName = "Player_" + UnityEngine.Random.Range(1, 12516);
		}
	}

	public void StartServer() 
	{
		BoltLauncher.StartServer();
	}

	public void StartClient() 
	{
		BoltLauncher.StartClient();
	}

	public override void BoltStartDone()
	{
		if(BoltNetwork.IsServer)
		{
			string matchName = Guid.NewGuid().ToString();

			BoltNetwork.SetServerInfo(matchName, null);
			// lets load the level here, with the name of the level
			BoltNetwork.LoadScene("2019_MultiplayerTestLevel");
		}
	}

	public override void SessionListUpdated(Map<System.Guid, UdpSession> sessionList)
	{
		Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);
		foreach(var session in sessionList)
		{
			UdpSession photonSession = session.Value as UdpSession;
			if(photonSession.Source == UdpSessionSource.Photon)
			{
				PlayerToken userData = new PlayerToken();
				userData.playerName =  playerName;
				userData.playerColor = playerColor;
				//userData.password = "Perkele";
				BoltNetwork.Connect(photonSession, userData);
			}

		}
	}
}
