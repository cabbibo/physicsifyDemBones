using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsifySkinnedMesh : MonoBehaviour {

  public GameObject bonePrefab;
  public Mesh basicMesh;
  public Material mat;
  //public C

  public SkinnedMeshRenderer skinnedMesh;

  public Transform[] bones;
  public GameObject[] PhysicsSkeleton;


	// Use this for initialization
	void Start () {

    skinnedMesh = GetComponent<SkinnedMeshRenderer>();

    bones = skinnedMesh.bones;
		physicsDemBones();

  }

  void physicsDemBones(){

    PhysicsSkeleton = new GameObject[ bones.Length ];

    for( int i = 0; i < bones.Length; i++ ){
      Transform b = bones[i];

      GameObject bone = Instantiate( bonePrefab , new Vector3(0, 0, 0), Quaternion.identity);
      
      bone.transform.position = b.position;
      bone.transform.rotation = b.rotation;
      bone.transform.localScale = b.localScale * .01f;

      PhysicsSkeleton[i] = bone;
      //bone.transform.parent = b;

      for( int j = 0; j < bones.Length; j++ ){

        if( b.transform.parent == bones[j]){
          bone.GetComponent<SpringJoint>().connectedBody = PhysicsSkeleton[j].GetComponent<Rigidbody>();
        }
      }

      print( b.transform.parent );


//      print( g );


    }
  }
	
	// Update is called once per frame
	void FixedUpdate () {

    for( int i = 0; i < bones.Length; i++ ){
      bones[i].position = PhysicsSkeleton[i].transform.position;
      bones[i].rotation = PhysicsSkeleton[i].transform.rotation;
    }
		
	}
}
