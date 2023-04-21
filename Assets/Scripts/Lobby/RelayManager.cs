using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Threading.Tasks;
public class RelayManager : MonoBehaviour
{
    public static RelayManager Instance;
    void Awake(){
        if(Instance != null){
            Debug.Log("You have two relay managers");
        }
        else{
            Instance = this;
        }
    }
    public async Task<string> CreateRelay(int playerNumber = 4){
        try{
            Allocation alloc = await RelayService.Instance.CreateAllocationAsync(playerNumber); //num players on relay

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(alloc.AllocationId);

            Debug.Log("Relay created " + joinCode);

            RelayServerData serverData = new RelayServerData(alloc, "dtls");
            //get network manage rto work with relay.
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);
            NetworkManager.Singleton.StartHost();
            return joinCode;
        }
        catch(RelayServiceException e){
            Debug.Log(e);
            return null;
        }
    }
    public async void JoinRelay(string joinCode){
        try{
            Debug.Log("joined relay " + joinCode);
            JoinAllocation alloc = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData serverData = new RelayServerData(alloc, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

            NetworkManager.Singleton.StartClient();
        }catch(RelayServiceException e){
            Debug.Log(e);
        }
    }
}
