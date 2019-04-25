using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiplayerPlayerRegistery  
{
	// keeps a list of all the private void OnPlayerDisconnected(NetworkPlayer player) {
	static List<MultiplayerPlayerObject> players = new List<MultiplayerPlayerObject>();
	
	// create a player for a connection
	// note: connection can be null
	static MultiplayerPlayerObject CreatePlayer(BoltConnection connection, PlayerToken playerData)
	{
		MultiplayerPlayerObject player;

		// create a new player object, assign the connection property
		// of the object to the connection was passed in
		player = new MultiplayerPlayerObject();
		player.connection = connection;
		
		// if we have a connection, assign this player 
		// as the user data for the connection so that we 
		// always have an easy way to get the player object 
		// for a connection
		if (player.connection != null) 
		{
			player.connection.UserData = player;
		}

		if(playerData != null)
		{
			player.playerColor =  playerData.playerColor; //new Color(Random.value, Random.value, Random.value);
			player.playerNumber = players.Count + 1;// lets hope this works.. PlayerNumber = the count of the players. If someone exits middle of the game, we have to keep it in mind.
			player.playerName = playerData.playerName; //"Player_" + players.Count+1;
		} else { // There should never be null playerData.
			player.playerColor = Color.black;
			player.playerNumber = players.Count + 1;
			player.playerName = "player_" + players.Count+1;
		}
		// add to list of all players
		players.Add(player);

		return player;
	}

	// returns the player list cast to
	// an IENUmerable<T> so that we hide the ability
	// to modify the player list from the outside.
	public static IEnumerable<MultiplayerPlayerObject> AllPlayers
	{
		get { return players; }
	}

	// finds the server player by checking the
	// .IsServer property for every player object.
	public static MultiplayerPlayerObject ServerPlayer
	{
		get { return players.First(player => player.IsServer); }
	}

	// utility that creates server player.
	public static MultiplayerPlayerObject CreateServerPlayer()
	{
		return CreatePlayer(null, null);
	}
	// Utility that creates a client player object.
	public static MultiplayerPlayerObject CreateClientPlayer(BoltConnection connection, PlayerToken playerData)
	{
		BoltLog.Info("playerData: " + playerData.playerName + "nro: " + playerData.playerNumber);		
		return CreatePlayer(connection, playerData);
	}

	// utility that you can pass connection and it returns proper player object.
	public static MultiplayerPlayerObject GetMultiplayerPlayer(BoltConnection connection)
	{
		if(connection == null)
		{
			return ServerPlayer;
		}

		return (MultiplayerPlayerObject)connection.UserData;
	}

	public static void RemovePlayerObject(BoltConnection connection)
	{
		// iterate throu all of the players.
		// save the disconnected one to playerObject and remove it after the iteration.
		MultiplayerPlayerObject playerToRemove = null;
		foreach(MultiplayerPlayerObject chunk in players)
		{
			if(chunk.connection == connection)
			{
				playerToRemove = chunk;
				break;
			}
		}
		if(playerToRemove != null)
		{
			playerToRemove.Kill();
			players.Remove(playerToRemove);
		}
		
	} 
}
