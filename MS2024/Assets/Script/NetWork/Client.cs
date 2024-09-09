using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    private TcpClient client;

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        client = new TcpClient();
        // �T�[�o�[�̃��[�J��IP�A�h���X�ƃ|�[�g�ԍ����w�� (��: "192.168.0.10", 7777)
        client.BeginConnect("192.168.125.1", 7777, new AsyncCallback(OnConnected), null);
    }

    void OnConnected(IAsyncResult result)
    {
        if (client.Connected)
        {
            Debug.Log("�T�[�o�[�ɐڑ����܂����B");
            SendData("����ɂ��́A�T�[�o�[�I");
        }
    }

    void SendData(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        client.GetStream().BeginWrite(data, 0, data.Length, null, null);
    }

    void OnApplicationQuit()
    {
        client.Close();
    }
}
