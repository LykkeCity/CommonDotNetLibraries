﻿using System;
using JetBrains.Annotations;

namespace Lykke.Common.Log
{
    /// <summary>
    /// Abstraction for app health notifier
    /// </summary>
    [PublicAPI]
    public interface IHealthNotifier : IDisposable
    {
        /// <summary>
        /// Notifies about app health changes
        /// </summary>
        /// <param name="healthMessage">Message that describes app health change</param>
        /// <param name="context">Health change context, if any</param>
        void Notify([NotNull] string healthMessage, [CanBeNull] object context = null);
    }
}
