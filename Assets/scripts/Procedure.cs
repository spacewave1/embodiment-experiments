using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Procedure : MonoBehaviour
{
    private static Procedure _instance;

    public static Procedure Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            types.ForEach(type =>
            {
                switch (type)
                {
                    case StateType.JumpingJacks:
                        _sceneStates.Add(new JumpingJackState(jumpingJacks, jumpingJacksInstructionLabel,
                            jumpingScore));
                        break;
                    case StateType.Evade:
                        _sceneStates.Add(new EvadeState(shootingMachines, dodgeTriggers, evadeBagInstructionlabel,
                            evadeScore, AvatarAnimator.GetComponent<BodyCalibrate>().Control));
                        break;
                    case StateType.Punch:
                        _sceneStates.Add(new PunchingState(punchingBag, punchingInstructionlabel, punchScore));
                        break;
                }
            });
        }
    }

    public List<StateType> types;
    public int testref;
    private List<SceneState> _sceneStates = new List<SceneState>();
    public List<SceneState> SceneStates => _sceneStates;

    public Animator AvatarAnimator;
    public GameObject jumpingJacks;
    public GameObject punchingBag;
    public GameObject jumpingJacksInstructionLabel;
    public GameObject punchingInstructionlabel;
    public GameObject evadeBagInstructionlabel;
    public GameObject shootingMachines;
    public GameObject dodgeTriggers;
    public Text jumpingScore;
    public Text evadeScore;
    public Text punchScore;
    public SceneState currentState;
    public GameObject nextButton;
    public CustomPointer CustomPointer;

    public void initiate()
    {
        CustomPointer.enabled = false;
        CustomPointer.GetComponent<LineRenderer>().enabled = false;
        StartCoroutine(startProcedure());
    }

    public void disableState()
    {
        currentState.isActive = false;
    }

    IEnumerator startProcedure()
    {
        for (int i = 0; i < _sceneStates.Count; i++)
        {
            foreach (AnimatorControllerParameter parameter in AvatarAnimator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Trigger)
                    AvatarAnimator.ResetTrigger(parameter.name);
            }

            if (AvatarAnimator.GetComponent<BodyCalibrate>().Pose == Pose.STATIC_POSE)
            {
                AvatarAnimator.SetTrigger("switch_to_idle_stance");
            }
            else
            {
                AvatarAnimator.SetTrigger(_sceneStates[i].animationTriggerName);
            }

            StartCoroutine(_sceneStates[i].enterAnimation());
            currentState = _sceneStates[i];
            while (currentState.isActive)
            {
                yield return null;
            }

            ;
            _sceneStates[i].disableObjects();
        }

        ScoreWriter.Instance.write();
        CustomPointer.enabled = true;
        CustomPointer.GetComponent<LineRenderer>().enabled = true;
        nextButton.SetActive(true);
        yield return null;
    }
}

public enum StateType
{
    JumpingJacks,
    Punch,
    Evade
}

[Serializable]
public class SceneState
{
    public string name;
    public bool isActive = true;
    public Text scoreLabel;
    protected int instructionDisplay = 5;
    protected int instructionKeepSeconds = 1;
    public string animationTriggerName = "";
    public StateType type;

    public virtual IEnumerator enterAnimation()
    {
        throw new NotImplementedException();
    }

    public virtual void disableObjects()
    {
        throw new NotImplementedException();
    }
}

class PunchingState : SceneState
{
    public PunchingState(GameObject punchingBag, GameObject punchingInstructionLabel, Text punchingScoreBoard)
    {
        name = "punchState";
        animationTriggerName = "switch_to_boxing_stance";
        type = StateType.Punch;
        this.punchingBag = punchingBag;
        this.punchingInstructionLabel = punchingInstructionLabel;
        scoreLabel = punchingScoreBoard;
    }

    private GameObject punchingBag;
    private GameObject punchingInstructionLabel;

    public override IEnumerator enterAnimation()
    {
        punchingInstructionLabel.SetActive(true);
        yield return new WaitForSeconds(instructionDisplay);
        punchingBag.SetActive(true);
        yield return new WaitForSeconds(instructionKeepSeconds);
        punchingInstructionLabel.SetActive(false);
        yield return null;
    }

    public override void disableObjects()
    {
        punchingBag.SetActive(false);
    }
}

public class JumpingJackState : SceneState
{
    public JumpingJackState(GameObject jacksTriggers, GameObject jacksInstructionLabel, Text jacksScoreBoard)
    {
        type = StateType.JumpingJacks;
        name = "jumpingJacks";
        animationTriggerName = "switch_to_idle_stance";
        this.jacksTriggers = jacksTriggers;
        this.jacksInstructionLabel = jacksInstructionLabel;
        scoreLabel = jacksScoreBoard;
    }

    private GameObject jacksTriggers;
    private GameObject jacksInstructionLabel;

    public override IEnumerator enterAnimation()
    {
        jacksInstructionLabel.SetActive(true);
        yield return new WaitForSeconds(instructionDisplay);
        jacksTriggers.SetActive(true);
        yield return new WaitForSeconds(instructionKeepSeconds);
        jacksInstructionLabel.SetActive(false);
        yield return null;
    }

    public override void disableObjects()
    {
        jacksTriggers.SetActive(false);
    }
}

class EvadeState : SceneState
{
    public EvadeState(GameObject shootingMachines, GameObject dodgeTriggers, GameObject evadeLabel,
        Text evadeScoreLabel, Control controlMode)
    {
        type = StateType.Evade;
        this.shootingMachines = shootingMachines;
        this.evadeLabel = evadeLabel;
        name = "evadeState";
        animationTriggerName = "switch_to_dodging_stance";
        scoreLabel = evadeScoreLabel;
        this.dodgeTriggers = dodgeTriggers;
        this._controlMode = controlMode;
    }

    private Control _controlMode;
    private GameObject dodgeTriggers;
    private GameObject shootingMachines;
    private GameObject evadeLabel;


    public override IEnumerator enterAnimation()
    {
        evadeLabel.SetActive(true);
        yield return new WaitForSeconds(instructionDisplay);
        shootingMachines.SetActive(true);
        if (_controlMode == Control.TRIGGER_ANIMS) dodgeTriggers.SetActive(true);
        yield return new WaitForSeconds(instructionKeepSeconds);
        evadeLabel.SetActive(false);
        yield return null;
    }

    public override void disableObjects()
    {
        if (dodgeTriggers != null)
        {
            dodgeTriggers.SetActive(false);
        }
        shootingMachines.SetActive(false);
    }
}