﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using com.spacepuppy.Utils;

namespace com.spacepuppy.Scenes
{

    public interface ISceneBehaviourLoadOptions
    {

        System.Type SceneBehaviourType { get; }

        void OnBeforeSceneLoaded(SceneManager manager, ISceneBehaviour sceneBehaviour);

        void OnSceneLoaded(SceneManager manager, ISceneBehaviour sceneBehaviour);

        void OnSceneStarted(SceneManager manager, ISceneBehaviour sceneBehaviour);

    }

    public class SceneBehaviourLoadOptions : ISceneBehaviourLoadOptions
    {

        #region Fields

        private System.Type _sceneBehaviourType;

        #endregion

        #region CONSTRUCTOR

        public SceneBehaviourLoadOptions(System.Type tp)
        {
            if (tp == null) throw new System.ArgumentNullException("tp");
            if (!TypeUtil.IsType(tp, typeof(ISceneBehaviour))) throw new TypeArgumentMismatchException(tp, typeof(ISceneBehaviour), "tp");
            _sceneBehaviourType = tp;
        }

        #endregion

        #region Methods

        public event System.Action<ISceneBehaviour> BeforeSceneCreated;
        public event System.Action<ISceneBehaviour> SceneLoaded;
        public event System.Action<ISceneBehaviour> SceneStarted;

        protected virtual void OnBeforeSceneLoaded(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            if (this.BeforeSceneCreated != null) this.BeforeSceneCreated(sceneBehaviour);
        }

        protected virtual void OnSceneLoaded(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            if (this.SceneLoaded != null) this.SceneLoaded(sceneBehaviour);
        }

        protected virtual void OnSceneStarted(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            if (this.SceneStarted != null) this.SceneStarted(sceneBehaviour);
        }

        #endregion

        #region ISceneLoadOptions Interface

        System.Type ISceneBehaviourLoadOptions.SceneBehaviourType
        {
            get { return _sceneBehaviourType; }
        }

        void ISceneBehaviourLoadOptions.OnBeforeSceneLoaded(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            this.OnBeforeSceneLoaded(manager, sceneBehaviour);
        }

        void ISceneBehaviourLoadOptions.OnSceneLoaded(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            this.OnSceneLoaded(manager, sceneBehaviour);
        }

        void ISceneBehaviourLoadOptions.OnSceneStarted(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            this.OnSceneStarted(manager, sceneBehaviour);
        }

        #endregion

    }

    public class SceneBehaviourLoadOptions<T> : ISceneBehaviourLoadOptions where T : class, ISceneBehaviour
    {

        #region Methods

        public event System.Action<T> BeforeSceneLoaded;
        public event System.Action<T> SceneLoaded;
        public event System.Action<T> SceneStarted;

        protected virtual void OnBeforeSceneLoaded(SceneManager manager, T sceneBehaviour)
        {
            if (this.BeforeSceneLoaded != null) this.BeforeSceneLoaded(sceneBehaviour);
        }

        protected virtual void OnSceneLoaded(SceneManager manager, T sceneBehaviour)
        {
            if (this.SceneLoaded != null) this.SceneLoaded(sceneBehaviour);
        }

        protected virtual void OnSceneStarted(SceneManager manager, T sceneBehaviour)
        {
            if (this.SceneStarted != null) this.SceneStarted(sceneBehaviour);
        }

        #endregion

        #region ISceneLoadOptions Interface

        System.Type ISceneBehaviourLoadOptions.SceneBehaviourType
        {
            get { return typeof(T); }
        }

        void ISceneBehaviourLoadOptions.OnBeforeSceneLoaded(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            if (!(sceneBehaviour is T)) return;
            this.OnBeforeSceneLoaded(manager, sceneBehaviour as T);
        }

        void ISceneBehaviourLoadOptions.OnSceneLoaded(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            if (!(sceneBehaviour is T)) return;
            this.OnSceneLoaded(manager, sceneBehaviour as T);
        }

        void ISceneBehaviourLoadOptions.OnSceneStarted(SceneManager manager, ISceneBehaviour sceneBehaviour)
        {
            if (!(sceneBehaviour is T)) return;
            this.OnSceneStarted(manager, sceneBehaviour as T);
        }

        #endregion

    }

}
