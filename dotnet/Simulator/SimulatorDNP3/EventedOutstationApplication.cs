﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DNP3.Interface;

namespace Automatak.Simulator.DNP3
{
    class EventedOutstationApplication : IOutstationApplication
    {
        bool supportsWriteAbsoluteTime = false;
        ApplicationIIN appIIN = new ApplicationIIN();
        RestartMode coldRestartSupport = RestartMode.UNSUPPORTED;
        RestartMode warmRestartSupport = RestartMode.UNSUPPORTED;
        UInt16 coldRestartTime = 0;
        UInt16 warmRestartTime = 0;

        public bool SupportsWriteTime
        {
            get
            {
                return supportsWriteAbsoluteTime;
            }
            set
            {
                supportsWriteAbsoluteTime = value;
            }
        }

        public bool LocalMode
        {
            get
            {
                return appIIN.localControl;
            }
            set
            {
               appIIN.localControl = value;
            }
        }

        public bool NeedTime
        {
            get
            {
                return appIIN.needTime;
            }
            set
            {
                appIIN.needTime = value;
            }
        }

        public RestartMode ColdRestartMode
        {
            get
            {
                return coldRestartSupport;
            }
            set
            {
                coldRestartSupport = value;
            }
        }

        public RestartMode WarmRestartMode
        {
            get
            {
                return warmRestartSupport;
            }
            set
            {
                warmRestartSupport = value;
            }
        }

        public UInt16 ColdRestartTime
        {
            get
            {
                return coldRestartTime;
            }
            set
            {
                coldRestartTime = value;
            }
        }

        public UInt16 WarmRestartTime
        {
            get
            {
                return warmRestartTime;
            }
            set
            {
                warmRestartTime = value;
            }
        }

        public delegate void OnWriteTime(ulong millisecSinceEpoch);
        public event OnWriteTime TimeWrite;

        public delegate void OnRestartRequested();
        public event OnRestartRequested ColdRestart;
        public event OnRestartRequested WarmRestart;

        bool IOutstationApplication.SupportsWriteAbsoluteTime
        {
            get { return supportsWriteAbsoluteTime; }
        }

        bool IOutstationApplication.WriteAbsoluteTime(ulong millisecSinceEpoch)
        {
            if (TimeWrite != null)
            {
                TimeWrite(millisecSinceEpoch);
            }

            return true;
        }

        ApplicationIIN IOutstationApplication.ApplicationIndications
        {
            get { return appIIN; }
        }

        RestartMode IOutstationApplication.ColdRestartSupport
        {
            get { return coldRestartSupport; }
        }

        RestartMode IOutstationApplication.WarmRestartSupport
        {
            get { return warmRestartSupport; }
        }

        ushort IOutstationApplication.ColdRestart()
        {
            if (ColdRestart != null)
            {
                ColdRestart();
            }

            return coldRestartTime;
        }

        ushort IOutstationApplication.WarmRestart()
        {
            if (WarmRestart != null)
            {
                WarmRestart();
            }

            return warmRestartTime; 
        }
    }
}
