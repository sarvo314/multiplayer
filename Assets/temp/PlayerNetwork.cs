using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform spawnedObjectPrefab;
    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData
        {
            _int = 56,
            _bool = true
        }
        , NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    //INetworkSerializable required to make data serializable
    public struct MyCustomData : INetworkSerializable
    {
        public int _int;
        public bool _bool;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            //throw new System.NotImplementedException();
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) =>
        {
            Debug.Log(OwnerClientId + "; random number " + newValue._int + "; " + newValue._bool + ";" + newValue.message);
        };
    }
    void Update()
    {
        Debug.Log(OwnerClientId + "; randomnum " + randomNumber.Value);

        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {

            Instantiate(spawnedObjectPrefab);
            //TestServerRpc(new ServerRpcParams());
            //TestClientRpc(new ClientRpcParams
            //{
            //    Send = new ClientRpcSendParams
            //    {
            //        TargetClientIds = new List<ulong> { 1 }
            //    }
            //}
            //                );
            //randomNumber.Value = new MyCustomData { _int = 10, _bool = false, message = "This is new message" };
        }

        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = 1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = 1f;

        float moveSpeed = 3f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

    }

    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        //Debug.Log("TestServerRPpc" + OwnerClientId + ';' + serverRpcParams.Send.);
        Debug.Log("TestServerRPpc" + OwnerClientId + ';' + serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log("Test Client RPC");
    }
}
