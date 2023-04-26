using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
public class TestNetwork : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {

        await UnityServices.InitializeAsync();
        
        if(AuthenticationService.Instance.IsSignedIn){
            Debug.Log("already signed in, logging out");
            AuthenticationService.Instance.SignOut();
        }
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log($"Trying to log in a player ...");

            // Use Unity Authentication to log in
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.Log("Player was not signed in successfully; unable to continue without a logged in player");
            }
        }
        
        await RelayManager.Instance.CreateRelay();
    }
}
