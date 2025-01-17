using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            // 入力方向のベクトルを正規化する
            data.direction.Normalize();
            // 入力方向を移動方向としてそのまま渡す
            characterController.Move(data.direction);

            if (data.buttons.IsSet(NetworkInputButtons.Jump))
            {
                characterController.Jump();
            }
        }
    }
}