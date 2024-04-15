using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base level flow control
/// </summary>
namespace YXCell
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(YXCommonTimer))]
    public sealed class YXCore : MonoSingleton<YXCore>
    {

        public static Action Awake000 = new Action(EmptyAction), Awake100 = new Action(EmptyAction),
                              Start000 = new Action(EmptyAction), Start100 = new Action(EmptyAction), Start200 = new Action(EmptyAction),
                              Tick000 = new Action(EmptyAction), Tick100 = new Action(EmptyAction), Tick200 = new Action(EmptyAction), Tick300 = new Action(EmptyAction), Tick400 = new Action(EmptyAction), Tick500 = new Action(EmptyAction),
                              FixedTick000 = new Action(EmptyAction), FixedTick100 = new Action(EmptyAction), FixedTick200 = new Action(EmptyAction),
                              LateTick000 = new Action(EmptyAction), LateTick100 = new Action(EmptyAction), LateTick200 = new Action(EmptyAction),
                              OnSceneLoaded000 = new Action(EmptyAction), OnSceneLoaded100 = new Action(EmptyAction);

        /// <summary>
        /// Initialize SKCell modules
        /// </summary>
        private void Initialize()
        {
            YXUtils.EditorLogNormal("Core Initialize"); 
        }

        #region Unity Lifecycle

        protected override void Awake()
        {
            base.Awake();

            Awake000();
            Awake100();
        }
        private void Start()
        {
            Initialize();
            Start000();
            Start100();
            Start200();
        }
        private void Update()
        {
            Tick000();
            Tick100();
            Tick200();
            Tick300();
            Tick400();
            Tick500();
        }
        private void FixedUpdate()
        {
            FixedTick000();
            FixedTick100();
            FixedTick200();
        }

        private void LateUpdate()
        {
            LateTick000();
            LateTick100();
            LateTick200();
        }
        //private void OnLevelWasLoaded(int level)
        //{
        //    OnSceneLoaded000();
        //    OnSceneLoaded100();
        //}

        #endregion
        private static void EmptyAction() { }
    }

    /// <summary>
    /// Represents an SK module manager class.
    /// </summary>
    //public enum SKModule
    //{
    //    CoreModule,
    //    InputModule,
    //    MediaModule,
    //    EnvironmentModule,
    //    LocalizationModule,
    //    CSVModule,
    //    FSMModule,
    //    GridModule,
    //    ObjectPoolModule,
    //    TimeModule,
    //    PhysicsModule,
    //    UIModule,
    //    StructureModule
    //}
}
