﻿using Assets.Scripts.Common;
using System;
using Memoria;
using UnityEngine;

#pragma warning disable 169
#pragma warning disable 414
#pragma warning disable 649

// ReSharper disable ArrangeThisQualifier
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable StringCompareToIsCultureSpecific
// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnassignedField.Global
// ReSharper disable NotAccessedField.Global
// ReSharper disable NotAccessedField.Local
// ReSharper disable ConvertToConstant.Global
// ReSharper disable ConvertToConstant.Local
// ReSharper disable UnusedParameter.Global
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
// ReSharper disable InconsistentNaming

[ExportedType("¯AÅ+&!!!ÌĴ³Ĩ5ñĒĜëYĨuÕķľÈ*!!!F;a¬uø$KĘær³ê*ZRäī²êÑr÷ôđ/ÕNĦÿ3t@!!!Ó¥ēëı9ñÿL#ĥã¶ÈĂUn¥4ßÏ¨ĭQ´Ăyk@KZÀĂĳ.Ĳġ©ªĵeŃÄJĳĜ@»ńĐh%RzĂôõÅAıvÏļåüďgh4Ø·ĔÆĳOısS+Ûĳ«ÄoÆêĆªbĬ'Ġ=´båĨÜĳ[ÏRĸā·WĆĪĦV,ÓÂâėkì@ĮĂńńńń")]
public class UIKeyTrigger : MonoBehaviour
{
    private Control keyCommand;
    private Control lazyKeyCommand;
    private bool isLockLazyInput;
    private float disableMouseCounter;
    private bool firstTimeInput;
    private float fastEventCounter;
    private bool triggleEventDialog;
    private bool quitConfirm;

    private bool AltKey
    {
        get
        {
            if (!UnityXInput.Input.GetKey(KeyCode.LeftAlt))
                return UnityXInput.Input.GetKey(KeyCode.RightAlt);
            return true;
        }
    }

    private bool AltKeyDown
    {
        get
        {
            if (!UnityXInput.Input.GetKeyDown(KeyCode.LeftAlt))
                return UnityXInput.Input.GetKeyDown(KeyCode.RightAlt);
            return true;
        }
    }

    private bool F2Key => UnityXInput.Input.GetKey(KeyCode.F2);
    private bool F4Key => UnityXInput.Input.GetKey(KeyCode.F4);
    private bool F5Key => UnityXInput.Input.GetKey(KeyCode.F5);
    private bool F9Key => UnityXInput.Input.GetKey(KeyCode.F9);
    private bool F2KeyDown => UnityXInput.Input.GetKeyDown(KeyCode.F2);
    private bool F4KeyDown => UnityXInput.Input.GetKeyDown(KeyCode.F4);
    private bool F5KeyDown => UnityXInput.Input.GetKeyDown(KeyCode.F5);
    private bool F9KeyDown => UnityXInput.Input.GetKeyDown(KeyCode.F9);

    public UIKeyTrigger()
    {
        keyCommand = Control.None;
        lazyKeyCommand = Control.None;
    }

    public static bool IsOnlyTouchAndLeftClick()
    {
        if (UICamera.currentTouchID > -2)
            return UICamera.currentTouchID < 2;
        return false;
    }

    public static bool IsNeedToRemap()
    {
        return Application.platform == RuntimePlatform.Android && PersistenSingleton<UIManager>.Instance.Dialogs.IsDialogNeedControl() && (PersistenSingleton<UIManager>.Instance.Dialogs.GetChoiceDialog() == null && EventHUD.CurrentHUD == MinigameHUD.None);
    }

    public void ResetTriggerEvent()
    {
        triggleEventDialog = false;
    }

    public bool GetKey(Control key)
    {
        if (PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.FieldHUD && PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.WorldHUD && (PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.BattleHUD && PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.QuadMistBattle) && PersistenSingleton<UIManager>.Instance.UnityScene != UIManager.Scene.EndGame)
            return false;
        if (triggleEventDialog && key == Control.Confirm)
        {
            triggleEventDialog = false;
            return true;
        }
        return (key != Control.Cancel || !PersistenSingleton<UIManager>.Instance.Dialogs.GetChoiceDialog()) &&
               (PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.WorldHUD ||
                !PersistenSingleton<UIManager>.Instance.Booster.IsSliderActive && (key != Control.Confirm || !UnityXInput.Input.GetMouseButtonDown(0) || !(UICamera.selectedObject == UIManager.World.RotationLockButtonGameObject) && !(UICamera.selectedObject == UIManager.World.PerspectiveButtonGameObject)) && (key != Control.Confirm || !UnityXInput.Input.GetMouseButtonDown(0) || !(UICamera.selectedObject == gameObject))) &&
               (PersistenSingleton<HonoInputManager>.Instance.IsInput((int)key) || lazyKeyCommand == key || key == Control.Confirm && UnityXInput.Input.GetMouseButtonDown(0) && (EventHUD.CurrentHUD == MinigameHUD.None && PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.EndGame) && (PersistenSingleton<UIManager>.Instance.Dialogs.GetChoiceDialog() == null && lazyKeyCommand == Control.None));
    }

    public bool GetKeyTrigger(Control key)
    {
        if (PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.FieldHUD && PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.WorldHUD && (PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.BattleHUD && PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.QuadMistBattle) && PersistenSingleton<UIManager>.Instance.UnityScene != UIManager.Scene.EndGame ||
            (key == Control.Cancel && PersistenSingleton<UIManager>.Instance.Dialogs.GetChoiceDialog() || PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.WorldHUD && ff9.m_GetIDEvent(ff9.w_moveCHRStatus[ff9.w_moveActorPtr.originalActor.index].id) != 0 && (key == Control.LeftBumper || key == Control.RightBumper)))
            return false;
        if (UnityXInput.Input.GetMouseButtonDown(0) || UnityXInput.Input.GetMouseButtonDown(1) || UnityXInput.Input.GetMouseButtonDown(2))
        {
            if (key != Control.Left && key != Control.Right && (key != Control.Up && key != Control.Down) && PersistenSingleton<HonoInputManager>.Instance.IsInputUp((int)key))
                return true;
        }
        else if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown((int)key))
            return true;
        if (lazyKeyCommand != key)
            return false;
        ResetKeyCode();
        return true;
    }

    private void Update()
    {
        GameLoopManager.RaiseUpdateEvent();

        if (UnityXInput.Input.GetAxis("Mouse X") < 1.0 / 1000.0 && UnityXInput.Input.GetAxis("Mouse Y") < 1.0 / 1000.0)
        {
            disableMouseCounter += Time.deltaTime;
        }
        else
        {
            disableMouseCounter = 0.0f;
            if (!UICamera.list[0].useMouse)
                UICamera.list[0].useMouse = true;
        }
        if (UnityXInput.Input.GetMouseButton(0) || UnityXInput.Input.GetMouseButton(1) || (UnityXInput.Input.GetMouseButton(2) || Mathf.Abs(UnityXInput.Input.GetAxis("Mouse ScrollWheel")) > 0.00999999977648258))
        {
            disableMouseCounter = 0.0f;
            if (!UICamera.list[0].useMouse)
                UICamera.list[0].useMouse = true;
        }
        if (disableMouseCounter > 1.0 && UICamera.list[0].useMouse)
            UICamera.list[0].useMouse = false;
        if (!UnityXInput.Input.anyKey && !isLockLazyInput)
            ResetKeyCode();
        AccelerateKeyNavigation();
        if (handleMenuControlKeyPressCustomInput())
            return;
        HandleBoosterButton();
        handleDialogControlKeyPressCustomInput();
    }

    private void AccelerateKeyNavigation()
    {
        if (!UnityXInput.Input.anyKey && PersistenSingleton<HonoInputManager>.Instance.GetHorizontalNavigation() <= (double)HonoInputManager.AnalogThreadhold && (PersistenSingleton<HonoInputManager>.Instance.GetHorizontalNavigation() >= -(double)HonoInputManager.AnalogThreadhold && PersistenSingleton<HonoInputManager>.Instance.GetVerticalNavigation() <= (double)HonoInputManager.AnalogThreadhold) && PersistenSingleton<HonoInputManager>.Instance.GetVerticalNavigation() >= -(double)HonoInputManager.AnalogThreadhold)
        {
            if (!firstTimeInput)
                UICamera.EventWaitTime = 0.175f;
            fastEventCounter = RealTime.time;
            firstTimeInput = true;
        }
        else
        {
            if (firstTimeInput)
            {
                fastEventCounter = RealTime.time;
                firstTimeInput = false;
            }
            if (RealTime.time - (double)fastEventCounter <= 0.300000011920929)
                return;
            UICamera.EventWaitTime = 0.1f;
        }
    }

    public void HandleBoosterButton(BoosterType triggerType = BoosterType.None)
    {
        if (!Configuration.Cheats.Enabled)
            return;
        if (PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.Title || PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.PreEnding || (PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.Ending || !MBG.IsNull && !MBG.Instance.IsFinished()))
            return;
        if (UnityXInput.Input.GetKeyDown(KeyCode.F1) || triggerType == BoosterType.HighSpeedMode)
        {
            if (!Configuration.Cheats.SpeedMode)
            {
                Log.Message("[Cheats] SpeedMode was disabled.");
                FF9Sfx.FF9SFX_Play(102);
                return;
            }

            bool flag = !FF9StateSystem.Settings.IsBoosterButtonActive[1];
            FF9StateSystem.Settings.CallBoosterButtonFuntion(BoosterType.HighSpeedMode, flag);
            PersistenSingleton<UIManager>.Instance.Booster.SetBoosterHudIcon(BoosterType.HighSpeedMode, flag);
            PersistenSingleton<UIManager>.Instance.Booster.SetBoosterButton(BoosterType.HighSpeedMode, flag);
        }
        if (UnityXInput.Input.GetKeyDown(KeyCode.F2) || triggerType == BoosterType.BattleAssistance)
        {
            if (!Configuration.Cheats.BattleAssistance)
            {
                Log.Message("[Cheats] BattleAssistance was disabled.");
                FF9Sfx.FF9SFX_Play(102);
                return;
            }

            if ((FF9StateSystem.Battle.isNoBoosterMap() || FF9StateSystem.Battle.FF9Battle.btl_escape_fade != 32) && SceneDirector.IsBattleScene())
                return;
            bool flag = !FF9StateSystem.Settings.IsBoosterButtonActive[0];
            FF9StateSystem.Settings.CallBoosterButtonFuntion(BoosterType.BattleAssistance, flag);
            PersistenSingleton<UIManager>.Instance.Booster.SetBoosterHudIcon(BoosterType.BattleAssistance, flag);
            PersistenSingleton<UIManager>.Instance.Booster.SetBoosterButton(BoosterType.BattleAssistance, flag);
        }
        if (UnityXInput.Input.GetKeyDown(KeyCode.F3) || triggerType == BoosterType.Attack9999)
        {
            if (!Configuration.Cheats.Attack9999)
            {
                Log.Message("[Cheats] Attack9999 was disabled.");
                FF9Sfx.FF9SFX_Play(102);
                return;
            }

            bool flag = !FF9StateSystem.Settings.IsBoosterButtonActive[3];
            FF9StateSystem.Settings.CallBoosterButtonFuntion(BoosterType.Attack9999, flag);
            PersistenSingleton<UIManager>.Instance.Booster.SetBoosterHudIcon(BoosterType.Attack9999, flag);
            PersistenSingleton<UIManager>.Instance.Booster.SetBoosterButton(BoosterType.Attack9999, flag);
        }
        if (UnityXInput.Input.GetKeyDown(KeyCode.F4) || triggerType == BoosterType.NoRandomEncounter)
        {
            if (!Configuration.Cheats.NoRandomEncounter)
            {
                Log.Message("[Cheats] NoRandomEncounter was disabled.");
                FF9Sfx.FF9SFX_Play(102);
                return;
            }

            if (PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.FieldHUD && PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.WorldHUD && PersistenSingleton<UIManager>.Instance.State != UIManager.UIState.Pause)
                return;
            bool flag = !FF9StateSystem.Settings.IsBoosterButtonActive[4];
            FF9StateSystem.Settings.CallBoosterButtonFuntion(BoosterType.NoRandomEncounter, flag);
            PersistenSingleton<UIManager>.Instance.Booster.SetBoosterHudIcon(BoosterType.NoRandomEncounter, flag);
            PersistenSingleton<UIManager>.Instance.Booster.SetBoosterButton(BoosterType.NoRandomEncounter, flag);
        }
        if (UnityXInput.Input.GetKeyDown(KeyCode.F5) && (PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.FieldHUD || PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.WorldHUD || PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.Pause))
        {
            if (!Configuration.Cheats.MasterSkill)
            {
                Log.Message("[Cheats] MasterSkill was disabled.");
                FF9Sfx.FF9SFX_Play(102);
                return;
            }

            if (!FF9StateSystem.Settings.IsMasterSkill)
            {
                PersistenSingleton<UIManager>.Instance.Booster.ShowWaringDialog(BoosterType.MasterSkill);
            }
            else
            {
                FF9StateSystem.Settings.CallBoosterButtonFuntion(BoosterType.MasterSkill, false);
                PersistenSingleton<UIManager>.Instance.Booster.SetBoosterHudIcon(BoosterType.MasterSkill, false);
            }
        }
        if (UnityXInput.Input.GetKeyDown(KeyCode.F6) && (PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.FieldHUD || PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.WorldHUD || PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.Pause))
        {
            if (!Configuration.Cheats.LvMax)
            {
                Log.Message("[Cheats] LvMax was disabled.");
                FF9Sfx.FF9SFX_Play(102);
                return;
            }

            PersistenSingleton<UIManager>.Instance.Booster.ShowWaringDialog(BoosterType.LvMax);
        }
        if (UnityXInput.Input.GetKeyDown(KeyCode.F7) && (PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.FieldHUD || PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.WorldHUD || PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.Pause))
        {
            if (!Configuration.Cheats.GilMax)
            {
                Log.Message("[Cheats] GilMax was disabled.");
                FF9Sfx.FF9SFX_Play(102);
                return;
            }

            PersistenSingleton<UIManager>.Instance.Booster.ShowWaringDialog(BoosterType.GilMax);
        }
    }

    private void OnApplicationQuit()
    {
        if (PersistenSingleton<UIManager>.Instance.UnityScene != UIManager.Scene.Bundle && !quitConfirm)
        {
            Application.CancelQuit();
            OnQuitCommandDetected();
        }
        else
        {
            GameLoopManager.RaiseQuitEvent();
        }
    }

    public void ConfirmQuit()
    {
        quitConfirm = true;
        BroadcastAll("OnQuit");
        Application.Quit();
    }

    public void OnQuitCommandDetected(UIScene scene)
    {
        if (PersistenSingleton<UIManager>.Instance.IsLoading || PersistenSingleton<UIManager>.Instance.QuitScene.isShowQuitUI)
            return;

        PersistenSingleton<UIManager>.Instance.QuitScene.SetPreviousActiveGroup();
        if (scene != null)
        {
            quitConfirm = false;
            scene.OnKeyQuit();
        }
        else
        {
            PersistenSingleton<UIManager>.Instance.QuitScene.Show(null);
        }
    }

    private void OnPartySceneCommandDetected(UIScene scene)
    {
        UIManager uiManager = PersistenSingleton<UIManager>.Instance;
        if (uiManager.IsLoading || uiManager.QuitScene.isShowQuitUI || uiManager.State == UIManager.UIState.Serialize)
        {
            FF9Sfx.FF9SFX_Play(102);
            return;
        }

        if (!uiManager.IsMenuControlEnable)
        {
            FF9Sfx.FF9SFX_Play(102);
            return;
        }

        switch (PersistenSingleton<UIManager>.Instance.State)
        {
            case UIManager.UIState.FieldHUD:
            case UIManager.UIState.WorldHUD:
                break;
            default:
                FF9Sfx.FF9SFX_Play(102);
                return;
        }

        FF9Sfx.FF9SFX_Play(103);
        scene?.Hide(UISceneHelper.OpenPartyMenu);
    }

    private static void OnSaveLoadSceneCommandDetected(UIScene scene, SaveLoadUI.SerializeType type)
    {
        UIManager uiManager = PersistenSingleton<UIManager>.Instance;
        if (uiManager.IsLoading || uiManager.QuitScene.isShowQuitUI || uiManager.State == UIManager.UIState.Serialize)
        {
            FF9Sfx.FF9SFX_Play(102);
            return;
        }

        if (!uiManager.IsMenuControlEnable)
        {
            FF9Sfx.FF9SFX_Play(102);
            return;
        }

        switch (type)
        {
            case SaveLoadUI.SerializeType.Save:
                TryShowSaveScene(scene);
                break;

            case SaveLoadUI.SerializeType.Load:
                TryShowLoadScene(scene);
                break;
        }
    }

    private static void TryShowSaveScene(UIScene scene)
    {
        switch (PersistenSingleton<UIManager>.Instance.State)
        {
            case UIManager.UIState.FieldHUD:
            case UIManager.UIState.WorldHUD:
                break;
            default:
                FF9Sfx.FF9SFX_Play(102);
                return;
        }

        FF9Sfx.FF9SFX_Play(103);
        scene?.Hide(OnSaveGameButtonClick);
    }

    private static void TryShowLoadScene(UIScene scene)
    {
        FF9Sfx.FF9SFX_Play(103);
        scene?.Hide(OnLoadGameButtonClick);
    }

    private static void OnSaveGameButtonClick()
    {
        PersistenSingleton<UIManager>.Instance.SaveLoadScene.Type = SaveLoadUI.SerializeType.Save;
        PersistenSingleton<UIManager>.Instance.ChangeUIState(UIManager.UIState.Serialize);
    }

    private static void OnLoadGameButtonClick()
    {
        PersistenSingleton<UIManager>.Instance.SaveLoadScene.Type = SaveLoadUI.SerializeType.Load;
        PersistenSingleton<UIManager>.Instance.ChangeUIState(UIManager.UIState.Serialize);
    }

    public void OnQuitCommandDetected()
    {
        OnQuitCommandDetected(PersistenSingleton<UIManager>.Instance.GetSceneFromState(PersistenSingleton<UIManager>.Instance.State));
    }

    private static void BroadcastAll(string method)
    {
        foreach (GameObject gameObject in (GameObject[])FindObjectsOfType(typeof(GameObject)))
        {
            if (gameObject && gameObject.transform.parent == null)
                gameObject.gameObject.BroadcastMessage(method, SendMessageOptions.DontRequireReceiver);
        }
    }

    private bool handleMenuControlKeyPressCustomInput(GameObject activeButton = null)
    {
        UIScene sceneFromState = PersistenSingleton<UIManager>.Instance.GetSceneFromState(PersistenSingleton<UIManager>.Instance.State);
        if (ButtonGroupState.ActiveButton && ButtonGroupState.ActiveButton != PersistenSingleton<UIManager>.Instance.gameObject)
            activeButton = ButtonGroupState.ActiveButton;
        else if (activeButton == null)
            activeButton = UICamera.selectedObject;
        if (sceneFromState != null && (!PersistenSingleton<UIManager>.Instance.Dialogs.Activate || PersistenSingleton<UIManager>.Instance.IsPause))
        {
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(1) || keyCommand == Control.Cancel)
            {
                keyCommand = Control.None;
                sceneFromState.OnKeyCancel(activeButton);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(0) || keyCommand == Control.Confirm)
            {
                keyCommand = Control.None;
                sceneFromState.OnKeyConfirm(activeButton);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(8) || keyCommand == Control.Pause)
            {
                keyCommand = Control.None;
                if (PersistenSingleton<UIManager>.Instance.IsPauseControlEnable)
                    sceneFromState.OnKeyPause(activeButton);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(9) || keyCommand == Control.Select)
            {
                keyCommand = Control.None;
                sceneFromState.OnKeySelect(UICamera.selectedObject);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(2) || keyCommand == Control.Menu)
            {
                keyCommand = Control.None;
                if (FF9StateSystem.AndroidTVPlatform && FF9StateSystem.EnableAndroidTVJoystickMode && (PersistenSingleton<HonoInputManager>.Instance.GetSource(Control.Menu) == SourceControl.Joystick && PersistenSingleton<UIManager>.Instance.State == UIManager.UIState.Pause))
                    sceneFromState.OnKeyMenu(activeButton);
                else if (PersistenSingleton<UIManager>.Instance.IsMenuControlEnable)
                    sceneFromState.OnKeyMenu(activeButton);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(3) || keyCommand == Control.Special)
            {
                keyCommand = Control.None;
                sceneFromState.OnKeySpecial(activeButton);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(4) || keyCommand == Control.LeftBumper)
            {
                keyCommand = Control.None;
                sceneFromState.OnKeyLeftBumper(activeButton);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(5) || keyCommand == Control.RightBumper)
            {
                keyCommand = Control.None;
                sceneFromState.OnKeyRightBumper(activeButton);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(6) || keyCommand == Control.LeftTrigger)
            {
                BattleHUD.ForceNextTurn = true;
                keyCommand = Control.None;
                sceneFromState.OnKeyLeftTrigger(activeButton);
                return true;
            }
            if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(7) || keyCommand == Control.RightTrigger)
            {
                keyCommand = Control.None;
                sceneFromState.OnKeyRightTrigger(activeButton);
                return true;
            }
        }

        //if (AltKeyDown)
        //{
        //    if (F2Key)
        //    {
        //        OnPartySceneCommandDetected(sceneFromState);
        //        return true;
        //    }
        //    if (F4Key)
        //    {
        //        OnQuitCommandDetected(sceneFromState);
        //        return true;
        //    }
        //    if (F5Key)
        //    {
        //        OnSaveLoadSceneCommandDetected(sceneFromState, SaveLoadUI.SerializeType.Save);
        //        return true;
        //    }
        //    if (F9Key)
        //    {
        //        OnSaveLoadSceneCommandDetected(sceneFromState, SaveLoadUI.SerializeType.Load);
        //        return true;
        //    }
        //}

        if (AltKey)
        {
            if (F2KeyDown)
            {
                OnPartySceneCommandDetected(sceneFromState);
                return true;
            }
            if (F4KeyDown)
            {
                OnQuitCommandDetected(sceneFromState);
                return true;
            }
            if (F5KeyDown)
            {
                OnSaveLoadSceneCommandDetected(sceneFromState, SaveLoadUI.SerializeType.Save);
                return true;
            }
            if (F9KeyDown)
            {
                OnSaveLoadSceneCommandDetected(sceneFromState, SaveLoadUI.SerializeType.Load);
                return true;
            }
        }

        return false;
    }

    private void handleDialogControlKeyPressCustomInput(GameObject activeButton = null)
    {
        if (activeButton == null)
            activeButton = UICamera.selectedObject;
        if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(0) || keyCommand == Control.Confirm)
        {
            keyCommand = Control.None;
            PersistenSingleton<UIManager>.Instance.Dialogs.OnKeyConfirm(activeButton);
            if (PersistenSingleton<UIManager>.Instance.Dialogs.IsDialogNeedControl() || !PersistenSingleton<UIManager>.Instance.Dialogs.CompletlyVisible)
                return;
            triggleEventDialog = true;
        }
        else if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(1) || keyCommand == Control.Cancel)
        {
            keyCommand = Control.None;
            PersistenSingleton<UIManager>.Instance.Dialogs.OnKeyCancel(activeButton);
        }
        else if (PersistenSingleton<HonoInputManager>.Instance.IsInputDown(8) || keyCommand == Control.Pause)
        {
            keyCommand = Control.None;
            if (!PersistenSingleton<UIManager>.Instance.IsPauseControlEnable)
                return;
            PersistenSingleton<UIManager>.Instance.GetSceneFromState(PersistenSingleton<UIManager>.Instance.State).OnKeyPause(activeButton);
        }
        else
        {
            if (!PersistenSingleton<HonoInputManager>.Instance.IsInputDown(2) && keyCommand != Control.Menu)
                return;
            keyCommand = Control.None;
            if (!PersistenSingleton<UIManager>.Instance.IsMenuControlEnable)
                return;
            PersistenSingleton<UIManager>.Instance.GetSceneFromState(PersistenSingleton<UIManager>.Instance.State).OnKeyMenu(activeButton);
        }
    }

    protected virtual void OnSelect(bool selected)
    {
        if (!selected || PersistenSingleton<UIManager>.Instance.IsLoading || (!(ButtonGroupState.ActiveButton != null) || !(ButtonGroupState.ActiveButton != gameObject)) || (ButtonGroupState.AllTargetEnabled || !ButtonGroupState.ActiveButton.GetComponent<ButtonGroupState>().enabled))
            return;
        UICamera.selectedObject = ButtonGroupState.ActiveButton;
        ButtonGroupState.ActiveButton.GetComponent<ButtonGroupState>().SetHover(true);
    }

    public virtual void OnScreenButtonPressed(GameObject go)
    {
        keyCommand = go.GetComponent<OnScreenButton>().KeyCommand;
    }

    public void ResetKeyCode()
    {
        lazyKeyCommand = Control.None;
        isLockLazyInput = false;
    }

    public void SendKeyCode(Control control, bool isLock = false)
    {
        lazyKeyCommand = control;
        isLockLazyInput = isLock;
    }

    public Control GetLazyKey()
    {
        return lazyKeyCommand;
    }

    public void OnKeyNavigate(GameObject go, KeyCode key)
    {
        switch (key)
        {
            case KeyCode.UpArrow:
                lazyKeyCommand = Control.Up;
                break;
            case KeyCode.DownArrow:
                lazyKeyCommand = Control.Down;
                break;
            case KeyCode.RightArrow:
                lazyKeyCommand = Control.Right;
                break;
            case KeyCode.LeftArrow:
                lazyKeyCommand = Control.Left;
                break;
        }
    }

    public virtual void OnItemSelect(GameObject go)
    {
        UIScene sceneFromState = PersistenSingleton<UIManager>.Instance.GetSceneFromState(PersistenSingleton<UIManager>.Instance.State);
        sceneFromState?.OnItemSelect(go);

        if (PersistenSingleton<UIManager>.Instance.Dialogs != null)
            PersistenSingleton<UIManager>.Instance.Dialogs.OnItemSelect(go);

        if (!go.GetComponent<ScrollItemKeyNavigation>())
            return;

        ScrollItemKeyNavigation component = go.GetComponent<ScrollItemKeyNavigation>();
        if (!component || !component.ListPopulator)
            return;

        component.ListPopulator.itemHasChanged(go);
    }

    public static string ControlToString(Control control)
    {
        switch (control)
        {
            case Control.Confirm:
                return "Submit";
            case Control.Cancel:
                return "Cancel";
            case Control.Menu:
                return "Menu";
            case Control.Special:
                return "Special";
            case Control.LeftBumper:
                return "Left Bumper";
            case Control.RightBumper:
                return "Right Bumper";
            case Control.LeftTrigger:
                return "Left Trigger";
            case Control.RightTrigger:
                return "Right Trigger";
            case Control.Pause:
                return "Pause";
            case Control.Select:
                return "Select";
            default:
                return string.Empty;
        }
    }

    public bool ContainsAndroidQuitKey()
    {
        if (Application.platform != RuntimePlatform.Android || !UnityXInput.Input.GetKey(KeyCode.Escape))
        {
        }
        return false;
    }

    private void Start()
    {
        UICamera.onNavigate = (UICamera.KeyCodeDelegate)Delegate.Combine(UICamera.onNavigate, (UICamera.KeyCodeDelegate)OnKeyNavigate);
        GameLoopManager.RaiseStartEvent();
    }
}


namespace Memoria
{
    public static class UISceneHelper
    {
        public static void OpenPartyMenu()
        {
            FF9PARTY_INFO party = new FF9PARTY_INFO();

            Int32 availableChatacters = 0;
            if (Configuration.Hacks.AllCharactersAvailable > 0)
            {
                availableChatacters = 0x1FF;
                party.party_ct = 4;
            }
            else
            {
                for (int characterIndex = 8; characterIndex >= 0; --characterIndex)
                {
                    Boolean isAvailable = (FF9StateSystem.Common.FF9.player[characterIndex].info.party != 0);
                    if (isAvailable)
                        party.party_ct++;

                    availableChatacters = availableChatacters << 1 | (isAvailable ? 1 : 0);
                }

                if (party.party_ct > 4)
                    party.party_ct = 4;
            }

            for (int memberIndex = 0; memberIndex < 4; ++memberIndex)
            {
                if (FF9StateSystem.Common.FF9.party.member[memberIndex] != null)
                {
                    Byte characterId = FF9StateSystem.Common.FF9.party.member[memberIndex].info.slot_no;
                    party.menu[memberIndex] = characterId;
                    availableChatacters &= ~(1 << characterId);
                }
                else
                {
                    party.menu[memberIndex] = Byte.MaxValue;
                }
            }

            Byte availableSlot = 0;
            for (Byte characterId = 0; characterId < 9 && availableSlot < PartySettingUI.FF9PARTY_PLAYER_MAX && availableChatacters > 0; ++characterId)
            {
                if ((availableChatacters & 1) > 0)
                    party.select[availableSlot++] = characterId;

                availableChatacters >>= 1;
            }

            while (availableSlot < PartySettingUI.FF9PARTY_PLAYER_MAX)
                party.select[availableSlot++] = PartySettingUI.FF9PARTY_NONE;

            EventService.OpenPartyMenu(party);
        }
    }
}