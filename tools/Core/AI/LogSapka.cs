﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AI
{
    public class LogSapka : ISapkaMindView
    {
        public IList<char> LastDecisionPath
        {
            get { return lastDecisionPath; }
            set { lastDecisionPath = value;  }
        }

        public string LastDecisionName
        {
            get { return lastDecisionName; }
            set { lastDecisionName = value; }
        }

        private IList<char> lastDecisionPath = new char[0];
        private string lastDecisionName = "?";
    }
}
