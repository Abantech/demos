using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RightHandTranslationProvider : HandTransformProvider
{
    public override HumanJointType GetHandType()
    {
        return HumanJointType.HandRight;
    }
}
