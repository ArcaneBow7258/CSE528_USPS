using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Threading.Tasks;
using UnityEngine.Events;
public class RelayManager : MonoBehaviour
{
    public static RelayManager Instance;
    public UnityEvent e_relayDone;
    public bool connected = false;
    void Awake(){
        if(Instance != null){
            Debug.Log("You have two relay managers");
        }
        else{
            Instance = this;
        }
        e_relayDone.AddListener(delegate{connected = !connected;});
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
            e_relayDone.Invoke();
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
            e_relayDone.Invoke();
        }catch(RelayServiceException e){
            Debug.Log(e);
        }
    }
}
