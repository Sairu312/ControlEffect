using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubePattern
{
    Check,
    Heso,
    HesoRe,
    H,
    T,
    TRe,
    CubeInCube,
    CubeInCubeRe,
    MiniCubeInCube,
    MiniCubeInCubeRe,
    Uzu,
    UzuRe,
    Tate
}
public struct RubiksMoves
{
    public RotAxis rotAxis;
    public RotDirection rotDirection;
    public RotPosition rotPosition;
}

public struct DfMove
{
    public RubiksMoves XFR;
    public RubiksMoves XBR;
    public RubiksMoves XFC;
    public RubiksMoves XBC;
    public RubiksMoves XFL;
    public RubiksMoves XBL;
    public RubiksMoves YFR;
    public RubiksMoves YBR;
    public RubiksMoves YFC;
    public RubiksMoves YBC;
    public RubiksMoves YFL;
    public RubiksMoves YBL;
    public RubiksMoves ZFR;
    public RubiksMoves ZBR;
    public RubiksMoves ZFC;
    public RubiksMoves ZBC;
    public RubiksMoves ZFL;
    public RubiksMoves ZBL;
}


public class RubiksPattern : MonoBehaviour
{
    public RubiksMove rubiksMove;
    public bool flag = false;
    public CubePattern pattern = CubePattern.Check;
    private int i = 0;
    RubiksMoves[] checkMoves = new RubiksMoves[6];
    RubiksMoves[] hesoMoves = new RubiksMoves[4];
    RubiksMoves[] hesoReMoves = new RubiksMoves[4];
    RubiksMoves[] HMoves = new RubiksMoves[26];
    RubiksMoves[] TMoves = new RubiksMoves[20];
    RubiksMoves[] TReMoves = new RubiksMoves[20];
    RubiksMoves[] cubeInCube = new RubiksMoves[24];
    RubiksMoves[] cubeInCubeRe = new RubiksMoves[24];
    RubiksMoves[] miniCubeInCube = new RubiksMoves[24];
    RubiksMoves[] miniCubeInCubeRe = new RubiksMoves[24];
    RubiksMoves[] uzuMoves = new RubiksMoves[18];
    RubiksMoves[] uzuMovesRe = new RubiksMoves[18];
    RubiksMoves[] tateMoves = new RubiksMoves[20];
    DfMove dfm = new DfMove();


    // Start is called before the first frame update
    void Start()
    {
        rubiksMove = GetComponent<RubiksMove>();
        moveListSet();
        checkSet();
        hesoSet();
        hesoReSet();
        HSet();
        TSet();
        TReSet();
        cubeInCubeSet();
        cubeInCubeReSet();
        miniCubeInCubeSet();
        miniCubeInCubeReSet();
        uzuSet();
        uzuReSet();
        tateSet();
    }

    // Update is called once per frame
    void Update()
    {
        switch(pattern)
        {
            case CubePattern.Check:
                MovePattern(checkMoves);
                break;
            case CubePattern.Heso:
                MovePattern(hesoMoves);
                break;
            case CubePattern.HesoRe:
                MovePattern(hesoReMoves);
                break;
            case CubePattern.H:
                MovePattern(HMoves);
                break;
            case CubePattern.T:
                MovePattern(TMoves);
                break;
            case CubePattern.TRe:
                MovePattern(TReMoves);
                break;
            case CubePattern.CubeInCube:
                MovePattern(cubeInCube);
                break;
            case CubePattern.CubeInCubeRe:
                MovePattern(cubeInCubeRe);
                break;
            case CubePattern.MiniCubeInCube:
                MovePattern(miniCubeInCube);
                break;
            case CubePattern.MiniCubeInCubeRe:
                MovePattern(miniCubeInCubeRe);
                break;
            case CubePattern.Uzu:
                MovePattern(uzuMoves);
                break;
            case CubePattern.UzuRe:
                MovePattern(uzuMovesRe);
                break;
            case CubePattern.Tate:
                MovePattern(tateMoves);
                break;
        }
    }

    RubiksMoves SetMoves(RotAxis rotAxis, RotDirection rotDirection, RotPosition rotPosition)
    {
        RubiksMoves i = new RubiksMoves();
        i.rotAxis = rotAxis;
        i.rotDirection = rotDirection;
        i.rotPosition = rotPosition;
        return i;
    }

    void MovePattern(RubiksMoves[] rubiksMoves)
    {
        if(!flag || rubiksMove.isRotate)return;
        if(i == rubiksMoves.Length)
        {
            flag = false;
            i = 0;
            return;
        }
        rubiksInput(rubiksMoves[i]);
    }

    void rubiksInput(RubiksMoves rubiksMoves)
    {
        rubiksMove.rotationAxis = rubiksMoves.rotAxis;
        rubiksMove.rotationDirection = rubiksMoves.rotDirection;
        rubiksMove.rotationPosition = rubiksMoves.rotPosition;
        rubiksMove.rotFlag = true;
        i += 1;
    }

     void checkSet()
    {
        checkMoves[0] = dfm.XFC;
        checkMoves[1] = dfm.XFC;
        checkMoves[2] = dfm.YFC;
        checkMoves[3] = dfm.YFC;
        checkMoves[4] = dfm.ZFC;
        checkMoves[5] = dfm.ZFC;
    }

    void hesoSet()
    {
        hesoMoves[0] = dfm.XFC;
        hesoMoves[1] = dfm.YFC;
        hesoMoves[2] = dfm.XBC;
        hesoMoves[3] = dfm.YBC;
    }

    void hesoReSet()
    {
        hesoReMoves[0] = dfm.YFC;
        hesoReMoves[1] = dfm.XFC;
        hesoReMoves[2] = dfm.YBC;
        hesoReMoves[3] = dfm.XBC;
    }

    void HSet()
    {
        HMoves[0] = dfm.XFC;
        HMoves[1] = dfm.YFR;
        HMoves[2] = dfm.YFR;
        HMoves[3] = dfm.XFC;
        HMoves[4] = dfm.XFC;
        HMoves[5] = dfm.YBL;
        HMoves[6] = dfm.YBL;
        HMoves[7] = dfm.XFC;
        HMoves[8] = dfm.YFC;
        HMoves[9] = dfm.ZFR;
        HMoves[10] = dfm.ZFR;
        HMoves[11] = dfm.YFC;
        HMoves[12] = dfm.YFC;
        HMoves[13] = dfm.ZBL;
        HMoves[14] = dfm.ZBL;
        HMoves[15] = dfm.YBC;
        HMoves[16] = dfm.ZBC;
        HMoves[17] = dfm.XBL;
        HMoves[18] = dfm.XBL;
        HMoves[19] = dfm.ZBC;
        HMoves[20] = dfm.ZBC;
        HMoves[21] = dfm.XFR;
        HMoves[22] = dfm.XFR;
        HMoves[23] = dfm.ZFC;
        HMoves[24] = dfm.XFC;
        HMoves[25] = dfm.XFC;
    }

    void TSet()
    {
        TMoves[0] = dfm.XBL;
        TMoves[1] = dfm.XBL;
        TMoves[2] = dfm.YFR;
        TMoves[3] = dfm.YFR;
        TMoves[4] = dfm.ZBL;
        TMoves[5] = dfm.ZBL;
        TMoves[6] = dfm.YBL;
        TMoves[7] = dfm.YBL;
        TMoves[8] = dfm.XFR;
        TMoves[9] = dfm.XFR;
        TMoves[10] = dfm.YFR;
        TMoves[11] = dfm.YFR;
        TMoves[12] = dfm.XFR;
        TMoves[13] = dfm.XFR;
        TMoves[14] = dfm.ZBL;
        TMoves[15] = dfm.ZBL;
        TMoves[16] = dfm.XFR;
        TMoves[17] = dfm.XFR;
        TMoves[18] = dfm.YFR;
        TMoves[19] = dfm.YFR;
    }

    void TReSet()
    {
        TReMoves[0] = dfm.YBR;
        TReMoves[1] = dfm.YBR;
        TReMoves[2] = dfm.XBR;
        TReMoves[3] = dfm.XBR;
        TReMoves[4] = dfm.ZFL;
        TReMoves[5] = dfm.ZFL;
        TReMoves[6] = dfm.XBR;
        TReMoves[7] = dfm.XBR;
        TReMoves[8] = dfm.YBR;
        TReMoves[9] = dfm.YBR;
        TReMoves[10] = dfm.XBR;
        TReMoves[11] = dfm.XBR;
        TReMoves[12] = dfm.YFL;
        TReMoves[13] = dfm.YFL;
        TReMoves[14] = dfm.ZFL;
        TReMoves[15] = dfm.ZFL;
        TReMoves[16] = dfm.YBR;
        TReMoves[17] = dfm.YBR;
        TReMoves[18] = dfm.XFL;
        TReMoves[19] = dfm.XFL;
    }

    void cubeInCubeSet()
    {
        cubeInCube[0] = dfm.XFR;
        cubeInCube[1] = dfm.ZBL;
        cubeInCube[2] = dfm.ZBL;
        cubeInCube[3] = dfm.XBR;
        cubeInCube[4] = dfm.YBR;
        cubeInCube[5] = dfm.XFR;
        cubeInCube[6] = dfm.XFR;
        cubeInCube[7] = dfm.YFR;
        cubeInCube[8] = dfm.YFR;
        cubeInCube[9] = dfm.XBR;
        cubeInCube[10] = dfm.YFR;
        cubeInCube[11] = dfm.ZBL;
        cubeInCube[12] = dfm.YBL;
        cubeInCube[13] = dfm.ZFR;
        cubeInCube[14] = dfm.XBR;
        cubeInCube[15] = dfm.ZBR;
        cubeInCube[16] = dfm.YFL;
        cubeInCube[17] = dfm.YFR;
        cubeInCube[18] = dfm.YFR;
        cubeInCube[19] = dfm.ZFL;
        cubeInCube[20] = dfm.XFR;
        cubeInCube[21] = dfm.ZBL;
        cubeInCube[22] = dfm.ZBL;
        cubeInCube[23] = dfm.XBR;
    }

    void cubeInCubeReSet()
    {
        cubeInCubeRe[0] = dfm.XFR;
        cubeInCubeRe[1] = dfm.ZFL;
        cubeInCubeRe[2] = dfm.ZFL;
        cubeInCubeRe[3] = dfm.XBR;
        cubeInCubeRe[4] = dfm.ZBL;
        cubeInCubeRe[5] = dfm.YBR;
        cubeInCubeRe[6] = dfm.YBR;
        cubeInCubeRe[7] = dfm.YBL;
        cubeInCubeRe[8] = dfm.ZFR;
        cubeInCubeRe[9] = dfm.XFR;
        cubeInCubeRe[10] = dfm.ZBR;
        cubeInCubeRe[11] = dfm.YFL;
        cubeInCubeRe[12] = dfm.ZFL;
        cubeInCubeRe[13] = dfm.YBR;
        cubeInCubeRe[14] = dfm.XFR;
        cubeInCubeRe[15] = dfm.YBR;
        cubeInCubeRe[16] = dfm.YBR;
        cubeInCubeRe[17] = dfm.XBR;
        cubeInCubeRe[18] = dfm.XBR;
        cubeInCubeRe[19] = dfm.YFR;
        cubeInCubeRe[20] = dfm.XFR;
        cubeInCubeRe[21] = dfm.ZFL;
        cubeInCubeRe[22] = dfm.ZFL;
        cubeInCubeRe[23] = dfm.XBR;
    }

    void miniCubeInCubeSet()
    {
        miniCubeInCube[0] = dfm.ZFR;
        miniCubeInCube[1] = dfm.ZFR;
        miniCubeInCube[2] = dfm.XBL;
        miniCubeInCube[3] = dfm.YFR;
        miniCubeInCube[4] = dfm.XFL;
        miniCubeInCube[5] = dfm.YFR;
        miniCubeInCube[6] = dfm.XBL;
        miniCubeInCube[7] = dfm.YFR;
        miniCubeInCube[8] = dfm.YFR;
        miniCubeInCube[9] = dfm.XFL;
        miniCubeInCube[10] = dfm.YFR;
        miniCubeInCube[11] = dfm.YFR;
        miniCubeInCube[12] = dfm.XFL;
        miniCubeInCube[13] = dfm.YBR;
        miniCubeInCube[14] = dfm.XBL;
        miniCubeInCube[15] = dfm.YBR;
        miniCubeInCube[16] = dfm.XFL;
        miniCubeInCube[17] = dfm.YFR;
        miniCubeInCube[18] = dfm.YFR;
        miniCubeInCube[19] = dfm.XBL;
        miniCubeInCube[20] = dfm.YFR;
        miniCubeInCube[21] = dfm.YFR;
        miniCubeInCube[22] = dfm.ZFR;
        miniCubeInCube[23] = dfm.ZFR;
    }

    void miniCubeInCubeReSet()
    {
        miniCubeInCubeRe[0] = dfm.ZBR;
        miniCubeInCubeRe[1] = dfm.ZBR;
        miniCubeInCubeRe[2] = dfm.YBR;
        miniCubeInCubeRe[3] = dfm.YBR;
        miniCubeInCubeRe[4] = dfm.XFL;
        miniCubeInCubeRe[5] = dfm.YBR;
        miniCubeInCubeRe[6] = dfm.YBR;
        miniCubeInCubeRe[7] = dfm.XBL;
        miniCubeInCubeRe[8] = dfm.YFR;
        miniCubeInCubeRe[9] = dfm.XFL;
        miniCubeInCubeRe[10] = dfm.YFR;
        miniCubeInCubeRe[11] = dfm.XBL;
        miniCubeInCubeRe[12] = dfm.YBR;
        miniCubeInCubeRe[13] = dfm.YBR;
        miniCubeInCubeRe[14] = dfm.XBL;
        miniCubeInCubeRe[15] = dfm.YBR;
        miniCubeInCubeRe[16] = dfm.YBR;
        miniCubeInCubeRe[17] = dfm.XFL;
        miniCubeInCubeRe[18] = dfm.YBR;
        miniCubeInCubeRe[19] = dfm.XBL;
        miniCubeInCubeRe[20] = dfm.YBR;
        miniCubeInCubeRe[21] = dfm.XFL;
        miniCubeInCubeRe[22] = dfm.ZBR;
        miniCubeInCubeRe[23] = dfm.ZBR;
    }

    void uzuSet()
    {
        uzuMoves[0] = dfm.XFL;
        uzuMoves[1] = dfm.ZBR;
        uzuMoves[2] = dfm.YBL;
        uzuMoves[3] = dfm.YFR;
        uzuMoves[4] = dfm.XFR;
        uzuMoves[5] = dfm.YBR;
        uzuMoves[6] = dfm.XBR;
        uzuMoves[7] = dfm.YBL;
        uzuMoves[8] = dfm.YBL;
        uzuMoves[9] = dfm.XFR;
        uzuMoves[10] = dfm.XFR;
        uzuMoves[11] = dfm.YBL;
        uzuMoves[12] = dfm.XBL;
        uzuMoves[13] = dfm.YFL;
        uzuMoves[14] = dfm.XFL;
        uzuMoves[15] = dfm.XBR;
        uzuMoves[16] = dfm.ZBL;
        uzuMoves[17] = dfm.YFR;
    }

    void uzuReSet()
    {
        uzuMovesRe[0] = dfm.YBR;
        uzuMovesRe[1] = dfm.ZFL;
        uzuMovesRe[2] = dfm.XFR;
        uzuMovesRe[3] = dfm.XBL;
        uzuMovesRe[4] = dfm.YBL;
        uzuMovesRe[5] = dfm.XFL;
        uzuMovesRe[6] = dfm.YFL;
        uzuMovesRe[7] = dfm.XBR;
        uzuMovesRe[8] = dfm.XBR;
        uzuMovesRe[9] = dfm.YFL;
        uzuMovesRe[10] = dfm.YFL;
        uzuMovesRe[11] = dfm.XFR;
        uzuMovesRe[12] = dfm.YFR;
        uzuMovesRe[13] = dfm.XBR;
        uzuMovesRe[14] = dfm.YBR;
        uzuMovesRe[15] = dfm.YFL;
        uzuMovesRe[16] = dfm.ZFR;
        uzuMovesRe[17] = dfm.XBL;
    }

    void tateSet()
    {
        tateMoves[0] = dfm.ZBL;
        tateMoves[1] = dfm.YFR;
        tateMoves[2] = dfm.ZBL;
        tateMoves[3] = dfm.XFR;
        tateMoves[4] = dfm.XBL;
        tateMoves[5] = dfm.XBL;
        tateMoves[6] = dfm.ZFR;
        tateMoves[7] = dfm.YFL;
        tateMoves[8] = dfm.XFR;
        tateMoves[9] = dfm.YBL;
        tateMoves[10] = dfm.YBL;
        tateMoves[11] = dfm.XBL;
        tateMoves[12] = dfm.YFL;
        tateMoves[13] = dfm.ZFR;
        tateMoves[14] = dfm.XFR;
        tateMoves[15] = dfm.XFR;
        tateMoves[16] = dfm.XBL;
        tateMoves[17] = dfm.ZBL;
        tateMoves[18] = dfm.YFR;
        tateMoves[19] = dfm.ZBL;
    }

    void moveListSet()
    {
        dfm.XFR = SetMoves(RotAxis.X, RotDirection.Forward, RotPosition.Right);
        dfm.XBR = SetMoves(RotAxis.X, RotDirection.Back, RotPosition.Right);
        dfm.XFC = SetMoves(RotAxis.X, RotDirection.Forward, RotPosition.Center);
        dfm.XBC = SetMoves(RotAxis.X, RotDirection.Back, RotPosition.Center);
        dfm.XFL = SetMoves(RotAxis.X, RotDirection.Forward, RotPosition.Left);
        dfm.XBL = SetMoves(RotAxis.X, RotDirection.Back, RotPosition.Left);
        dfm.YFR = SetMoves(RotAxis.Y, RotDirection.Forward, RotPosition.Right);
        dfm.YBR = SetMoves(RotAxis.Y, RotDirection.Back, RotPosition.Right);
        dfm.YFC = SetMoves(RotAxis.Y, RotDirection.Forward, RotPosition.Center);
        dfm.YBC = SetMoves(RotAxis.Y, RotDirection.Back, RotPosition.Center);
        dfm.YFL = SetMoves(RotAxis.Y, RotDirection.Forward, RotPosition.Left);
        dfm.YBL = SetMoves(RotAxis.Y, RotDirection.Back, RotPosition.Left);
        dfm.ZFR = SetMoves(RotAxis.Z, RotDirection.Forward, RotPosition.Right);
        dfm.ZBR = SetMoves(RotAxis.Z, RotDirection.Back, RotPosition.Right);
        dfm.ZFC = SetMoves(RotAxis.Z, RotDirection.Forward, RotPosition.Center);
        dfm.ZBC = SetMoves(RotAxis.Z, RotDirection.Back, RotPosition.Center);
        dfm.ZFL = SetMoves(RotAxis.Z, RotDirection.Forward, RotPosition.Left);
        dfm.ZBL = SetMoves(RotAxis.Z, RotDirection.Back, RotPosition.Left);
    }
}
