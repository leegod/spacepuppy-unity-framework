﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace com.spacepuppy.Async
{

    public abstract class RadicalAsyncOperation : IRadicalYieldInstruction
    {

        #region Fields

        private bool _complete;

        #endregion

        #region CONSTRUCTOR

        public RadicalAsyncOperation()
        {
        }

        #endregion

        #region Properties

        public bool IsComplete
        {
            get { return _complete; }
        }

        protected virtual object CurrentYieldObject
        {
            get { return null; }
        }

        #endregion

        #region Methods

        public void Begin()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(this.AsyncCallback, null);
        }

        private void AsyncCallback(object state)
        {
            this.DoAsyncWork();
        }

        protected abstract void DoAsyncWork();

        protected void SetSignal()
        {
            _complete = true;
        }

        #endregion

        #region IRadicalYieldInstruction Interface

        object System.Collections.IEnumerator.Current
        {
            get { return this.CurrentYieldObject; }
        }

        bool System.Collections.IEnumerator.MoveNext()
        {
            return !_complete;
        }

        void System.Collections.IEnumerator.Reset()
        {
            throw new System.NotSupportedException();
        }

        #endregion

    }

}
