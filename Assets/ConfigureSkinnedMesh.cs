using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureSkinnedMesh : MonoBehaviour {


	  public GameObject bonePrefab;
	  public GameObject connectionPrefab;
	  public GameObject Base;

	  //public C

	  public GameObject BaseConnection;

	  public SkinnedMeshRenderer skinnedMesh;

	  public Transform[] bones;
	  public List<GameObject> PhysicsSkeleton;


		// Use this for initialization
		void Start () {

	    skinnedMesh = GetComponent<SkinnedMeshRenderer>();

	    bones = skinnedMesh.bones;

	    Transform baseBone = bones[0];
	    
	    Vector3 dir = baseBone.position - Base.transform.position;
	  	Vector3 fPos = new Vector3( 0,0,0);// + dir * .5f;

	  	GameObject connection = Instantiate( connectionPrefab , fPos , Quaternion.identity );

	  	connection.GetComponent<ConfigurableJoint>().connectedBody = Base.GetComponent<Rigidbody>();
	  	connection.GetComponent<ConfigurableJoint>().connectedAnchor = new Vector3(0 ,0, 0 );
	  	connection.GetComponent<ConfigurableJoint>().anchor = new Vector3(0 , 0 , 0 );
	  	connection.GetComponent<Collider>().enabled = false;

	  	BoneLinker bl = baseBone.gameObject.GetComponent<BoneLinker>();

			connection.transform.localScale = new Vector3(0 , dir.magnitude , 0  );

	  	connection.GetComponent<ConnectionLinker>().Length = 1;	 		
	    //bl.Connection = connection;

	    BaseConnection = connection;

	    connection.transform.parent = Base.transform;

	    recurseDemBones( baseBone , connection );


	  }

	  void recurseDemBones( Transform bone , GameObject grandDadBone ){

	  	BoneLinker dadBL = bone.gameObject.GetComponent<BoneLinker>();


			if( bone.childCount > 0 ){

				GameObject connection = CreateConnection( bone.GetChild(0) , bone , grandDadBone );	 
				dadBL.Connection = connection;

				int i = 0;
				foreach (Transform child in bone ){

					//GameObject connection = CreateConnection( bone.GetChild(i) , bone , dadConnection );	
					
					connection.GetComponent<ConnectionLinker>().Kiddies.Add( child );

					

		  		//BoneLinker bl = child.gameObject.AddComponent<BoneLinker>();
		  		
					recurseDemBones( child , connection );

		  	}

			}

	  	
	  	

	  }


	  GameObject CreateConnection( Transform kid , Transform dad, GameObject grandDadBone ){

	  	Vector3 dir = kid.position - dad.position;
	  	Vector3 fPos = dad.position;

	  	//fPos = new Vector3( 0,0,0);
	  	GameObject connection = Instantiate( connectionPrefab , fPos , dad.rotation );

			ConnectionLinker dadCL = grandDadBone.GetComponent<ConnectionLinker>();
			//BoneLinker dadBL = grandDadBone.GetComponent<BoneLinker>();

	  	connection.GetComponent<ConfigurableJoint>().connectedBody = grandDadBone.GetComponent<Rigidbody>();
	  	//connection.GetComponent<ConfigurableJoint>().connectedAnchor = new Vector3(0 , dadCL.Length * .5f, 0 );
	  	//connection.GetComponent<ConfigurableJoint>().anchor = new Vector3(0 , dir.magnitude * -.5f , 0 );

	  	connection.GetComponent<ConfigurableJoint>().connectedAnchor = new Vector3(0 , dadCL.Length, 0 );
	  	connection.GetComponent<ConfigurableJoint>().anchor = new Vector3(0 , 0 , 0 );

	  	connection.transform.localScale = new Vector3( dir.magnitude * .5f , dir.magnitude * .5f , dir.magnitude * .5f );
	 
	 		connection.GetComponent<ConnectionLinker>().Length = dir.magnitude;

	 		Debug.Log( dad );
	 		connection.transform.parent = dad;

	 		return connection;
	  	
	  }

		
		// Update is called once per frame
		void FixedUpdate () {


			/*for( int i = 0; i < bones.Length; i++ ){
				GameObject c = bones[i].GetComponent<BoneLinker>().Connection;

				Vector3 fPos = new Vector3(0, -c.GetComponent<ConnectionLinker>().Length * .5f , 0 );
				bones[i].position = c.transform.position + c.transform.TransformVector(fPos);
				bones[i].rotation = c.transform.rotation;
			}*/

			
		}


	}
