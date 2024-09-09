using System.Net;
using UnityEngine;

public class ShowLocalIPAddress : MonoBehaviour
{
    private void Start()
    {
        // ���[�J���z�X�g��IP�A�h���X���擾
        string localIP = GetLocalIPAddress();
        Debug.Log("���[�J��IP�A�h���X: " + localIP);
    }

    // ���[�J��IP�A�h���X���擾���郁�\�b�h
    private string GetLocalIPAddress()
    {
        string localIP = "";
        foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) // IPv4�A�h���X�݂̂��擾
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
