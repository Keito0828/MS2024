using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Server : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;

    void Start()
    {
        StartServer();
    }

    void StartServer()
    {
        // �C�ӂ̃|�[�g�ԍ����w�� (��: 7777)
        int port = 7777;
        server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Debug.Log("�T�[�o�[���N�����܂����B");

        server.BeginAcceptTcpClient(new AsyncCallback(OnClientConnected), null);
    }

    void OnClientConnected(IAsyncResult result)
    {
        client = server.EndAcceptTcpClient(result);
        Debug.Log("�N���C�A���g���ڑ����܂����B");
        // �����Ń��b�Z�[�W�̑���M�������s��
        byte[] buffer = new byte[1024];
        client.GetStream().BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnDataReceived), buffer);
    }

    void OnDataReceived(IAsyncResult result)
    {
        byte[] buffer = (byte[])result.AsyncState;
        string message = Encoding.UTF8.GetString(buffer);
        Debug.Log("��M�������b�Z�[�W: " + message);
    }

    void OnApplicationQuit()
    {
        client.Close();
        server.Stop();
    }
}
