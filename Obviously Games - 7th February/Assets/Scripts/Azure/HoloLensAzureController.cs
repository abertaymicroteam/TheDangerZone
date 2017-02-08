using UnityEngine;
using System;
using System.Net;
using System.Collections.Generic;
using RestSharp;
using Pathfinding.Serialization.JsonFx;
using Unity3dAzure.AppServices;
using UnityEngine.UI;
//using Tacticsoft;
//using Prefabs;
using UnityEngine.SceneManagement;

public class HoloLensAzureController : MonoBehaviour//, ITableViewDataSource 
{
	// This script handles the communication with the azure app service for the HoloLens app

	// App URL
	[SerializeField] // Forces unity to serialise this field
	private string APP_URL = "http://dangerzoneabertay.azurewebsites.net";

	// App Service REST Client
	private MobileServiceClient _client;

	// App Service Table defined using a DataModel
	private MobileServiceTable<SpawnFlag> _table;

	// Local spawn flag model for updating the server
	SpawnFlag tree = new SpawnFlag();

	// Tree prefab
	public GameObject AzureTree;

	// Use this for initialization
	void Start () 
	{
		// Create App Service client (Using factory Create method to force 'https' url)
		_client = MobileServiceClient.Create(APP_URL); // new MobileServiceClient(APP_URL);

		// Get App Service 'SpawnFlags' table
		_table = _client.GetTable<SpawnFlag>("SpawnFlags");

		// Initialise local spawnflag model
		tree.name = "AzureTree";
		tree.flag = false;

		// Insert flag to the server table
		Insert(tree);
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.S))
		{ // Update flag in server table
				
			tree.flag = true;
			UpdateFlag (tree);
		}
	}

	public void Lookup(SpawnFlag item)
	{
		_table.Lookup<SpawnFlag>(item.id, OnLookupCompleted);
	}

	private void OnLookupCompleted(IRestResponse<SpawnFlag> response)
	{
		Debug.Log("OnLookupItemCompleted: " + response.Content);
		if (response.StatusCode == HttpStatusCode.OK)
		{
			SpawnFlag item = response.Data;
		}
		else
		{
			ResponseError err = JsonReader.Deserialize<ResponseError>(response.Content);
			Debug.Log("Lookup Error Status:" + response.StatusCode + " Code:" + err.code.ToString() + " " + err.error);
		}
	}

	public void Insert(SpawnFlag item)
	{ // Inserts new spawn flag into Azure table

		if (Validate(item))
		{
			_table.Insert<SpawnFlag>(item, OnInsertCompleted);
		}
	}

	private void OnInsertCompleted(IRestResponse<SpawnFlag> response)
	{
		if (response.StatusCode == HttpStatusCode.Created)
		{
			Debug.Log( "OnInsertItemCompleted: " + response.Data );
			tree = response.Data; // If inserted successfully, azure will return an ID
		}
		else
		{
			Debug.Log("Insert Error Status:" + response.StatusCode + " Uri: "+response.ResponseUri );
		}
	}

	public void Delete(SpawnFlag item)
	{
		// Deletes spawn flag from azure table
		if (Validate(item))
		{
			_table.Delete<SpawnFlag>(item.id, OnDeleteCompleted);
		}

	}

	private void OnDeleteCompleted(IRestResponse<SpawnFlag> response)
	{
		if (response.StatusCode == HttpStatusCode.OK)
		{
			Debug.Log("OnDeleteItemCompleted");
		}
		else
		{
			Debug.Log("Delete Error Status:" + response.StatusCode + " " + response.ErrorMessage + " Uri: " + response.ResponseUri);
		}
	}

	public void UpdateFlag(SpawnFlag item)
	{
		// Updates item in the table
		if (Validate(item))
		{
			_table.Update<SpawnFlag>(item, OnUpdateFlagCompleted);
		}
	}

	private void OnUpdateFlagCompleted(IRestResponse<SpawnFlag> response)
	{
		if (response.StatusCode == HttpStatusCode.OK)
		{
			Debug.Log("OnUpdateItemCompleted: " + response.Content);
		}
		else
		{
			Debug.Log("Update Error Status:" + response.StatusCode + " " + response.ErrorMessage + " Uri: " + response.ResponseUri);
		}
	}

	public void Read()
	{
		_table.Read<SpawnFlag>(OnReadCompleted);
	}

	private void OnReadCompleted(IRestResponse<List<SpawnFlag>> response)
	{
		if (response.StatusCode == HttpStatusCode.OK)
		{
			Debug.Log("OnReadCompleted data: " + response.ResponseUri +" data: "+ response.Content);
			List<SpawnFlag> items = response.Data;
			Debug.Log("Read items count: " + items.Count);
		}
		else
		{
			Debug.Log("Read Error Status:" + response.StatusCode + " Uri: "+response.ResponseUri );
		}
	}

	// Checks if flag is valid before sending
	private bool Validate(SpawnFlag flag)
	{
		bool isUsernameValid = true;//, isScoreValid = true;
		// Validate username
		if (String.IsNullOrEmpty(flag.name))
		{
			isUsernameValid = false;
			Debug.Log("Error, player username required");
		}

		return (isUsernameValid);// && isScoreValid);
	}
}
