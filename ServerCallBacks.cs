using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "2019_MultiplayerTestLevel", "2019_Prefabs")]
public class ServerCallBacks : Bolt.GlobalEventListener
{

	private void Awake() {
		BoltLog.Info("ServerCallBack: Server player created on awake call");
		GamemanagerNetwork.Instantiate();
		MultiplayerPlayerRegistery.CreateServerPlayer();
	}

	// public override void ConnectRequest(UdpKit.UdpEndPoint endpoint, Bolt.IProtocolToken token)
	// {
	// 	BoltConsole.Write("Connecting request", Color.red);
	// 	var playerToken = token as PlayerToken;
	// 	if(playerToken != null)
	// 	{
	// 		BoltConsole.Write("Token received", Color.red);
	// 		BoltNetwork.Accept(endpoint, playerToken);
	// 	} else {
	// 		BoltConsole.Write("No tokens", Color.red);
	// 		BoltNetwork.Accept(endpoint);
	// 	}
	// }

	public override void Connected(BoltConnection connection)
	{
		BoltLog.Error("ServerCallBack: Client player are being made, connection: " + connection.ToString());
		PlayerToken playerToken = connection.ConnectToken as PlayerToken;
		//BoltConsole.Write(playerToken.playerName + " ja " + playerToken.playerNumber, Color.magenta);
		MultiplayerPlayerRegistery.CreateClientPlayer(connection, playerToken);
	}

	public override void SceneLoadLocalDone(string scene)
	{
		BoltLog.Info("ServerCallBack: SceneLoadLocalDone - Spawning Server Player");
		MultiplayerPlayerRegistery.ServerPlayer.Spawn();
	}
	public override void SceneLoadRemoteDone(BoltConnection connection)
	{
		BoltLog.Info("ServerCallBack: RemoveSceneLoaded, spawn client. Connection: " + connection.ToString());
		MultiplayerPlayerRegistery.GetMultiplayerPlayer(connection).Spawn();
	}

	public override void Disconnected(BoltConnection connection)
	{
		BoltLog.Info(connection.ToString() + " just disconnected");
		MultiplayerPlayerRegistery.RemovePlayerObject(connection);
	}
}
