using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class particlePositioner : MonoBehaviour {

    private ParticleSystem particles;
    private ParticleSystem.MainModule pMain;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        pMain = particles.main;
    }

    // Update is called once per frame
    void Update () {
        if (!MLHands.IsStarted)
        {            
            return;
        }

        HandPostParticleEffects(MLHands.Right);
        HandPostParticleEffects(MLHands.Left);
    }

    private void HandPostParticleEffects(MLHand hand)
    {
        switch (hand.KeyPose)
        {
            case MLHandKeyPose.Fist:
                transform.position = hand.Center;
                break;
            case MLHandKeyPose.Ok:
                pMain.gravityModifier = ToggleGravity(pMain.gravityModifier.constant, hand);
                break;
        }
    }

    float ToggleGravity(float curGravity, MLHand hand)
    {
        if(hand.GetKeyPoseDown(MLHandKeyPose.Ok))
            return curGravity > 0 ? 0 : 0.1f;
        return curGravity;
    }
}
