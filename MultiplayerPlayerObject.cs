using UnityEngine;

public class MultiplayerPlayerObject
{
	public BoltEntity character;
	public BoltConnection connection;
	public string playerName;
	public int playerNumber;
	public Color playerColor;
	
	public bool IsServer
	{
		get { return connection == null; }
	}

	public bool IsClient
	{
		get { return connection != null; }
	}

	public void Spawn()
	{
		if(!character)
		{
			// assign the colors/numbers etc for the player.
			var playerToken = new PlayerToken();
			playerToken.playerNumber = playerNumber;
			playerToken.playerColor = playerColor;
			playerToken.playerName = playerName;

			character = BoltNetwork.Instantiate(BoltPrefabs.PlayerMultiplayer, playerToken);

			if(IsServer)
			{
				character.TakeControl();
			} else {
				character.AssignControl(connection);
			}

			// move entity to random spawn position
			character.transform.position = GamemanagerNetwork.instance.RandomSpawnPoint();
			
		}
	}

	// used for disconnection
	public void Kill()
	{
		BoltLog.Info("PlayerObject killed from in its script");
		BoltNetwork.Destroy(character);
	}

}
