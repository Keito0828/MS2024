using Fusion;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    [SerializeField]
    private NetworkRunner networkRunnerPrefab;

    [SerializeField]
    private NetworkPrefabRef playerAvatarPrefab;

    private NetworkRunner networkRunner;

   private async void Start()
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        var result = await networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SceneManager = networkRunner.GetComponent<NetworkSceneManagerDefault>()
        });

        if (result.Ok)
        {
            // �����_���Ȑ����ʒu�i���a5�̉~�̓����j���擾����
            var randomValue = Random.insideUnitCircle * 5f;
            var spawnPosition = new Vector3(randomValue.x, 5f, randomValue.y);
            // �v���C���[���g�̃A�o�^�[�𐶐�����
            networkRunner.Spawn(playerAvatarPrefab, spawnPosition, Quaternion.identity, networkRunner.LocalPlayer);
        }
        else
        {
            Debug.Log("���s�I");            
        }
    }
}