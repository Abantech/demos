using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class AvatarDirectMapping : HumanoidMapping
{
    public GameObject Head;
    public GameObject Neck;
     
    public GameObject Hips;
    public GameObject Spine;
    public GameObject Chest; 

    public GameObject RightArmShoulder;
    public GameObject RightArmUpper;
    public GameObject RightArmLower;
    public GameObject RightArmHand;

    public GameObject RightLegUpper;
    public GameObject RightLegLower;
    public GameObject RightLegFoot;
    public GameObject RightLegToes;

    public GameObject LeftArmShoulder;
    public GameObject LeftArmUpper;
    public GameObject LeftArmLower;
    public GameObject LeftArmHand;

    public GameObject LeftLegUpper;
    public GameObject LeftLegLower;
    public GameObject LeftLegFoot;
    public GameObject LeftLegToes;

    List<Bone> BoneList = new List<Bone>();

    private void Start()
    {
        
    }

    private void Update()
    {
        this.IsInitialized =
            !(Head == null &&
            //     Neck == null && 
            Hips == null &&
            Spine == null &&
            //     Chest == null && 
            //     RightArmShoulder == null && 
            RightArmUpper == null &&
            RightArmLower == null &&
            RightLegUpper == null &&
            RightLegLower == null &&
            RightLegFoot == null &&
            //     RightLegToes == null && 
            //     LeftArmShoulder == null && 
            LeftArmUpper == null &&
            LeftArmLower == null &&
            LeftLegUpper == null &&
            LeftLegLower == null &&
            LeftLegFoot == null 
            //     LeftLegToes == null
            );

        if (BoneList.Count == 0)
        {
            BoneList.Add(Bone.AssociateBoneJoints(Head, () => HeadPosition, () => NeckPosition));
            BoneList.Add(Bone.AssociateBoneJoints(Neck, () => NeckPosition, () => SpineShoulderPosition));

            BoneList.Add(Bone.AssociateBoneJoints(Chest, () => NeckPosition, () => ChestPosition));
            BoneList.Add(Bone.AssociateBoneJoints(Spine, () => ChestPosition, () => (HipsPosition + SpinePosition) / 2));
            BoneList.Add(Bone.AssociateBoneJoints(Hips, () => (HipsPosition + SpinePosition) / 2, () => (LeftLegUpperPosition + RightLegUpperPosition) / 2));

            BoneList.Add(Bone.AssociateBoneJoints(RightArmShoulder, () => SpineShoulderPosition, () => RightArmShoulderPosition));
            BoneList.Add(Bone.AssociateBoneJoints(RightArmUpper, () => RightArmShoulderPosition, () => RightArmLowerPosition));
            BoneList.Add(Bone.AssociateBoneJoints(RightArmLower, () => RightArmLowerPosition, () => RightArmWristPosition));
            BoneList.Add(Bone.AssociateBoneJoints(RightArmHand, () => RightArmWristPosition, () => RightArmHandTipPosition));

            BoneList.Add(Bone.AssociateBoneJoints(RightLegUpper, () => HipsPosition, () => RightLegUpperPosition));
            BoneList.Add(Bone.AssociateBoneJoints(RightLegLower, () => HipsPosition, () => RightLegLowerPosition));
            BoneList.Add(Bone.AssociateBoneJoints(RightLegFoot, () => RightLegLowerPosition, () => RightLegFootPosition));
            BoneList.Add(Bone.AssociateBoneJoints(RightLegToes, () => RightLegFootPosition, () => RightLegToesPosition));

            BoneList.Add(Bone.AssociateBoneJoints(LeftArmShoulder, () => SpineShoulderPosition, () => LeftArmShoulderPosition));
            BoneList.Add(Bone.AssociateBoneJoints(LeftArmUpper, () => LeftArmShoulderPosition, () => LeftArmLowerPosition));
            BoneList.Add(Bone.AssociateBoneJoints(LeftArmLower, () => LeftArmLowerPosition, () => LeftArmWristPosition));
            BoneList.Add(Bone.AssociateBoneJoints(LeftArmHand, () => LeftArmWristPosition, () => LeftArmHandTipPosition));

            BoneList.Add(Bone.AssociateBoneJoints(LeftLegUpper, () => HipsPosition, () => LeftLegUpperPosition));
            BoneList.Add(Bone.AssociateBoneJoints(LeftLegLower, () => HipsPosition, () => LeftLegLowerPosition));
            BoneList.Add(Bone.AssociateBoneJoints(LeftLegFoot, () => LeftLegLowerPosition, () => LeftLegFootPosition));
            BoneList.Add(Bone.AssociateBoneJoints(LeftLegToes, () => LeftLegFootPosition, () => LeftLegToesPosition));
        }

        //BoneList.ForEach(x => Bone.Update(x));
        foreach(var bone in BoneList)
        {
            Bone.Update(bone);
        }
    }

    public override Vector3 ChestPosition { get; set; }
    public override Vector3 HeadPosition { get; set; }
    public override Vector3 SpineShoulderPosition { get; set; }
    public override Vector3 HipsPosition { get; set; }
    public override Vector3 LeftArmLowerPosition { get; set; }
    public override Vector3 LeftArmShoulderPosition { get; set; }
    public override Vector3 LeftArmUpperPosition { get; set; }
    public override Vector3 LeftArmWristPosition { get; set; }
    public override Vector3 LeftArmHandTipPosition { get; set; }
    public override Vector3 LeftArmHandThumbPosition { get; set; }

    public override Vector3 LeftLegFootPosition { get; set; }
    public override Vector3 LeftLegLowerPosition { get; set; }
    public override Vector3 LeftLegToesPosition { get; set; }
    public override Vector3 LeftLegUpperPosition { get; set; }
    public override Vector3 NeckPosition { get; set; }
    public override Vector3 RightArmLowerPosition { get; set; }
    public override Vector3 RightArmShoulderPosition { get; set; }
    public override Vector3 RightArmUpperPosition { get; set; }
    public override Vector3 RightArmWristPosition { get; set; }

    public override Vector3 RightArmHandTipPosition { get; set; }
    public override Vector3 RightArmHandThumbPosition { get; set; }

    public override Vector3 RightLegFootPosition { get; set; }
    public override Vector3 RightLegLowerPosition { get; set; }
    public override Vector3 RightLegToesPosition { get; set; }
    public override Vector3 RightLegUpperPosition { get; set; }
    public override Vector3 SpinePosition { get; set; }
    /*
    public override Vector3 ChestPosition
    {
        get
        {
            return Chest.transform.position;
        }

        set
        {
            Chest.transform.position = value;
        }
    }

    public override Vector3 HeadPosition
    {
        get
        {
            return Head.transform.position;
        }

        set
        {
            Head.transform.position = value;
        }
    }

    public override Vector3 HipsPosition
    {
        get
        {
            return Hips.transform.position;
        }

        set
        {
            Hips.transform.position = value;
        }
    }

    public override Vector3 LeftArmLowerPosition
    {
        get
        {
            return LeftArmLower.transform.position;
        }

        set
        {
            LeftArmLower.transform.position = value;
        }
    }

    public override Vector3 LeftArmShoulderPosition
    {
        get
        {
            return LeftArmShoulder.transform.position;
        }

        set
        {
            LeftArmShoulder.transform.position = value;
        }
    }

    public override Vector3 LeftArmUpperPosition
    {
        get
        {
            return LeftArmUpper.transform.position;
        }

        set
        {
            LeftArmUpper.transform.position = value;
        }
    }

    public override Vector3 LeftArmWristPosition
    {
        get
        {
            return LeftArmWrist.transform.position;
        }

        set
        {
            LeftArmWrist.transform.position = value;
        }
    }

    public override Vector3 LeftLegFootPosition
    {
        get
        {
            return LeftLegFoot.transform.position;
        }

        set
        {
            LeftLegFoot.transform.position = value;
        }
    }

    public override Vector3 LeftLegLowerPosition
    {
        get
        {
            return LeftLegLower.transform.position;
        }

        set
        {
            LeftLegLower.transform.position = value;
        }
    }

    public override Vector3 LeftLegToesPosition
    {
        get
        {
            return LeftLegToes.transform.position;
        }

        set
        {
            LeftLegToes.transform.position = value;
        }
    }

    public override Vector3 LeftLegUpperPosition
    {
        get
        {
            return LeftLegUpper.transform.position;
        }

        set
        {
            LeftLegUpper.transform.position = value;
        }
    }

    public override Vector3 NeckPosition
    {
        get
        {
            return Neck.transform.position;
        }

        set
        {
            Neck.transform.position = value;
        }
    }

    public override Vector3 RightArmLowerPosition
    {
        get
        {
            return RightArmLower.transform.position;
        }

        set
        {
            RightArmLower.transform.position = value;
        }
    }

    public override Vector3 RightArmShoulderPosition
    {
        get
        {
            return RightArmShoulder.transform.position;
        }

        set
        {
            RightArmShoulder.transform.position = value;
        }
    }

    public override Vector3 RightArmUpperPosition
    {
        get
        {
            return RightArmUpper.transform.position;
        }

        set
        {
            RightArmUpper.transform.position = value;
        }
    }

    public override Vector3 RightArmWristPosition
    {
        get
        {
            return RightArmWrist.transform.position;
        }

        set
        {
            RightArmWrist.transform.position = value;
        }
    }

    public override Vector3 RightLegFootPosition
    {
        get
        {
            return RightLegFoot.transform.position;
        }

        set
        {
            RightLegFoot.transform.position = value;
        }
    }

    public override Vector3 RightLegLowerPosition
    {
        get
        {
            return RightLegLower.transform.position;
        }

        set
        {
            RightLegLower.transform.position = value;
        }
    }

    public override Vector3 RightLegToesPosition
    {
        get
        {
            return RightLegToes.transform.position;
        }

        set
        {
            RightLegToes.transform.position = value;
        }
    }
    
    public override Vector3 RightLegUpperPosition
    {
        get 
        {
            return RightLegUpper.transform.position; 
        }

        set
        {
            RightLegUpper.transform.position = value;
        }
    }

    public override Vector3 SpinePosition
    {
        get
        {
            return Spine.transform.position;
        }

        set
        {
            Spine.transform.position = value;
        }
    }
    */
}
