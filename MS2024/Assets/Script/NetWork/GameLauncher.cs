using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    [SerializeField]
    private NetworkRunner networkRunnerPrefab;

    [SerializeField]
    private NetworkPrefabRef playerAvatarPrefab;

    [SerializeField]
    private string ipAddress = "127.0.0.1"; // �ڑ�����T�[�o�[��IP�A�h���X

    [SerializeField]
    private ushort port = 27015; // �ڑ�����|�[�g�ԍ�

    private NetworkRunner networkRunner;

    private async void Start()
    {
        networkRunner = Instantiate(networkRunnerPrefab);

        // LAN�ŃQ�[�����z�X�g�܂��̓N���C�A���g�Ƃ��ĊJ�n����
        var startArgs = new StartGameArgs
        {
            GameMode = GameMode.Host,  // �z�X�g�Ƃ��ĊJ�n�A�N���C�A���g�Ƃ��Đڑ�����ꍇ�� GameMode.Client
            Address = NetAddress.CreateFromIpPort(ipAddress, port),
            SceneManager = networkRunner.GetComponent<NetworkSceneManagerDefault>()
        };

        var result = await networkRunner.StartGame(startArgs);

        if (result.Ok)
        {
            if (networkRunner.IsServer) // �T�[�o�[�̏ꍇ
            {
                // �����_���Ȑ����ʒu�i���a5�̉~�̓����j���擾����
                var randomValue = Random.insideUnitCircle * 5f;
                var spawnPosition = new Vector3(randomValue.x, 5f, randomValue.y);
                // �v���C���[���g�̃A�o�^�[�𐶐�����
                networkRunner.Spawn(playerAvatarPrefab, spawnPosition, Quaternion.identity, networkRunner.LocalPlayer);
            }
        }
        else
        {
            Debug.LogError("�ڑ����s�I");
        }
    }
}
