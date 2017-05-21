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
    public GameObject RightArmWrist;

    public GameObject RightLegUpper;
    public GameObject RightLegLower;
    public GameObject RightLegFoot;
    public GameObject RightLegToes;

    public GameObject LeftArmShoulder;
    public GameObject LeftArmUpper;
    public GameObject LeftArmLower;
    public GameObject LeftArmWrist;

    public GameObject LeftLegUpper;
    public GameObject LeftLegLower;
    public GameObject LeftLegFoot;
    public GameObject LeftLegToes;

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
            RightArmWrist == null &&
            RightLegUpper == null &&
            RightLegLower == null &&
            RightLegFoot == null &&
            //     RightLegToes == null && 
            //     LeftArmShoulder == null && 
            LeftArmUpper == null &&
            LeftArmLower == null &&
            LeftArmWrist == null &&
            LeftLegUpper == null &&
            LeftLegLower == null &&
            LeftLegFoot == null 
            //     LeftLegToes == null
            );
    }

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
}
