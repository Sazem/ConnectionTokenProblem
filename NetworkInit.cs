using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkInit : Bolt.GlobalEventListener {

	// register all needed tokens
	public override void BoltStartBegin()
	{
		BoltNetwork.RegisterTokenClass<PlayerToken>();
		BoltNetwork.RegisterTokenClass<UserToken>();
        BoltNetwork.RegisterTokenClass<BulletToken>();
        BoltNetwork.RegisterTokenClass<DeathPrefabToken>();
        BoltNetwork.RegisterTokenClass<AudioLibraryToken>();
	}
}
