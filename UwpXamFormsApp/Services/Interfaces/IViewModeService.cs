﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UwpXamFormsApp.Services
{
    public interface IViewModeService
    {
        void TryEnterFullScreenMode();
        void ExitFullScreenMode();
    }
}
