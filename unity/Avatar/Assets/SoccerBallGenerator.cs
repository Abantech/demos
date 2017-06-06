using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class SoccerBallGenerator : MonoBehaviour
    {
        GameObject soccerBall;
        Vector3 startPos = new Vector3(-.1f, .3f, 0);

        private void Start()
        {
            CreateSoccerBall();
        }

        private void Update()
        {
            if (Vector3.Distance(soccerBall.transform.position, startPos) > 4)
            {
                Destroy(soccerBall);
                CreateSoccerBall();
            }
        }

        private void CreateSoccerBall()
        {
            soccerBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            soccerBall.transform.position = startPos;
            soccerBall.transform.localScale = new Vector3(.25f, .25f, .25f);
            var rigid = soccerBall.AddComponent<Rigidbody>();
            rigid.mass = .3f;
        }
    }
}
