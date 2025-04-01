using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private CinemachineBrain playerCamBrain;
    [SerializeField] private CinemachineInputAxisController playerInputAxisController;
    void Start()
    {
        int playercount = GameObject.FindGameObjectsWithTag("Player").Length;
        
        if (playercount == 1)
        {
            playerCam.OutputChannel = OutputChannels.Channel01;
            playerCamBrain.ChannelMask = OutputChannels.Channel01;
            playerInputAxisController.PlayerIndex = playercount;
        }
        else if(playercount == 2)
        {
            playerCam.OutputChannel = OutputChannels.Channel02;
            playerCamBrain.ChannelMask = OutputChannels.Channel02;
            playerInputAxisController.PlayerIndex = playercount;
        }
    }
}
