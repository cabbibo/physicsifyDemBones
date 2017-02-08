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


	// Use this for initialization
	void Start () {

    skinnedMesh = GetComponent<SkinnedMeshRenderer>();

    bones = skinnedMesh.bones;
		physicsDemBones();

  }

  void physicsDemBones(){
    for( int i = 0; i < bones.Length; i++ ){
      Transform b = bones[i];

      GameObject bone = Instantiate( bonePrefab , new Vector3(0, 0, 0), Quaternion.identity);
      
      bone.transform.position = b.position;
      bone.transform.rotation = b.rotation;
      bone.transform.localScale = b.localScale;
      bone.transform.parent = b;


//      print( g );


    }
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
