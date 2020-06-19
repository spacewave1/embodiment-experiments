using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JumpingJacksProcedure : MonoBehaviour
{
    public List<ZoneType> order;

    private int nextOrderPointer;

    public AvatarController _controller;
    public List<JumpingJackZone> middle_zones;
    public List<JumpingJackZone> up_zones;
    public List<JumpingJackZone> down_zones;

    private List<JumpingJackZone> allZones = new List<JumpingJackZone>();
    
    public float duranceTaskActive;
    public Animator avatarAnimator;

    private float countTaskActive;

    private void Start()
    {
        allZones.AddRange(middle_zones);
        allZones.AddRange(up_zones);
        allZones.AddRange(down_zones);
        ShowZones(nextOrderPointer);

    }

    public void Update()
    {

        switch (order[nextOrderPointer])
        {
            case ZoneType.UP:
                if (up_zones.All(zone => zone.handsInZone))
                {
                    nextOrderPointer = (nextOrderPointer + 1) % order.Count;
                    if (_controller.GetComponent<BodyCalibrate>().Control == Control.TRIGGER_ANIMS)
                    {
                        avatarAnimator.SetTrigger("jumping_jack_up");
                    };
                    ShowZones(nextOrderPointer);
                    _controller.ProcessJumpingJackCycle();
                }
                break;
            
            case ZoneType.DOWN:
                if (down_zones.All(zone => zone.handsInZone))
                {
                    nextOrderPointer = (nextOrderPointer + 1) % order.Count;
                    if (_controller.GetComponent<BodyCalibrate>().Control == Control.TRIGGER_ANIMS)
                    {
                        avatarAnimator.SetTrigger("jumping_jack_down");
                    };
                    ShowZones(nextOrderPointer);
                }
                break;
            
            case ZoneType.MIDDLE:
                if (middle_zones.All(zone => zone.handsInZone))
                {
                    nextOrderPointer = (nextOrderPointer + 1) % order.Count;
                    ShowZones(nextOrderPointer);
                }
                break;
        }

        countTaskActive += Time.deltaTime;

        if (countTaskActive >= duranceTaskActive)
        {
            Procedure.Instance.disableState();
        }
    }

    private void ShowZones(int count)
    {
        allZones.ForEach(zone =>
        {
            zone.gameObject.SetActive(false);
            zone.handsInZone = false;
        });
        
        switch (order[count])
        {
            case ZoneType.UP:
                up_zones.ForEach(zone => zone.gameObject.SetActive(true));
                break;
            case ZoneType.DOWN:
                down_zones.ForEach(zone => zone.gameObject.SetActive(true));
                break;
            case ZoneType.MIDDLE:
                middle_zones.ForEach(zone => zone.gameObject.SetActive(true));
                break;
        }
    }
}

public enum ZoneType
{
    UP,
    DOWN,
    MIDDLE
}