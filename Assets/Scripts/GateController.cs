using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    [SerializeField] private TMP_Text gateNumberText = null;
    [SerializeField] private enum GateType
    {
        PositiveGate,
        NegativeGate
    }

    [SerializeField] private GateType gateType;
    [SerializeField] private int gateNumber;
    void Start()
    {
        RandomGateNumber();
    }

    public int GetGateNumber()
    {
        return gateNumber;
    }

    void RandomGateNumber()
    {
        switch (gateType)
        {
            case GateType.PositiveGate:
                gateNumber =  Random.Range(1,10);
                gateNumberText.text = gateNumber.ToString();
                break;
            case GateType.NegativeGate:
                gateNumber = Random.Range(-10,-1);
                gateNumberText.text = gateNumber.ToString();
                break ;
        }
    }
}
