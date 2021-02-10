using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private GameObject player;
	private Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;
    private GameObject camObj;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        tFollowTarget = player.transform;
        vcam.LookAt = tFollowTarget;
        vcam.Follow = tFollowTarget;
    }


    public void setCamera()
    {
        camObj = GameObject.FindWithTag("MainCamera");
        //var freeLook = camObj.GetComponent<CinemachineFreeLook>();
        //var comp = freeLook.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        //comp.m_TrackedObjectOffset.y = .5f;
        vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = .5f;
        vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = .5f;
        vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0f;
        vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = 0f;
        vcam.m_Lens.OrthographicSize = 1.4f;
    }
    public void setCamera2()
    {
        camObj = GameObject.FindWithTag("MainCamera");
        //var freeLook = camObj.GetComponent<CinemachineFreeLook>();
        //var comp = freeLook.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        //comp.m_TrackedObjectOffset.y = .5f;
        vcam.m_Lens.OrthographicSize = 2.5f;
    }
}
