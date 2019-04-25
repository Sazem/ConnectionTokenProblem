using UdpKit;
using UnityEngine;

public class UserToken : Bolt.IProtocolToken
{
	public string name;
	public Color color;
	//public string password;

	// public UserToken()
	// {
	// 	this.name = "Player_" + Random.Range(1, 91520);
	// 	this.color = Color.red;
	// }
	// public UserToken(string name, Color color)
	// {
	// 	this.name = name;
	// 	this.color = color;
	// }

    public void Write(UdpKit.UdpPacket packet)
    {
		packet.WriteColorRGB(color);
		packet.WriteString(name);								
    }

    public void Read(UdpKit.UdpPacket packet)
    {
		name = packet.ReadString();
		color = packet.ReadColorRGB();
    }
}
