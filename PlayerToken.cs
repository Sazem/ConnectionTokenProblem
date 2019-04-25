using System.Collections;
using System.Collections.Generic;
using UdpKit;
using UnityEngine;

public class PlayerToken : Bolt.IProtocolToken {

	public string playerName;
	public int playerNumber;
	public Color playerColor;

	public void Write(UdpPacket packet)
	{
		packet.WriteString(playerName);
		packet.WriteInt(playerNumber);
		packet.WriteColorRGB(playerColor);
	}

	public void Read(UdpPacket packet)
	{
		playerName = packet.ReadString();
		playerNumber = packet.ReadInt();
		playerColor = packet.ReadColorRGB();
	}

}
