// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/InputMaster.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class InputMaster : InputActionAssetReference
{
    public InputMaster()
    {
    }
    public InputMaster(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_OpenRadialMenu = m_Player.GetAction("OpenRadialMenu");
        m_Player_Movement = m_Player.GetAction("Movement");
        m_Player_Look = m_Player.GetAction("Look");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_Player = null;
        m_Player_OpenRadialMenu = null;
        m_Player_Movement = null;
        m_Player_Look = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Player
    private InputActionMap m_Player;
    private InputAction m_Player_OpenRadialMenu;
    private InputAction m_Player_Movement;
    private InputAction m_Player_Look;
    public struct PlayerActions
    {
        private InputMaster m_Wrapper;
        public PlayerActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenRadialMenu { get { return m_Wrapper.m_Player_OpenRadialMenu; } }
        public InputAction @Movement { get { return m_Wrapper.m_Player_Movement; } }
        public InputAction @Look { get { return m_Wrapper.m_Player_Look; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
    }
    public PlayerActions @Player
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PlayerActions(this);
        }
    }
    private int m_KeyboradMouseSchemeIndex = -1;
    public InputControlScheme KeyboradMouseScheme
    {
        get

        {
            if (m_KeyboradMouseSchemeIndex == -1) m_KeyboradMouseSchemeIndex = asset.GetControlSchemeIndex("Keyborad & Mouse");
            return asset.controlSchemes[m_KeyboradMouseSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get

        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.GetControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
}
