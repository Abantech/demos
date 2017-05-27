using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LeftHandTranslationProvider : HandTransformProvider
{
    public override HumanJointType GetHandType()
    {
        return HumanJointType.HandLeft;
    }
}
